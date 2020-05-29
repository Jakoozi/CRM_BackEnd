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
    
    }
}
