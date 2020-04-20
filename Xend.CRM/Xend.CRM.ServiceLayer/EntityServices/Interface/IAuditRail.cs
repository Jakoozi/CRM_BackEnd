using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface IAuditRail
    {
		AuditServiceResponseModel AuditCreationService(AuditViewModel audit);
		AuditServiceResponseModel UpdateAuditService(AuditViewModel audit);
		AuditServiceResponseModel AuditDeleteService(Guid id);
		AuditServiceResponseModel GetAuditByIdService(Guid id);
		Task<IEnumerable<Audit_Rail>> GetAllAuditService();


	}
}
