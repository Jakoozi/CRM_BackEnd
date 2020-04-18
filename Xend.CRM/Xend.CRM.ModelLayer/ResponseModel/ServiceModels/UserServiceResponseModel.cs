using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class UserServiceResponseModel
	{
			public User company = new User();
			public string Message { get; set; }
			public string code { get; set; }
	}
}
