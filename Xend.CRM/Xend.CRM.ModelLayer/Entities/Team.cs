using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xend.CRM.ModelLayer.Entities
{
    public class Team : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
		public Guid Createdby_Userid { get; set; }
		public string Team_Name { get; set; }

        //This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }
		[ForeignKey("Createdby_Userid")]
		public virtual User CreatedUser { get; set; }
	}
}
