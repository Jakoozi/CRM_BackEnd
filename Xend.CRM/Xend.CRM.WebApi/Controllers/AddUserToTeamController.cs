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
    [Route("api/UserTeam")]
    [ApiController]
    public class AddUserToTeamController : BaseAPIController
	{
		IAddUserToTeam _iaddUserToTeam;
		ResponseCodes responseCode = new ResponseCodes();

		public AddUserToTeamController(IAddUserToTeam iaddUserToTeam)
		{
			_iaddUserToTeam = iaddUserToTeam;
		}

		[HttpPost("AddUserToTeam")]
		public IActionResult AddUserToTeam([FromBody] UserTeamViewModel userTeam)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AddUserToTeamResponseModel createMethodServiceResponseModel = _iaddUserToTeam.AddUserToATeamService(userTeam);
					if (createMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(createMethodServiceResponseModel.userTeam, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(createMethodServiceResponseModel.userTeam, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteUserFromTeam")]
		public IActionResult DeleteUserFromTeam(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AddUserToTeamResponseModel deleteMethodServiceResponseModel = _iaddUserToTeam.DeleteUserService(id);
					if (deleteMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(deleteMethodServiceResponseModel.userTeam, deleteMethodServiceResponseModel.Message, deleteMethodServiceResponseModel.code);
					}
					else if (deleteMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(deleteMethodServiceResponseModel.userTeam, deleteMethodServiceResponseModel.Message, deleteMethodServiceResponseModel.code);
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

		[HttpGet("GetAllTheUsersTeam")]
		public IActionResult GetAllTheUsersTeam(Guid id)
		{
			try
			{
				Task<IEnumerable<UserTeam>> getAllResponseReciever = _iaddUserToTeam.GetAllTheUsersTeamService(id);
				var fetchedUsersTeam = getAllResponseReciever.Result;
				return Ok(fetchedUsersTeam, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}