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

					if (createResponseReciever.code == "001")
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == "002")
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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

		[HttpPut("UpdateCompany")]
		public IActionResult UpdateCompany([FromBody] CompanyViewModel company)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.UpdateCompanyService(company);

					if (createResponseReciever.code == "001")
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == "002")
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == "005")
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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

		[HttpDelete("DeleteCompany/{id}")]
		public IActionResult DeleteCompany(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.DeleteCompanyService(id);

					if (createResponseReciever.code == "001")
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == "002")
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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

		[HttpGet("GetCompanyById/{id}")]
		//[HttpGet("{id}")]
		public IActionResult GetCompanyById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CompanyServiceResponseModel createResponseReciever = _icompany.GetCompanyByIdService(id);

					if (createResponseReciever.code == "001")
					{
						return BadRequest(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
					}
					else if (createResponseReciever.code == "002")
					{
						return Ok(createResponseReciever.company, createResponseReciever.Message, createResponseReciever.code);
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
		[HttpGet("GetAllCompaniesService")]
		public IActionResult GetAllCompanies()
		{
			Task<IEnumerable<Company>> createResponseReciever = _icompany.GetAllCompaniesService();
			var fetchedCompanies = createResponseReciever.Result;
			return Ok(fetchedCompanies, "Successful", "002");
		}




	}
}