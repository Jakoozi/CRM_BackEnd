using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ModelExtensions;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ServiceLayer.ServiceExtentions;


namespace Xend.CRM.ServiceLayer.ServiceExtentions
{
	public class AuditServiceExtension : BaseService, IAuditExtension
	{
		ILoggerManager _loggerManager { get; }
		IAuditRail _iaudit { get; }
		public AuditServiceExtension(IUnitOfWork<XendDbContext> unitOfWork, IAuditRail iaudit, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iaudit = iaudit;
		}

		public void Auditlogger(Guid company_Id, Guid user_Id, string activity)
		{
			AuditViewModel auditToBeCreated = new AuditViewModel
			{
				Company_Id = company_Id,
				User_Id = user_Id,
				Activity = activity,
				Status = EntityStatus.Active,
				CreatedAt = DateTime.Now,
				CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
				UpdatedAt = DateTime.Now,
				UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
			};

			_iaudit.AuditCreationService(auditToBeCreated);
		
		}
	}
}
