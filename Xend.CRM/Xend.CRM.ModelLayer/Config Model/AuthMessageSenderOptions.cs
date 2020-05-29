using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.Config_Model
{
	public class AuthMessageSenderOptions
	{
		public string SendGridKey { get; set; }
		public string SendGridUser { get; set; }
		public string[] SmsWebhookKeys { get; set; }
	}
}
