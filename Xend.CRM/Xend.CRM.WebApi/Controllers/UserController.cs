using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseAPIController
    {
		IUser _iuser { get; }
	

		public UserController(IUser iuser)
		{
			_iuser = iuser;
		}

		[HttpPost("CreateUser")]
		public IActionResult CreateUser([FromBody] UserViewModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{

					UserServiceResponseModel createMethodServiceResponseModel = _iuser.CreateUserService(user);

					if (createMethodServiceResponseModel.code == "001")
					{
						return BadRequest(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "002")
					{
						return Ok(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "005")
					{
						return Ok(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", "003");
					}
				}
				return BadRequest(null, "Null Entity", "004");

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpPut("UpdateUser")]
		public IActionResult UpdateCompany([FromBody] UserViewModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{

					UserServiceResponseModel updateMethodServiceResponseModel = _iuser.UpdateUserService(user);

					if (updateMethodServiceResponseModel.code == "001")
					{
						return BadRequest(updateMethodServiceResponseModel.user, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "002")
					{
						return Ok(updateMethodServiceResponseModel.user, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "005")
					{
						return BadRequest(updateMethodServiceResponseModel.user, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", "003");
					}
				}
				return BadRequest(null, "Null Entity", "004");

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}

}