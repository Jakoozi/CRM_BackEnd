using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ITicket
    {
        void CreateTicketService();
        void UpdateTicketService();
        void GetAllTicketsService();
        void GetTicketByIdService();
        void DeleteTicketService();
    }
}
