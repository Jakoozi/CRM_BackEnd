using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels.Put_View_Models
{
	public class UpdateUserViewModel
	{
		public Guid Id { get; set; }
		public Guid? Company_Id { get; set; }
		public string Company_Name { get; set; }
		public string First_Name { get; set; }
		public string Last_Name { get; set; }
		public string Phonenumber { get; set; }
		public string Email { get; set; }
		public string XendCode { get; set; }
		public User_Role User_Role { get; set; }
		public string User_Password { get; set; }
	}
}
