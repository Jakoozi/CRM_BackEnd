using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using AutoMapper;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ModelExtensions;
using Xend.CRM.ServiceLayer.ServiceExtentions;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ViewModels.Put_View_Models;

namespace Xend.CRM.ServiceLayer.EntityServices
{
	
	public class UserServices : BaseService, IUser
	{

		ILoggerManager _loggerManager { get; }
		IAuditExtension _iauditExtension { get; }
		UserServiceResponseModel userModel;
		ResponseCodes responseCode = new ResponseCodes();
		public UserServices(IUnitOfWork<XendDbContext> unitOfWork, IAuditExtension iauditExtention, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iauditExtension = iauditExtention;
		}

		//this service creates new users
		public UserServiceResponseModel CreateUserService(UserViewModel user)
        {
			try
			{
				//unit of work is used to replace _context.

				User userToBeCreated = UnitOfWork.GetRepository<User>().Single(p => p.Email == user.Email || p.Phonenumber == user.Phonenumber || p.XendCode == user.XendCode);
				if(userToBeCreated != null)
				{
					userModel = new UserServiceResponseModel() {user = null, Message = "Entity Already Exists", code = responseCode.ErrorOccured };
					return userModel;
				}
				else
				{
					Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == user.Company_Id && p.Status == EntityStatus.Active);
					//if(checkIfCompanyExists != null)
					//{
						userToBeCreated = new User()
						{
							Company_Id = user.Company_Id,
							Company_Name = checkIfCompanyExists.Company_Name,
							User_Password = user.User_Password,
							First_Name = user.First_Name,
							Last_Name = user.Last_Name,
							Email = user.Email,
							Phonenumber = user.Phonenumber,
							User_Role = user.User_Role,
							XendCode = user.XendCode,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
						};
						UnitOfWork.GetRepository<User>().Add(userToBeCreated);
						UnitOfWork.SaveChanges();

						//Audit Logger
						var converte_Company_id = userToBeCreated.Company_Id.GetValueOrDefault();
						_iauditExtension.Auditlogger(converte_Company_id, userToBeCreated.Id, "You Created A User");

						userModel = new UserServiceResponseModel() { user = null, Message = "Entity Created Successfully", code = responseCode.Successful };
						return userModel;
					//}
					//else
					//{
					//	userModel = new UserServiceResponseModel() { user = userToBeCreated, Message = "Company Do Not Exist", code = responseCode.ErrorOccured };
					//	return userModel;
					//}
					
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service updates user
		public UserServiceResponseModel UpdateUserService(UpdateUserViewModel user)
		{

			try
			{
				User toBeUpdatedUser = UnitOfWork.GetRepository<User>().Single(p => p.Id == user.Id);
				if (toBeUpdatedUser == null)
				{
					userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return userModel;
				}
				else
				{
					if(toBeUpdatedUser.Status == EntityStatus.Active)
					{
						Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == user.Company_Id && p.Status == EntityStatus.Active);
						if (checkIfCompanyExists != null)
						{

							//here i will assign directly what i want to update to the model instead of creating a new instance
							toBeUpdatedUser.Company_Id = user.Company_Id;
							toBeUpdatedUser.Company_Name = checkIfCompanyExists.Company_Name;
							toBeUpdatedUser.User_Password = user.User_Password;
							toBeUpdatedUser.First_Name = user.First_Name;
							toBeUpdatedUser.Last_Name = user.Last_Name;
							toBeUpdatedUser.Email = user.Email;
							toBeUpdatedUser.Phonenumber = user.Phonenumber;
							toBeUpdatedUser.User_Role = user.User_Role;
							toBeUpdatedUser.XendCode = user.XendCode;
							toBeUpdatedUser.Status = EntityStatus.Active;
							toBeUpdatedUser.UpdatedAt = DateTime.Now;
							toBeUpdatedUser.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
							UnitOfWork.GetRepository<User>().Update(toBeUpdatedUser);
							UnitOfWork.SaveChanges();

							//Audit Logger
							var converte_Company_id = toBeUpdatedUser.Company_Id.GetValueOrDefault();
							_iauditExtension.Auditlogger(converte_Company_id, toBeUpdatedUser.Id, "You were updated");

							userModel = new UserServiceResponseModel() { user = null, Message = "Entity Updated Successfully", code = responseCode.Successful };
							return userModel;
						}
						else
						{
							userModel = new UserServiceResponseModel() { user = null, Message = "Company Do Not Exist", code = responseCode.ErrorOccured };
							return userModel;
						}
					}
					else
					{
						userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return userModel;
					}
					
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		//this service deletes user
		public UserServiceResponseModel DeleteUserService(Guid id)
		{
			try
			{

				User user = UnitOfWork.GetRepository<User>().Single(p => p.Id == id);
				if (user == null)
				{
					userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return userModel;
				}
				else
				{
					if (user.Status == EntityStatus.Active)
					{
						user.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<User>().Update(user);
						UnitOfWork.SaveChanges();

						//Audit Logger
						var converte_Company_id = user.Company_Id.GetValueOrDefault();
						_iauditExtension.Auditlogger(converte_Company_id, user.Id, "You Deleted a User");


						userModel = new UserServiceResponseModel() { user = user, Message = "Entity Deleted Successfully", code = responseCode.Successful };
						return userModel;
					}
					else
					{
						userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return userModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service fetches all companies
		public async Task<IEnumerable<User>> GetAllUsersService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<User> users = await UnitOfWork.GetRepository<User>().GetListAsync(t => t.Status == EntityStatus.Active);
				return users;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}

		//this service fetches companies by there id
		//I have an issue knowing what to return and the return type to use in this method
		public UserServiceResponseModel GetUserByIdService(Guid id)
		{
			try
			{
				User user = UnitOfWork.GetRepository<User>().Single(p => p.Id == id);

				//since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
				//CompanyViewModel companyViewModel = new CompanyViewModel
				//{
				//    Company_Name = company.Company_Name,
				//    Id = company.Id
				//};

				if (user != null)
				{
					if (user.Status == EntityStatus.Active)
					{
						userModel = new UserServiceResponseModel() { user = user, Message = "Entity Fetched Successfully", code = responseCode.Successful };
						return userModel;
					}
					else
					{
						userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return userModel;
					}
				}
				userModel = new UserServiceResponseModel() { user = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
				return userModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}

		public async Task<IEnumerable<User>> GetUsersByCompanyIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<User> users = await UnitOfWork.GetRepository<User>().GetListAsync(t =>t.Company_Id == id && t.Status == EntityStatus.Active);
				return users;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}
		public async Task<IEnumerable<User>> GetUsersByRoleService(User_Role role)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<User> users = await UnitOfWork.GetRepository<User>().GetListAsync(t => t.User_Role == role && t.Status == EntityStatus.Active);
				return users;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}
		//this methodis used to fetch deleted users
		public async Task<IEnumerable<User>> GetDeletedUsersService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<User> users = await UnitOfWork.GetRepository<User>().GetListAsync(t => t.Status == EntityStatus.InActive);
				return users;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}
	}
}
