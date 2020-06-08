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
using Xend.CRM.ModelLayer.ViewModels.Put_View_Models;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.WebApi.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : BaseAPIController
    {
		IUser _iuser { get; }
		ResponseCodes responseCode = new ResponseCodes();


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

					if (createMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(createMethodServiceResponseModel.user, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", responseCode.ErrorOccured);
					}
				}
				return BadRequest(null, "Null Entity", responseCode.ErrorOccured);

			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpPut("UpdateUser")]
		public IActionResult UpdateUser([FromBody] UpdateUserViewModel user)
		{
			try
			{
				if (ModelState.IsValid)
				{

					UserServiceResponseModel updateMethodServiceResponseModel = _iuser.UpdateUserService(user);

					if (updateMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(updateMethodServiceResponseModel.user, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(updateMethodServiceResponseModel.user, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", responseCode.ErrorOccured);
					}
				}
				return BadRequest(null, "Null Entity", responseCode.ErrorOccured);

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

					if (deleteResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(deleteResponseReciever.user, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == responseCode.Successful)
					{
						return Ok(deleteResponseReciever.user, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else
					{
						return BadRequest(null, "Error Occured", responseCode.ErrorOccured);
					}
				}
				return BadRequest(null, "Null Entity", responseCode.ErrorOccured);

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
				Task<IEnumerable<User>> getAllResponseReciever = _iuser.GetAllUsersService();
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
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

					if (getByIdResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(getByIdResponseReciever.user, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == responseCode.Successful)
					{
						return Ok(getByIdResponseReciever.user, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else
					{
						return BadRequest("Error Occured", responseCode.ErrorOccured);
					}
				}
				return BadRequest("Null Entity", responseCode.ErrorOccured);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetUsersByCompanyId")]
		public IActionResult GetUsersByCompanyId(Guid id)
		{
			try
			{
				Task<IEnumerable<User>> getAllResponseReciever = _iuser.GetUsersByCompanyIdService(id);
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpGet("GetUsersByRole")]
		public IActionResult GetUsersByRole(User_Role role)
		{
			try
			{
				Task<IEnumerable<User>> getAllResponseReciever = _iuser.GetUsersByRoleService(role);
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetDeletedUsers")]
		public IActionResult GetDeletedUsers()
		{
			try
			{
				Task<IEnumerable<User>> getAllResponseReciever = _iuser.GetDeletedUsersService();
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}

}