using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

		public TicketController(ITicket iticket)
		{
			_iticket = iticket;
		}

		[HttpPost("CreateTicket")]
		public IActionResult CreateTicket([FromBody] TicketViewModel ticket)
		{
			try
			{
				if (ModelState.IsValid)
				{

					TicketServiceResponseModel createMethodResponseReciever = _iticket.CreateTicketService(ticket);

					if (createMethodResponseReciever.code == "002")
					{
						return Ok(createMethodResponseReciever.ticket, createMethodResponseReciever.Message, createMethodResponseReciever.code);
					}
					else if (createMethodResponseReciever.code == "005")
					{
						return BadRequest(createMethodResponseReciever.ticket, createMethodResponseReciever.Message, createMethodResponseReciever.code);
					}
					else if (createMethodResponseReciever.code == "006")
					{
						return BadRequest(createMethodResponseReciever.ticket, createMethodResponseReciever.Message, createMethodResponseReciever.code);
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
	}
}