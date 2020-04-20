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
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class TicketServices : BaseService, ITicket
	{

		ILoggerManager _loggerManager { get; }
		TicketServiceResponseModel ticketModel;
		public TicketServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}
		//this service creates new tickets
		public TicketServiceResponseModel CreateTicketService(TicketViewModel ticket)
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
							Company_Id = ticket.Customer_Id,
							Customer_Id = ticket.Customer_Id,
							Createdby_Userid = ticket.Createdby_Userid,
							Resolvedby_Userid = ticket.Resolvedby_Userid,
							Resolvedby_Teamid = ticket.Resolvedby_Teamid,
							Ticket_Subject = ticket.Ticket_Subject,
							Ticket_Details = ticket.Ticket_Details,
							Ticket_Status = ticket.Ticket_Status,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp(),

						};
						UnitOfWork.GetRepository<Ticket>().Add(toBeCreatedTicket);
						UnitOfWork.SaveChanges();

						ticketModel = new TicketServiceResponseModel() { ticket = toBeCreatedTicket, Message = "Entity Created Successfully", code = "002" };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Ticket Creator Do Not Exist", code = "006" };
						return ticketModel;
					}	
				}
				else
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Company Do Not Exist", code = "005" };
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
		public TicketServiceResponseModel UpdateTicketService(TicketViewModel ticket)
		{
			
			try
			{
				Ticket toBeUpdatedTicket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == ticket.Id);
				if (toBeUpdatedTicket == null)
				{
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
					return ticketModel;
				}
				else
				{
					if (toBeUpdatedTicket.Status == EntityStatus.Active)
					{
						Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == ticket.Company_Id && p.Status == EntityStatus.Active);
						if (checkIfCompanyExists != null)
						{
							User checkIfUserExists = UnitOfWork.GetRepository<User>().Single(p => p.Id == ticket.Resolvedby_Userid && p.Status == EntityStatus.Active);
							if (checkIfUserExists != null)
							{

								//here i will assign directly what i want to update to the model instead of creating a new instance
								//toBeUpdatedUser.Company_Id = user.Company_Id;
								toBeUpdatedTicket.Resolvedby_Userid = ticket.Resolvedby_Userid;
								toBeUpdatedTicket.Resolvedby_Teamid = ticket.Resolvedby_Teamid;
								toBeUpdatedTicket.Ticket_Status = ticket.Ticket_Status;
								toBeUpdatedTicket.Status = EntityStatus.Active;
								toBeUpdatedTicket.UpdatedAt = DateTime.Now;
								toBeUpdatedTicket.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
								UnitOfWork.GetRepository<Ticket>().Update(toBeUpdatedTicket); ;
								UnitOfWork.SaveChanges();

								ticketModel = new TicketServiceResponseModel() { ticket = toBeUpdatedTicket, Message = "Entity Updated Successfully", code = "002" };
								return ticketModel;
							}
							else
							{
								
								ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Ticket Resolver Do Not Exist", code = "006" };
								return ticketModel;
							}

						}
						else
						{
							ticketModel = new TicketServiceResponseModel() { ticket = toBeUpdatedTicket, Message = "Company Do Not Exist", code = "005" };
							return ticketModel;
						}
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
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
					ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
					return ticketModel;
				}
				else
				{
					if (ticket.Status == EntityStatus.Active)
					{
						ticket.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Ticket>().Update(ticket);
						UnitOfWork.SaveChanges();

						ticketModel = new TicketServiceResponseModel() { ticket = ticket, Message = "Entity Deleted Successfully", code = "002" };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
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
		//this service fetches all the tickets
		public async Task<IEnumerable<Ticket>> GetAllTicketsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync(t => t.Status == EntityStatus.Active);
				return tickets;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
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
						ticketModel = new TicketServiceResponseModel() { ticket = ticket, Message = "Entity Fetched Successfully", code = "002" };
						return ticketModel;
					}
					else
					{
						ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
						return ticketModel;
					}
				}
				ticketModel = new TicketServiceResponseModel() { ticket = null, Message = "Entity Does Not Exist", code = "001" };
				return ticketModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		public void GetTicketByIdService()
        {

        }

    }
}
