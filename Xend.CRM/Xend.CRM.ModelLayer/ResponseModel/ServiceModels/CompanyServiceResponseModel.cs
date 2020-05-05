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
		public string code { get; set; }
	}
	//public class CompanyListServiceResponseModel
	//{

	//	public List<Company> companies = new List<Company>();
	//	public string Message { get; set; }
	//	public string code { get; set; }
	//}
}
