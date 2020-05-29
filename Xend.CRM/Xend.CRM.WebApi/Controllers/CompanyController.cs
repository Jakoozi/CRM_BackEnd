using System;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using System.Collections.Generic;
using Xend.CRM.ModelLayer.Entities;
using System.Threading.Tasks;

namespace Xend.CRM.WebApi.Controllers
{
	[Route("api/Company")]
	[ApiController]
	//meant to inherit from BaseController
	public class CompanyController : BaseAPIController
	{
		ICompany _icompany { get; }
		ResponseCodes responseCode = new ResponseCodes();

		public CompanyController(ICompany icompany)
		{
			_icompany = icompany;
		}

		[HttpPost("CreateCompany")]
		public IActionResult CreateCompany([FromBody] CompanyViewModel company)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.CompanyCreationService(company);

					if (createResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == responseCode.Successful)
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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

		[HttpPut("UpdateCompany")]
		public IActionResult UpdateCompany([FromBody] CompanyViewModel company)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createMethodServiceResponseModel = _icompany.UpdateCompanyService(company);

					if (createMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(createMethodServiceResponseModel.company, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(createMethodServiceResponseModel.company, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteCompany/{id}")]
		public IActionResult DeleteCompany(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.DeleteCompanyService(id);

					if (createResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == responseCode.Successful)
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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

		[HttpGet("GetCompanyById/{id}")]
		public IActionResult GetCompanyById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.GetCompanyByIdService(id);

					if (createResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == responseCode.Successful)
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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
		[HttpGet("GetAllCompaniesService")]
		public IActionResult GetAllCompanies()
		{	
			try
			{
				Task<IEnumerable<Company>> createResponseReciever = _icompany.GetAllCompaniesService();
				var fetchedCompanies = createResponseReciever.Result;
				return Ok(fetchedCompanies, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetDeletedCompanies")]
		public IActionResult GetDeletedCompanies()
		{
			try
			{
				Task<IEnumerable<Company>> createResponseReciever = _icompany.GetDeletedCompaniesService();
				var fetchedCompanies = createResponseReciever.Result;
				return Ok(fetchedCompanies, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}




	}
}