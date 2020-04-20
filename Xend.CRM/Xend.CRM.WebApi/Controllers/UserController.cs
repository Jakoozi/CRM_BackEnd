using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.Entities;
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
						return BadRequest(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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
		public IActionResult UpdateUser([FromBody] UserViewModel user)
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

		[HttpDelete("DeleteUser/{id}")]
		public IActionResult DeleteUser(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					UserServiceResponseModel deleteResponseReciever = _iuser.DeleteUserService(id);

					if (deleteResponseReciever.code == "001")
					{
						return BadRequest(deleteResponseReciever.user, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == "002")
					{
						return Ok(deleteResponseReciever.user, deleteResponseReciever.Message, deleteResponseReciever.code);
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
		[HttpGet("GetAllUsersService")]
		public IActionResult GetAllUsers()
		{
			try
			{
				Task<IEnumerable<User>> ghetAllResponseReciever = _iuser.GetAllUsersService();
				var fetchedUsers = ghetAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetUserById/{id}")]
		public IActionResult GetUserById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					UserServiceResponseModel getByIdResponseReciever = _iuser.GetUserByIdService(id);

					if (getByIdResponseReciever.code == "001")
					{
						return BadRequest(getByIdResponseReciever.user, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == "002")
					{
						return Ok(getByIdResponseReciever.user, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else
					{
						return BadRequest("Error Occured", "003");
					}
				}
				return BadRequest("Null Entity", "004");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}

}