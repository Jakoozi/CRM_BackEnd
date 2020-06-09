using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.Entities
{
    public class Ticket : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
        public Guid Customer_Id { get; set; }
        public Guid Createdby_Userid { get; set; }
		public Guid? Resolvedby_Entityid { get; set; }
		//public Guid? Resolvedby_Userid { get; set; }
		//public Guid? Resolvedby_Teamid { get; set; }
		//the ? after the Guid is for nullable entity value
		public string Company_Name { get; set; }
		public string Ticket_Subject { get; set; }
        public string Ticket_Details { get; set; }
		public string Staff_Response { get; set; }
		public Ticket_Status Ticket_Status { get; set; }

		//This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
		[ForeignKey("Company_Id")]
		public virtual Company Company { get; set; }

		[ForeignKey("Customer_Id")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Createdby_Userid")]
        public virtual User CreatedUser { get; set; }

        [ForeignKey("Resolvedby_Entityid")]
		public virtual User ResolvedUser { get; set; }

    }
}
