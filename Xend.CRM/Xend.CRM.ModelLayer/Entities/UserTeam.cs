using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Enums;



namespace Xend.CRM.ModelLayer.Entities
{
	public class UserTeam : BASE_ENTITY
	{
		public Guid Company_Id { get; set; }
		public Guid Team_Id { get; set; }
		public Guid User_Id { get; set; }
		public string Company_Name { get; set; }
		public string Team_Name { get; set; }
		public string User_Name { get; set; }

		//This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
		[ForeignKey("Company_Id")]
		public virtual Company Company { get; set; }
		[ForeignKey("Team_Id")]
		public virtual Team Team { get; set; }
		[ForeignKey("User_Id")]
		public virtual User user { get; set; }
	}
}
