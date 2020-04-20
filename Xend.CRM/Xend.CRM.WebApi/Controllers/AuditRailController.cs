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
    public class AuditRailController : BaseAPIController
    {
		IAuditRail _iaudit { get; }


		public AuditRailController(IAuditRail iaudit)
		{
			_iaudit = iaudit;
		}

		[HttpPost("CreateAudit")]
		public IActionResult CreateAudit([FromBody] AuditViewModel audit)
		{
			try
			{
				if (ModelState.IsValid)
				{

					AuditServiceResponseModel createMethodServiceResponseModel = _iaudit.AuditCreationService(audit);
					if (createMethodServiceResponseModel.code == "002")
					{
						return Ok(createMethodServiceResponseModel.audit, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "005")
					{
						return BadRequest(createMethodServiceResponseModel.audit, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "006")
					{
						return BadRequest(createMethodServiceResponseModel.audit, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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

		[HttpPut("UpdateAudit")]
		public IActionResult UpdateAudit([FromBody] AuditViewModel audit)
		{
			try
			{
				if (ModelState.IsValid)
				{

					AuditServiceResponseModel updateMethodServiceResponseModel = _iaudit.UpdateAuditService(audit);

					if (updateMethodServiceResponseModel.code == "001")
					{
						return BadRequest(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "002")
					{
						return Ok(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "005")
					{
						return BadRequest(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "006")
					{
						return BadRequest(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteAudit/{id}")]
		public IActionResult DeleteAudit(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					AuditServiceResponseModel deleteResponseReciever = _iaudit.AuditDeleteService(id);

					if (deleteResponseReciever.code == "001")
					{
						return BadRequest(deleteResponseReciever.audit, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == "002")
					{
						return Ok(deleteResponseReciever.audit, deleteResponseReciever.Message, deleteResponseReciever.code);
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
		
		[HttpGet("GetAuditById/{id}")]
		public IActionResult GetAuditById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AuditServiceResponseModel getByIdResponseReciever = _iaudit.GetAuditByIdService(id);

					if (getByIdResponseReciever.code == "001")
					{
						return BadRequest(getByIdResponseReciever.audit, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == "002")
					{
						return Ok(getByIdResponseReciever.audit, getByIdResponseReciever.Message, getByIdResponseReciever.code);
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
		[HttpGet("GetAllAudit")]
		public IActionResult GetAllAudit()
		{
			try
			{
				Task<IEnumerable<Audit_Rail>> ghetAllResponseReciever = _iaudit.GetAllAuditService();
				var fetchedUsers = ghetAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		
	}
}