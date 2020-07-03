using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class DashboardResponseModel
	{
		public DashboardViewModel dashboardViewModel = new DashboardViewModel();
		public string Message { get; set; }
		public string code { get; set; }
	}
}
