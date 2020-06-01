using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ModelExtensions;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ServiceLayer.ServiceExtentions;

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class TicketServices : BaseService, ITicket
	{

		ILoggerManager _loggerManager { get; }
		IAuditExtension _iauditExtension { get; }
		ITicketExtension _iticket { get; }
		IEmailService _iEmailService { get; }
		TicketServiceResponseModel ticketModel;
		ResponseCodes responseCode = new ResponseCodes();
		public TicketServices(IUnitOfWork<XendDbContext> unitOfWork,IEmailService iEmailService, ITicketExtension iticket, IAuditExtension iauditExtention, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iauditExtension = iauditExtention;
			_iticket = iticket;
			_iEmailService = iEmailService;
		}
		//this service creates new tickets
		public async Task<TicketServiceResponseModel> CreateTicketService(TicketViewModel ticket)
		{
			try
			{
				Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == ticket.Company_Id && p.Status == EntityStatus.Active);
				if (checkIfCompanyExists != null)
				{
					User checkIfUserExists = UnitOfWork.GetRepository<User>().Single(p => p.Id == ticket.Createdby_Userid && p.Status == EntityStatus.Active);
					if (checkIfUserExists != null)
					{
						Ticket toBeCreatedTicket = new Ticket
						{
							Company_Id = ticket.Company_Id,
							Customer_Id = ticket.Customer_Id,
							Createdby_Userid = ticket.Createdby_Userid,
							//Resolvedby_Entityid = ticket.Resolvedby_Entityid,
							Ticket_Subject = ticket.Ticket_Subject,
							Ticket_Details = ticket.Ticket_Details,
							Ticket_Status = Ticket_Status.New,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp(),

						};

						//create email
						Customer customer = UnitOfWork.GetRepository<Customer>().Single(p => p.Id == ticket.Customer_Id && p.Status == EntityStatus.Active);
						string customerFullName = $"{customer.First_Name} {customer.Last_Name}";
						var emailResponse = await _iEmailService.SendTicketCreatedEmail(customer.Email, "Ticket Created", new object[] { customerFullName, ticket.Ticket_Subject, ticket.Ticket_Details });


						if(emailResponse != null)
						{
							
							UnitOfWork.GetRepository<Ticket>().Add(toBeCreatedTicket);
							UnitOfWork.SaveChanges();

							//Audit Logger
							_iauditExtension.Auditlogger(toBeCreatedTicket.Company_Id, toBeCreatedTicket.Createdby_Userid, "You Created a Ticket");

							ticketModel = new TicketServiceResponseModel() { ticket = toBeCreatedTicket, Message = "Entity Created Successfully", code = responseCode.Successful };
							return ticketModel;
						
						}
						else
						{
							ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Email not sent, please try again.", code = responseCode.ErrorOccured };
							return ticketModel;
						}

					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Ticket Creator Do Not Exist", code = responseCode.ErrorOccured };
						return ticketModel;
					}	
				}
				else
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Company Do Not Exist", code = responseCode.ErrorOccured };
					return ticketModel;
					
				}

			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}

		}

		//this service updates tickets
		public async Task<TicketServiceResponseModel> ResolveTicketService(TicketViewModel ticket)
		{
			try
			{
				Ticket toBeUpdatedTicket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == ticket.Id);
				if (toBeUpdatedTicket == null)
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return ticketModel;
				}
				else
				{
					if (toBeUpdatedTicket.Status == EntityStatus.Active)
					{
						if(toBeUpdatedTicket.Ticket_Status != Ticket_Status.Closed)
						{
							Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == toBeUpdatedTicket.Company_Id && p.Status == EntityStatus.Active);
							if (checkIfCompanyExists != null)
							{
								User checkIfResolverIsAUser = UnitOfWork.GetRepository<User>().Single(p => p.Id == ticket.Resolvedby_Entityid && p.Status == EntityStatus.Active);
								if (checkIfResolverIsAUser != null)
								{
									//Removed stuff from here
									Ticket extensionServiceResponse = await _iticket.TicketResolver(ticket);
									if(extensionServiceResponse != null)
									{
										ticketModel = new TicketServiceResponseModel() { ticket = extensionServiceResponse, Message = "Staff Response Sent Successfully", code = responseCode.Successful };
										return ticketModel;
									}			
									else
									{
										ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Email not sent, please try again.", code = responseCode.ErrorOccured };
										return ticketModel;
									}
								}
								else
								{
									ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Resolvig User Does not Exist", code = responseCode.ErrorOccured };
									return ticketModel;
								}

							}
							else
							{
								ticketModel = new TicketServiceResponseModel() { ticket = toBeUpdatedTicket, Message = "Company Do Not Exist", code = responseCode.ErrorOccured };
								return ticketModel;
							}
						}
						else
						{
							ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "This Ticket is Closed", code = responseCode.ErrorOccured };
							return ticketModel;
						}
						
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return ticketModel;
					}

				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		public TicketServiceResponseModel DeleteTicketService(Guid id)
		{
			try
			{

				Ticket ticket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == id);
				if (ticket == null)
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return ticketModel;
				}
				else
				{
					if (ticket.Status == EntityStatus.Active)
					{
						ticket.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Ticket>().Update(ticket);
						UnitOfWork.SaveChanges();

						//Audit logger
						Guid idOfUserWhoDeleted_Ticket = ticket.Resolvedby_Entityid.GetValueOrDefault();
						_iauditExtension.Auditlogger(ticket.Company_Id, idOfUserWhoDeleted_Ticket, "You Deleted a Ticket");

						ticketModel = new TicketServiceResponseModel() { ticket = ticket, Message = "Entity Deleted Successfully", code = responseCode.Successful };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return ticketModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		public TicketServiceResponseModel CloseTicketService(Guid id)
		{
			try
			{
				Ticket ticket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == id);
				if (ticket == null)
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return ticketModel;
				}
				else
				{
					if (ticket.Status == EntityStatus.Active)
					{
						ticket.Ticket_Status = Ticket_Status.Closed;
						UnitOfWork.GetRepository<Ticket>().Update(ticket);
						UnitOfWork.SaveChanges();

						//Audit logger
						Guid idOfUserWhoClosed_Ticket = ticket.Resolvedby_Entityid.GetValueOrDefault();
						_iauditExtension.Auditlogger(ticket.Company_Id, idOfUserWhoClosed_Ticket, "You Closed a Ticket");

						ticketModel = new TicketServiceResponseModel() { ticket = ticket, Message = "Ticket Closed Successfully", code = responseCode.Successful };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return ticketModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		//this service fetches ticket by there id
		public TicketServiceResponseModel GetTicketByIdService(Guid id)
		{
			try
			{
				Ticket ticket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == id);

				//since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
				//CompanyViewModel companyViewModel = new CompanyViewModel
				//{
				//    Company_Name = company.Company_Name,
				//    Id = company.Id
				//};

				if (ticket != null)
				{
					if (ticket.Status == EntityStatus.Active)
					{
						ticketModel = new TicketServiceResponseModel() { ticket = ticket, Message = "Entity Fetched Successfully", code = responseCode.Successful };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return ticketModel;
					}
				}
				ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
				return ticketModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this service fetches all the tickets
		public async Task<IEnumerable<Ticket>> GetAllTicketsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Status == EntityStatus.Active && t.Ticket_Status != Ticket_Status.Closed);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}
		//this service fetches deleted tickets
		public async Task<IEnumerable<Ticket>> GetDeletedTicketsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Status == EntityStatus.InActive);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this method gets tickets by company id
		public async Task<IEnumerable<Ticket>> GetTicketByCompany_IdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t =>t.Company_Id == id && t.Status == EntityStatus.Active && t.Ticket_Status != Ticket_Status.Closed);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this method gets tickets by there customer id
		public async Task<IEnumerable<Ticket>> GetTicketByCustomer_IdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Customer_Id == id && t.Status == EntityStatus.Active);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this method gets users by the created users id
		public async Task<IEnumerable<Ticket>> GetTicketByCreated_UserIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Createdby_Userid == id && t.Status == EntityStatus.Active);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this method gets tickets by the resolved users id
		public async Task<IEnumerable<Ticket>> GetTicketByResolved_UserIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Resolvedby_Entityid == id && t.Status == EntityStatus.Active);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
	}
}
