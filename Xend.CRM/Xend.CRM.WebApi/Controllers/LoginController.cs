using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.WebApi.Controllers
{
	[Route("api/User")]
	[ApiController]
	public class LoginController : BaseAPIController
	{
		
		ILogin _ilogin { get; }
		ResponseCodes responseCode = new ResponseCodes();


		public LoginController( ILogin ilogin)
		{

			_ilogin = ilogin;
		}

		[HttpPost("AgentLogin")]
		public IActionResult AgentLogin([FromBody] UserViewModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UserServiceResponseModel loginResponse = _ilogin.AdminLogin(user);
					if (loginResponse.code == responseCode.ErrorOccured)
					{
						return BadRequest(loginResponse.user, loginResponse.Message, loginResponse.code);
					}
					else if (loginResponse.code == responseCode.Successful)
					{
						return Ok(loginResponse.user, loginResponse.Message, loginResponse.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", responseCode.ErrorOccured);
					}
				}
				else
				{
					return BadRequest(null, "Null Entity", responseCode.ErrorOccured);
				}

			}
			catch (Exception exe)
			{
				return BadRequest(exe);
			}
		}
	}

}