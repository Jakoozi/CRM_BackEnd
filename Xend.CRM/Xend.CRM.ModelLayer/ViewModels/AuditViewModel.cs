using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class AuditViewModel : BASE_ENTITY
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
