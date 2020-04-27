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
    public class CustomerController : BaseAPIController
    {
		ICustomer _icustomer { get; }


		public CustomerController(ICustomer icustomer)
		{
			_icustomer = icustomer;
		}

		[HttpPost("CreateCustomer")]
		public IActionResult CreateCustomer([FromBody] CustomerViewModel customer)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CustomerServiceResponseModel createMethodServiceResponseModel = _icustomer.CreateCustomerService(customer);

					if (createMethodServiceResponseModel.code == "001")
					{
						return BadRequest(createMethodServiceResponseModel.customer, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "002")
					{
						return Ok(createMethodServiceResponseModel.customer, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
					}
					else if (createMethodServiceResponseModel.code == "005")
					{
						return BadRequest(createMethodServiceResponseModel.customer, createMethodServiceResponseModel.Message, createMethodServiceResponseModel.code);
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

		[HttpPut("UpdateCustomer")]
		public IActionResult UpdateCustomer([FromBody] CustomerViewModel customer)
		{
			try
			{
				if (ModelState.IsValid)
				{

					CustomerServiceResponseModel updateMethodServiceResponseModel = _icustomer.UpdateCustomerService(customer);

					if (updateMethodServiceResponseModel.code == "001")
					{
						return BadRequest(updateMethodServiceResponseModel.customer, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "002")
					{
						return Ok(updateMethodServiceResponseModel.customer, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == "005")
					{
						return BadRequest(updateMethodServiceResponseModel.customer, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
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

		[HttpDelete("DeleteCustomer/{id}")]
		public IActionResult DeleteCustomer(Guid id)
		
{
			try
			{
				if (ModelState.IsValid)
				{

					CustomerServiceResponseModel deleteResponseReciever = _icustomer.DeleteCustomerService(id);

					if (deleteResponseReciever.code == "001")
					{
						return BadRequest(deleteResponseReciever.customer, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == "002")
					{
						return Ok(deleteResponseReciever.customer, deleteResponseReciever.Message, deleteResponseReciever.code);
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

		[HttpGet("GetCustomerById/{id}")]
		public IActionResult GetCustomerById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					CustomerServiceResponseModel getByIdResponseReciever = _icustomer.GetCustomerByIdService(id);

					if (getByIdResponseReciever.code == "001")
					{
						return BadRequest(getByIdResponseReciever.customer, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == "002")
					{
						return Ok(getByIdResponseReciever.customer, getByIdResponseReciever.Message, getByIdResponseReciever.code);
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

		[HttpGet("GetAllCustomerssService")]
		public IActionResult GetAllCustomers()
		{
			try
			{
				Task<IEnumerable<Customer>> ghetAllResponseReciever = _icustomer.GetAllCustomerService();
				var fetchedCustomers = ghetAllResponseReciever.Result;
				return Ok(fetchedCustomers, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetCustomerByCompanyId")]
		public IActionResult GetCustomerByCompanyId(Guid id)
		{
			try
			{
				Task<IEnumerable<Customer>> ghetAllResponseReciever = _icustomer.GetCustomerByCompanyIdService(id);
				var fetchedCustomers = ghetAllResponseReciever.Result;
				return Ok(fetchedCustomers, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetDeletedCustomer")]
		public IActionResult GetDeletedCustomer()
		{
			try
			{
				Task<IEnumerable<Customer>> ghetAllResponseReciever = _icustomer.GetDeletedCustomerService();
				var fetchedCustomers = ghetAllResponseReciever.Result;
				return Ok(fetchedCustomers, "Successful", "002");
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}