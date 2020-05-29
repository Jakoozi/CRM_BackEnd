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
    [Route("api/[controller]")]
    [ApiController]
    public class AuditRailController : BaseAPIController
    {
		IAuditRail _iaudit { get; }
		ResponseCodes responseCode = new ResponseCodes();


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
					if (createMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(createMethodServiceResponseModel.audit, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(createMethodServiceResponseModel.audit, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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

		[HttpPut("UpdateAudit")]
		public IActionResult UpdateAudit([FromBody] AuditViewModel audit)
		 {
			try
			{
				if (ModelState.IsValid)
				{

					AuditServiceResponseModel updateMethodServiceResponseModel = _iaudit.UpdateAuditService(audit);

					if (updateMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(updateMethodServiceResponseModel.audit, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteAudit/{id}")]
		public IActionResult DeleteAudit(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					AuditServiceResponseModel deleteResponseReciever = _iaudit.AuditDeleteService(id);

					if (deleteResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(deleteResponseReciever.audit, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == responseCode.Successful)
					{
						return Ok(deleteResponseReciever.audit, deleteResponseReciever.Message, deleteResponseReciever.code);
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
		
		[HttpGet("GetAuditById/{id}")]
		public IActionResult GetAuditById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					AuditServiceResponseModel getByIdResponseReciever = _iaudit.GetAuditByIdService(id);

					if (getByIdResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(getByIdResponseReciever.audit, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == responseCode.Successful)
					{
						return Ok(getByIdResponseReciever.audit, getByIdResponseReciever.Message, getByIdResponseReciever.code);
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
		[HttpGet("GetAllAudit")]
		public IActionResult GetAllAudit()
		{
			try
			{
				Task<IEnumerable<Audit_Rail>> getAllResponseReciever = _iaudit.GetAllAuditService();
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetAuditByCompanyId")]
		public IActionResult GetAuditByCompanyId(Guid id)
		{
			try
			{
				Task<IEnumerable<Audit_Rail>> getAllResponseReciever = _iaudit.GetAuditByCompanyIdService(id);
				var fetchedUsers = getAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetAuditByUserId")]
		public IActionResult GetAuditByUserId(Guid id)
		{
			try
			{
				Task<IEnumerable<Audit_Rail>> ghetAllResponseReciever = _iaudit.GetAuditByUserIdService(id);
				var fetchedUsers = ghetAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetDeletedAudits")]
		public IActionResult GetDeletedAudits()
		{
			try
			{
				Task<IEnumerable<Audit_Rail>> ghetAllResponseReciever = _iaudit.GetDeletedAuditsService();
				var fetchedUsers = ghetAllResponseReciever.Result;
				return Ok(fetchedUsers, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}