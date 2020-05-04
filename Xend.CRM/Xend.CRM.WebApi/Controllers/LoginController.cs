using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
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
					UserServiceResponseModel loginResponse = _ilogin.AgentLogin(user);
					if (loginResponse.code == "001")
					{
						return BadRequest(loginResponse.user, loginResponse.Message, loginResponse.code);
					}
					else if (loginResponse.code == "002")
					{
						return Ok(loginResponse.user, loginResponse.Message, loginResponse.code);
					}
					else if (loginResponse.code == "005")
					{
						return BadRequest(loginResponse.user, loginResponse.Message, loginResponse.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", "003");
					}
				}
				else
				{
					return BadRequest(null, "Null Entity", "004");
				}

			}
			catch (Exception exe)
			{
				return BadRequest(exe);
			}
		}
	}

}