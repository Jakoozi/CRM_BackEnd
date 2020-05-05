using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
	public interface ITicketExtension
	{
		Ticket TicketUpdater(TicketViewModel ticket);
	}
}
