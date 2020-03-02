using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xend.CRM.ModelLayer.Entities
{
    public class Audit_Rail : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
        public Guid User_Id { get; set; }
        public string Activity { get; set; }
        //public DateTime Time_Of_Activity { get; set; }

        //This creates a relationship between the foriegn key tables and the AuditRail Table in my Database. 
        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }

        [ForeignKey("User_Id")]
        public virtual User User { get; set; }

    }

}
