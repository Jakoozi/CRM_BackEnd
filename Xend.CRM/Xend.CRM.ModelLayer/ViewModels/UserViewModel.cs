using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class UserViewModel : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
		public Guid Team_Id { get; set; }
		public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phonenumber { get; set; }
        public string Email { get; set; }
        public string XendCode { get; set; }
        public User_Role User_Role { get; set; }
		public string User_Password { get; set; }

		[ForeignKey("Company_Id")]
		public virtual Company Company { get; set; }
		[ForeignKey("Team_Id")]
		public virtual Team team { get; set; }
	}
}
