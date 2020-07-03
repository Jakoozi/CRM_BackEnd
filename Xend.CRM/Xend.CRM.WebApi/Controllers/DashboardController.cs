using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.WebApi.Controllers
{
    [Route("api/Dashboard")]
    [ApiController]
    public class DashboardController : BaseAPIController
	{
		//maxwells demon
		ResponseCodes responseCode = new ResponseCodes();
		IDashboard _iDashboard;

		public DashboardController(IDashboard iDashboard)
		{
			_iDashboard = iDashboard;
		}

		[HttpGet("GetDashboardValuesbyCompanyId")]
		public async Task<IActionResult> GetDashboardValuesbyCompanyId( Guid id)
		{
			try
			{
				DashboardResponseModel dashboardResponseReciever = await _iDashboard.GetDashboardValuesService(id);
				return Ok(dashboardResponseReciever.dashboardViewModel, dashboardResponseReciever.Message, dashboardResponseReciever.code);
			}
			catch(Exception exe)
			{
				return BadRequest(exe);
			}
		}
	}
}