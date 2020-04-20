using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ITicket
    {
		TicketServiceResponseModel CreateTicketService(TicketViewModel ticket);

		void UpdateTicketService();
        void GetAllTicketsService();
        void GetTicketByIdService();
        void DeleteTicketService();
    }
}
