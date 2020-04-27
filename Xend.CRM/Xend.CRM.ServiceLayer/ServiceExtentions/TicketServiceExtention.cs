using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.Common.ExtensionMethods;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.ServiceExtentions
{
	public class TicketServiceExtention : BaseService
	{
		ILoggerManager _loggerManager { get; }
		public TicketServiceExtention(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}
		public string TicketUpdater(TicketViewModel ticket)
		{
			Ticket toBeUpdatedTicket = UnitOfWork.GetRepository<Ticket>().Single(p => p.Id == ticket.Id);
			//here i will assign directly what i want to update to the model instead of creating a new instance
			//toBeUpdatedUser.Company_Id = user.Company_Id;
			toBeUpdatedTicket.Resolvedby_Entityid = ticket.Resolvedby_Entityid;
			//toBeUpdatedTicket.Resolvedby_Teamid = ticket.Resolvedby_Teamid;
			toBeUpdatedTicket.Ticket_Status = ticket.Ticket_Status;
			toBeUpdatedTicket.Status = EntityStatus.Active;
			toBeUpdatedTicket.UpdatedAt = DateTime.Now;
			toBeUpdatedTicket.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
			UnitOfWork.GetRepository<Ticket>().Update(toBeUpdatedTicket); ;
			UnitOfWork.SaveChanges();

			return "Successful";
		}
	}
}
