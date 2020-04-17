using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class CompanyViewModel 
    {
        public Guid? Id { get; set; }
        [Required]
        public string Company_Name { get; set; }

    }
}
