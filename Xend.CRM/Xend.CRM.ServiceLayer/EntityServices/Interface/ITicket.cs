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
		Task<IEnumerable<Ticket>> GetAllTicketsService();
		TicketServiceResponseModel GetTicketByIdService(Guid id);

	}
}
