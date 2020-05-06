using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ITicket
    {
		TicketServiceResponseModel CreateTicketService(TicketViewModel ticket);
		TicketServiceResponseModel UpdateTicketService(TicketViewModel ticket);
		TicketServiceResponseModel DeleteTicketService(Guid id);
		TicketServiceResponseModel CloseTicketService(Guid id);
		TicketServiceResponseModel GetTicketByIdService(Guid id);
		Task<IEnumerable<Ticket>> GetAllTicketsService();
		Task<IEnumerable<Ticket>> GetDeletedTicketsService();
		Task<IEnumerable<Ticket>> GetTicketByCompany_IdService(Guid id);
		Task<IEnumerable<Ticket>> GetTicketByCustomer_IdService(Guid id);
		Task<IEnumerable<Ticket>> GetTicketByCreated_UserIdService(Guid id);
		Task<IEnumerable<Ticket>> GetTicketByResolved_UserIdService(Guid id);
	}
}
