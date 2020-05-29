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

namespace Xend.CRM.ServiceLayer.EntityServices
{
	public class LoginService : BaseService, ILogin
	{
		ILoggerManager _loggerManager { get; }
		IAuditExtension _iauditExtension { get; }
		UserServiceResponseModel userModel;
		ResponseCodes responseCode = new ResponseCodes();
		public LoginService(IUnitOfWork<XendDbContext> unitOfWork, IAuditExtension iauditExtention, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iauditExtension = iauditExtention;
		}

		//this logs in an Agent user
		public UserServiceResponseModel AdminLogin(UserViewModel user)
		{
			try
			{
				
				User agentToBeLogged = UnitOfWork.GetRepository<User>().Single(p => p.Email == user.Email);

				if(agentToBeLogged == null)
				{
					userModel = new UserServiceResponseModel() {user = null, code = responseCode.ErrorOccured, Message = "Agent Does Not Exist" };
					return userModel;
				}
				else
				{
					if(agentToBeLogged.User_Password == user.User_Password)
					{
						//Audit Logger
						_iauditExtension.Auditlogger(agentToBeLogged.Company_Id, agentToBeLogged.Id, "You Logged in");

						User responseUser = new User()
						{
							Company_Id = agentToBeLogged.Company_Id,
							Id = agentToBeLogged.Id,
							First_Name = agentToBeLogged.First_Name,
							Last_Name = agentToBeLogged.Last_Name
						};
						userModel = new UserServiceResponseModel() { user = responseUser, code = responseCode.Successful, Message = "Login Successful" };
						return userModel;
					}
					else
					{
						userModel = new UserServiceResponseModel() { user = null, code = responseCode.ErrorOccured, Message = "Incorrect Password" };
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
	}
}
