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

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class AuditRailServices : BaseService, IAuditRail
    {
		ILoggerManager _loggerManager { get; }
		AuditServiceResponseModel auditModel;
		public AuditRailServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}
		//this service adds logs to the audit rail
		public AuditServiceResponseModel AuditCreationService(AuditViewModel audit)
		{
			try
			{
				//unit of work is used to replace _context.
					Audit_Rail auditToBeCreated;

					Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == audit.Company_Id && p.Status == EntityStatus.Active);
					if (checkIfCompanyExists != null)
					{
						User checkIfUserExists = UnitOfWork.GetRepository<User>().Single(p => p.Id == audit.User_Id && p.Status == EntityStatus.Active);
						if(checkIfUserExists != null)
						{
							auditToBeCreated = new Audit_Rail
							{
								Company_Id = audit.Company_Id,
								User_Id = audit.User_Id,
								Activity = audit.Activity,
								Status = EntityStatus.Active,
								CreatedAt = DateTime.Now,
								CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
								UpdatedAt = DateTime.Now,
								UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
							};
							UnitOfWork.GetRepository<Audit_Rail>().Add(auditToBeCreated);
							UnitOfWork.SaveChanges();

							auditModel = new AuditServiceResponseModel() { audit = auditToBeCreated, Message = "Entity Created Successfully", code = "002" };
							return auditModel;
						}
						else
						{
							auditModel = new AuditServiceResponseModel() { audit = null, Message = "User Does Not Exist", code = "006" };
							return auditModel;
						}
					}
					else
					{
						auditModel = new AuditServiceResponseModel() { audit = null, Message = "Company Does Not Exist", code = "005" };
						return auditModel;
					}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service updates logs 
		public AuditServiceResponseModel UpdateAuditService(AuditViewModel audit)
		{

			try
			{
				Audit_Rail toBeUpdatedAudit = UnitOfWork.GetRepository<Audit_Rail>().Single(p => p.Id == audit.Id);
				if (toBeUpdatedAudit == null)
				{
					auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
					return auditModel;
				}
				else
				{
					if (toBeUpdatedAudit.Status == EntityStatus.Active)
					{
						Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == audit.Company_Id && p.Status == EntityStatus.Active);
						if (checkIfCompanyExists != null)
						{
							User checkIfUserExists = UnitOfWork.GetRepository<User>().Single(p => p.Id == audit.User_Id && p.Status == EntityStatus.Active);
							if (checkIfCompanyExists != null)
							{
								//here i will assign directly what i want to update to the model instead of creating a new instance
								//toBeUpdatedUser.Company_Id = user.Company_Id;

								toBeUpdatedAudit.Activity = audit.Activity;
								toBeUpdatedAudit.Status = EntityStatus.Active;
								toBeUpdatedAudit.UpdatedAt = DateTime.Now;
								toBeUpdatedAudit.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
								UnitOfWork.GetRepository<Audit_Rail>().Update(toBeUpdatedAudit); ;
								UnitOfWork.SaveChanges();

								auditModel = new AuditServiceResponseModel() { audit = toBeUpdatedAudit, Message = "Entity Updated Successfully", code = "002" };
								return auditModel;
							}
							else
							{
								auditModel = new AuditServiceResponseModel() { audit = null, Message = "user Do Not Exist", code = "006" };
								return auditModel;
							}
						}
						else
						{
							auditModel = new AuditServiceResponseModel() { audit = null, Message = "Company Do Not Exist", code = "005" };
							return auditModel;
						}
					}
					else
					{
						auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
						return auditModel;
					}

				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		//this service deletes logs
		public AuditServiceResponseModel AuditDeleteService(Guid id)
		{
			try
			{

				Audit_Rail audit = UnitOfWork.GetRepository<Audit_Rail>().Single(p => p.Id == id);
				if (audit == null)
				{
					auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
					return auditModel;
				}
				else
				{
					if (audit.Status == EntityStatus.Active)
					{
						audit.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Audit_Rail>().Update(audit);
						UnitOfWork.SaveChanges();

						auditModel = new AuditServiceResponseModel() { audit = audit, Message = "Entity Deleted Successfully", code = "002" };
						return auditModel;
					}
					else
					{
						auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
						return auditModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service fetches logs by there id
		public AuditServiceResponseModel GetAuditByIdService(Guid id)
		{
			try
			{
				Audit_Rail audit = UnitOfWork.GetRepository<Audit_Rail>().Single(p => p.Id == id);

				//since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
				//CompanyViewModel companyViewModel = new CompanyViewModel
				//{
				//    Company_Name = company.Company_Name,
				//    Id = company.Id
				//};

				if (audit != null)
				{
					if (audit.Status == EntityStatus.Active)
					{
						auditModel = new AuditServiceResponseModel() { audit = audit, Message = "Entity Fetched Successfully", code = "002" };
						return auditModel;
					}
					else
					{
						auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
						return auditModel;
					}
				}
				auditModel = new AuditServiceResponseModel() { audit = null, Message = "Entity Does Not Exist", code = "001" };
				return auditModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
	
		//this service fetches all logs
		public async Task<IEnumerable<Audit_Rail>> GetAllAuditService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Audit_Rail> audits = await UnitOfWork.GetRepository<Audit_Rail>().GetListAsync(t => t.Status == EntityStatus.Active);
				return audits;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}

		//this service geets audits by there company id's
		public async Task<IEnumerable<Audit_Rail>> GetAuditByCompanyIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Audit_Rail> audits = await UnitOfWork.GetRepository<Audit_Rail>().GetListAsync(t =>t.Company_Id == id && t.Status == EntityStatus.Active);
				return audits;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}

		//this method Gets Audits By User Id
		public async Task<IEnumerable<Audit_Rail>> GetAuditByUserIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Audit_Rail> audits = await UnitOfWork.GetRepository<Audit_Rail>().GetListAsync(t => t.User_Id == id && t.Status == EntityStatus.Active);
				return audits;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this method fetches deleted audits
		public async Task<IEnumerable<Audit_Rail>> GetDeletedAuditsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Audit_Rail> audits = await UnitOfWork.GetRepository<Audit_Rail>().GetListAsync(t => t.Status == EntityStatus.InActive);
				return audits;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
	}
}
