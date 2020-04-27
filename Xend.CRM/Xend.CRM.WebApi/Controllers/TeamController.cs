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
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : BaseAPIController
    {
		ITeam _iteam { get; }

		public TeamController(ITeam iteam)
		{
			_iteam = iteam;
		}

		[HttpPost("CreateTeam")]
		public IActionResult CreateTeam([FromBody] TeamViewModel team)
		  {
			try
			{
				if (ModelState.IsValid)
				{

					TeamServiceResponseModel createMethodResponseReciever = _iteam.CreateTeamService(team);

					if (createMethodResponseReciever.code == "001")
					{
						return BadRequest(createMethodResponseReciever.team, createMethodResponseReciever.Message, createMethodResponseReciever.code);
					}
					else if (createMethodResponseReciever.code == "002")
					{
						return Ok(createMethodResponseReciever.team, createMethodResponseReciever.Message, createMethodResponseReciever.code);
					}
					else if (createMethodResponseReciever.code == "005")
					{
						return BadRequest(createMethodResponseReciever.team, createMethodResponseReciever.Message, createMethodResponseReciever.code);
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
		[HttpPut("UpdateTeam")]
		public IActionResult UpdateTeam([FromBody] TeamViewModel team)
		
{
			try
			{
				if (ModelState.IsValid)
				{

					TeamServiceResponseModel updateMethodServiceResponseModel = _iteam.UpdateTeamService(team);

					if (updateMethodServiceResponseModel.code == "001")
					{
						return BadRequest(updateMethodServiceResponseModel.team, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "002")
					{
						return Ok(updateMethodServiceResponseModel.team, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteTeam/{id}")]
		public IActionResult DeleteTeam(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TeamServiceResponseModel deleteResponseReciever = _iteam.DeleteTeamService(id);

					if (deleteResponseReciever.code == "001")
					{
						return BadRequest(deleteResponseReciever.team, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == "002")
					{
						return Ok(deleteResponseReciever.team, deleteResponseReciever.Message, deleteResponseReciever.code);
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

		[HttpGet("GetTeamById/{id}")]
		public IActionResult GetTeamById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TeamServiceResponseModel getAllResponseReciever = _iteam.GetTeamByIdService(id);

					if (getAllResponseReciever.code == "001")
					{
						return BadRequest(getAllResponseReciever.team, getAllResponseReciever.Message, getAllResponseReciever.code);
					}
					else if (getAllResponseReciever.code == "002")
					{
						return Ok(getAllResponseReciever.team, getAllResponseReciever.Message, getAllResponseReciever.code);
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

		[HttpGet("GetAllTeams")]
		public IActionResult GetAllTeam()
		{
			try
			{
				Task<IEnumerable<Team>> getAllResponseReciever = _iteam.GetAllTeamsService();
				var fetchedTeams = getAllResponseReciever.Result;
				return Ok(fetchedTeams, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetTeamsByCompanyId")]
		public IActionResult GetTeamsByCompanyId(Guid id)
		{
			try
			{
				Task<IEnumerable<Team>> getAllResponseReciever = _iteam.GetTeamsByCompanyIdService(id);
				var fetchedTeams = getAllResponseReciever.Result;
				return Ok(fetchedTeams, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetDeletedTeams")]
		public IActionResult GetDeletedTeams()
		{
			try
			{
				Task<IEnumerable<Team>> getAllResponseReciever = _iteam.GetDeletedTeamsService();
				var fetchedTeams = getAllResponseReciever.Result;
				return Ok(fetchedTeams, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
	}
}