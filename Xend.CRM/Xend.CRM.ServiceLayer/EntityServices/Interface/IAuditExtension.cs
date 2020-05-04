using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.ServiceExtentions
{
	public interface IAuditExtension
	{
		void Auditlogger(Guid company_Id, Guid user_Id, string activity);
	}
}