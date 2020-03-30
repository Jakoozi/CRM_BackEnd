using System;
using Microsoft.AspNetCore.Mvc;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ModelLayer.ResponseModel;

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
                if(ModelState.IsValid)
                {

                    string createResponseReciever = _icompany.CompanyCreationService(company);
              
                    if (createResponseReciever == "Entity Already Exists")
                    {
                        return BadRequest("Entity Already Exists", "001");
                    }
                    else if (createResponseReciever == "Entity Created Successfully")
                    {
                        return Ok("Entity Created Successfully", "002");
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

        [HttpPut("UpdateCompany")]
        public IActionResult UpdateCompany([FromBody] CompanyViewModel company)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string createResponseReciever = _icompany.UpdateCompanyService(company);

                    if (createResponseReciever == "Entity Does Not Exist")
                    {
                        return BadRequest("Entity Does Not Exist", "001");
                    }
                    else if (createResponseReciever == "Entity Updated Successfully")
                    {
                        return Ok("Entity Updated Successfully", "002");
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

        [HttpDelete("DeleteCompany/{id}")]
        public IActionResult DeleteCompany(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    string createResponseReciever = _icompany.DeleteCompanyService(id);

                    if (createResponseReciever == "Entity Does Not Exist")
                    {
                        return BadRequest("Entity Does Not Exist", "001");
                    }
                    else if (createResponseReciever == "Entity Deleted Successfully")
                    {
                        return Ok("Entity Deleted Successfully", "002");
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

        [HttpGet("GetCompanyById/{id}")]
        public IActionResult GetByCompanyId(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    CompanyViewModel createResponseReciever = _icompany.GetCompanyByIdService(id);

                    if (createResponseReciever == null)
                    {
                        return BadRequest("Entity Does Not Exist", "001");
                    }
                    else if (createResponseReciever != null)
                    {
                        return Ok(createResponseReciever, "Entity Fetched Successfully", "002");
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