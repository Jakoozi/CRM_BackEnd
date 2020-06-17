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
	public class TicketController : BaseAPIController
	{
		ITicket _iticket { get; }
		ResponseCodes responseCode = new ResponseCodes();

		public TicketController(ITicket iticket)
		{
			_iticket = iticket;
		}

		[HttpPost("CreateTicket")]
		public async Task<IActionResult> CreateTicket([FromBody] TicketViewModel ticket)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TicketServiceResponseModel createMethodResponseReciever = await _iticket.CreateTicketService(ticket);

					if (createMethodResponseReciever.code == responseCode.Successful)
					{
						return Ok(createMethodResponseReciever.ticket, createMethodResponseReciever.Message, createMethodResponseReciever.code);
					}
					else if (createMethodResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(createMethodResponseReciever.ticket, createMethodResponseReciever.Message, createMethodResponseReciever.code);
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

		//changed from put to post method because of the server error i was experiencing
		[HttpPost("ResolveTicket")]
		public async Task<IActionResult> ResolveTicket([FromBody] TicketViewModel ticket)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TicketServiceResponseModel updateMethodServiceResponseModel = await _iticket.ResolveTicketService(ticket);

					if (updateMethodServiceResponseModel.code == responseCode.ErrorOccured)
					{
						return BadRequest(updateMethodServiceResponseModel.ticket, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
					}
					else if (updateMethodServiceResponseModel.code == responseCode.Successful)
					{
						return Ok(updateMethodServiceResponseModel.ticket, updateMethodServiceResponseModel.Message, updateMethodServiceResponseModel.code);
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
		//changed from put to post method because of the server error i was experiencing
		[HttpPost("CloseTicket/{id}")]
		public IActionResult CloseTicket(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TicketServiceResponseModel closeResponseReciever = _iticket.CloseTicketService(id);

					if (closeResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(closeResponseReciever.ticket, closeResponseReciever.Message, closeResponseReciever.code);
					}
					else if (closeResponseReciever.code == responseCode.Successful)
					{
						return Ok(closeResponseReciever.ticket, closeResponseReciever.Message, closeResponseReciever.code);
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

		[HttpDelete("DeleteTicket/{id}")]
		public IActionResult DeleteTicket(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TicketServiceResponseModel deleteResponseReciever = _iticket.DeleteTicketService(id);

					if (deleteResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(deleteResponseReciever.ticket, deleteResponseReciever.Message, deleteResponseReciever.code);
					}
					else if (deleteResponseReciever.code == responseCode.Successful)
					{
						return Ok(deleteResponseReciever.ticket, deleteResponseReciever.Message, deleteResponseReciever.code);
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
		[HttpGet("GetTicketById/{id}")]
		public IActionResult GetTicketById(Guid id)
		{
			try
			{
				if (ModelState.IsValid)
				{
					TicketServiceResponseModel getByIdResponseReciever = _iticket.GetTicketByIdService(id);

					if (getByIdResponseReciever.code == responseCode.ErrorOccured)
					{
						return BadRequest(getByIdResponseReciever.ticket, getByIdResponseReciever.Message, getByIdResponseReciever.code);
					}
					else if (getByIdResponseReciever.code == responseCode.Successful)
					{
						return Ok(getByIdResponseReciever.ticket, getByIdResponseReciever.Message, getByIdResponseReciever.code);
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
		[HttpGet("GetAllTickets")]
		public IActionResult GetAllTickets()
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetAllTicketsService();
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}

		[HttpGet("GetDeletedTickets")]
		public IActionResult GetDeletedTickets()
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetDeletedTicketsService();
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetTicketByCompany_Id")]
		public IActionResult GetTicketByCompany_Id(Guid id)
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetTicketByCompany_IdService(id);
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetTicketByCustomer_Id")]
		public IActionResult GetTicketByCustomer_Id(Guid id)
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetTicketByCustomer_IdService(id);
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
		[HttpGet("GetTicketByCreated_UserId")]
		public IActionResult GetTicketByCreated_UserId(Guid id)
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetTicketByCreated_UserIdService(id);
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}

		}
		[HttpGet("GetTicketByResolved_UserId")]
		public IActionResult GetTicketByResolved_UserId(Guid id)
		{
			try
			{
				Task<IEnumerable<Ticket>> ghetAllResponseReciever = _iticket.GetTicketByResolved_UserIdService(id);
				var fetchedTckets = ghetAllResponseReciever.Result;
				return Ok(fetchedTckets, "Successful", responseCode.Successful);
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}


	}
}