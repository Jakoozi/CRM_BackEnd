using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
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
						Ticket toBeCreatedTicket = new Ticket
						{
							//Company_Id = ticket.Customer_Id,
							//Customer_Id = ticket.Customer_Id,
							//Createdby_Userid = ticket.Createdby_Userid,
							//Resolvedby_Userid = ticket.Resolvedby_Userid,
							//Resolvedby_Teamid = ticket.Resolvedby_Teamid,
							//Ticket_Subject = ticket.Ticket_Subject,
							//Ticket_Details = ticket.Ticket_Details,
							//Ticket_Status = ticket.Ticket_Status,
							//Status = EntityStatus.Active,
							//CreatedAt = DateTime.Now,
							//CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							//UpdatedAt = DateTime.Now,
							//UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp(),

						};
						UnitOfWork.GetRepository<Ticket>().Add(toBeCreatedTicket);
						UnitOfWork.SaveChanges();

						ticketModel = new TicketServiceResponseModel() { ticket = toBeCreatedTicket, Message = "Entity Created Successfully", code = "002" };
						return ticketModel;

			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}

		}

        //this service updates tickets
        public void UpdateTicketService()
        {

        }

        //this service fetches all the tickets
        public void GetAllTicketsService()
        {

        }

        //this service fetches ticket by there id
        public void GetTicketByIdService()
        {

        }

        //this service deletes ticket
        public void DeleteTicketService()
        {

        }
    }
}
