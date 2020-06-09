using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Xend.CRM.ModelLayer.Entities
{
    public class Customer : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
		public Guid Createdby_Userid { get; set; }
		public Guid? Updatedby_Userid { get; set; }
		public string Company_Name { get; set; }
		public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string XendCode { get; set; }

        //This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }
		[ForeignKey("Createdby_Userid")]
		public virtual User CreatedUser { get; set; }
		[ForeignKey("Updatedby_Userid")]
		public virtual User UpdatedUser { get; set; }


	}
}
