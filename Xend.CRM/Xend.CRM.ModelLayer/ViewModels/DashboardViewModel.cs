using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.ViewModels
{
	public class DashboardViewModel
	{
		public int NumberOf_NewTickets { get; set; }
		public int NumberOf_ResolvedTickets { get ; set; }
		public int NumberOf_ClosedTickets { get; set; }
		public int NumberOf_Tickets { get; set; }
		public int TotalNumberOfTickets { get; set; }
		public int NumberOf_Admins { get; set; }
		public int NumberOf_Agents { get; set; }
		public int TotalNumberOfUsers { get; set; }
		public int TotalNumberOfCompanies { get; set; }
		public int TotalNumberOfCustomers { get; set; }
		public int NumberOf_Customers { get; set; }
		public int TotalNumberOfTeams { get; set; }
		public int NumberOf_Teams { get; set; }
	}
}
