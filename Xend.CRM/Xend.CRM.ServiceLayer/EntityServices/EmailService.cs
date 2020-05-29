using MassTransit;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Config_Model;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using MimeKit;

namespace Xend.CRM.ServiceLayer.EntityServices
{
	public class EmailService : IEmailService
	{
		public IHostingEnvironment HostingEnvironment { get; set; }
		public AuthMessageSenderOptions AuthMessageSenderOptions { get; set; }

		public EmailService(IHostingEnvironment hostingEnvironment, AuthMessageSenderOptions authMessageSenderOptions)
		{
			HostingEnvironment = hostingEnvironment;
			AuthMessageSenderOptions = authMessageSenderOptions;
		}

		public async Task<SendGrid.Response> SendTicketCreatedEmail(string to, string subject, params object[] messageContents)
		{
			try
			{
				subject = PrepareSubjectBasedOnEnvironment(HostingEnvironment, subject);
				var builder = new BodyBuilder();

				string pathToEmailFile = $"{HostingEnvironment.WebRootPath}/EmailTemplate/CreatedTicketTemplate.html";

				using (StreamReader SourceReader = File.OpenText(pathToEmailFile))
				{
					builder.HtmlBody = SourceReader.ReadToEnd();
				}

				string messageBody = builder.HtmlBody;
				for (int i = 0; i < messageContents.Length; i++)
				{
					messageBody = messageBody.Replace("{{" + i + "}}", (string)messageContents[i]);
				}

				var client = new SendGridClient(AuthMessageSenderOptions.SendGridKey);
				var msg = new SendGridMessage()
				{
					From = new EmailAddress("no-reply@Xend.ng", "Xend Team"),
					Subject = subject,
					PlainTextContent = messageBody,
					HtmlContent = messageBody
				};
				msg.AddTo(new EmailAddress(to));

				// Disable click tracking.
				// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
				msg.SetClickTracking(false, false);

				var response = await client.SendEmailAsync(msg);

				var statusCode = response.StatusCode;
				var content = await response.DeserializeResponseBodyAsync(response.Body);
				return response;

			}
			catch (Exception ex)
			{
				return null;
			}
		}

		public async Task<SendGrid.Response> SendTicketReplyEmail(string to, string subject, params object[] messageContents)
		{
			try
			{
				subject = PrepareSubjectBasedOnEnvironment(HostingEnvironment, subject);
				var builder = new BodyBuilder();

				string pathToEmailFile = $"{HostingEnvironment.WebRootPath}/EmailTemplate/TicketReplyTemplate.html";

				using (StreamReader SourceReader = File.OpenText(pathToEmailFile))
				{
					builder.HtmlBody = SourceReader.ReadToEnd();
				}

				string messageBody = builder.HtmlBody;
				for (int i = 0; i < messageContents.Length; i++)
				{
					messageBody = messageBody.Replace("{{" + i + "}}", (string)messageContents[i]);
				}

				var client = new SendGridClient(AuthMessageSenderOptions.SendGridKey);
				var msg = new SendGridMessage()
				{
					From = new EmailAddress("no-reply@Xend.ng", "Xend Team"),
					Subject = subject,
					PlainTextContent = messageBody,
					HtmlContent = messageBody
				};
				msg.AddTo(new EmailAddress(to));

				// Disable click tracking.
				// See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
				msg.SetClickTracking(false, false);

				var response = await client.SendEmailAsync(msg);

				var statusCode = response.StatusCode;
				var content = await response.DeserializeResponseBodyAsync(response.Body);
				return response;

			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public string PrepareSubjectBasedOnEnvironment(IHostingEnvironment hostingEnvironment, string subject)
		{
			if (HostingEnvironment.IsDevelopment() || HostingEnvironment.IsStaging())
			{
				//subject = "[TEST MODE] " + subject;
				subject =  subject;
			}
			return subject;
		}
	}


}
