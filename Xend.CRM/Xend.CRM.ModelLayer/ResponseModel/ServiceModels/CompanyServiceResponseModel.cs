using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class CompanyServiceResponseModel
	{
		CompanyViewModel company = new CompanyViewModel();
		string Message { get; set; }
	}
}
