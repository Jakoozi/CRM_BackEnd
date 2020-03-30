using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class CustomerViewModel : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string XendCode { get; set; }

        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }
    }
}
