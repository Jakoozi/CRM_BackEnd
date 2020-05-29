using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Response = SendGrid.Response;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
	public interface IEmailService
	{
		Task<Response> SendTicketCreatedEmail(string to, string subject, params object[] messageContents);
		Task<Response> SendTicketReplyEmail(string to, string subject, params object[] messageContents);
	}

}
