using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class AuditServiceResponseModel
	{
		public Audit_Rail audit = new Audit_Rail();
		public string Message { get; set; }
		public string code { get; set; }
	}
}
