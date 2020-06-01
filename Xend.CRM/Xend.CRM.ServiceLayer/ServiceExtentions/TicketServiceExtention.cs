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
using Xend.CRM.ServiceLayer.ServiceExtentions;

namespace Xend.CRM.ServiceLayer.ServiceExtentions
{
	public class TicketServiceExtention : BaseService, ITicketExtension
	{
		ILoggerManager _loggerManager { get; }
		IEmailService _iEmailService { get; }
		IAuditExtension _iauditExtension { get; }
		public TicketServiceExtention(IUnitOfWork<XendDbContext> unitOfWork, IEmailService iEmailService, IAuditExtension iauditExtention, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iauditExtension = iauditExtention;
			_iEmailService = iEmailService;
		}
		public async Task<Ticket> TicketResolver(TicketViewModel ticket)
		{
			Ticket toBeUpdatedTicket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == ticket.Id);
			//here i will assign directly what i want to update to the model instead of creating a new instance
			//toBeUpdatedUser.Company_Id = user.Company_Id;
			toBeUpdatedTicket.Resolvedby_Entityid = ticket.Resolvedby_Entityid;
			toBeUpdatedTicket.Staff_Response = ticket.Staff_Response;
			//toBeUpdatedTicket.Resolvedby_Teamid = ticket.Resolvedby_Teamid;
			toBeUpdatedTicket.Ticket_Status = Ticket_Status.Resolved;
			toBeUpdatedTicket.Status = EntityStatus.Active;
			toBeUpdatedTicket.UpdatedAt = DateTime.Now;
			toBeUpdatedTicket.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
			

			//create email
			Customer customer = UnitOfWork.GetRepository<Customer>().Single(p => p.Id == toBeUpdatedTicket.Customer_Id && p.Status == EntityStatus.Active);
			string customerFullName = $"{customer.First_Name} {customer.Last_Name}";
			var emailResponse = await _iEmailService.SendTicketReplyEmail(customer.Email, "Ticket Resolved", new object[] { customerFullName, ticket.Ticket_Subject, ticket.Ticket_Details, ticket.Staff_Response });

			if (emailResponse != null)
			{
				UnitOfWork.GetRepository<Ticket>().Update(toBeUpdatedTicket); 
				UnitOfWork.SaveChanges();

				//Audit Logger. This update means Resolve, that is the ticket was 
				Guid idOfUserWhoResolved_Ticket = ticket.Resolvedby_Entityid.GetValueOrDefault();
				_iauditExtension.Auditlogger(toBeUpdatedTicket.Company_Id, idOfUserWhoResolved_Ticket, "You Resolved a Ticket");

				return toBeUpdatedTicket;
			}
			else
			{
				return null;
			}
		}
	}
}
