using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ModelLayer.ViewModels.Put_View_Models;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
	public interface IDashboard
	{
		Task<DashboardResponseModel> GetDashboardValuesService(Guid id);
	}
}
