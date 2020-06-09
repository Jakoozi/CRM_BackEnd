using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class CustomerViewModel
    {
		public Guid Id { get; set; }
		public Guid Company_Id { get; set; }
		public Guid Createdby_Userid { get; set; }
		public Guid?  Updatedby_Userid { get; set; }
		public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string XendCode { get; set; }

      
	}
}
