using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class CompanyServiceResponseModel
	{
		public Company company = new Company();
		public string Message { get; set; }
	}
}
