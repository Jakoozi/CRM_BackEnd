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
	public class AddUserToTeamService : BaseService, IAddUserToTeam
	{
		ILoggerManager _loggerManager { get; }
		IAuditExtension _iauditExtension { get; }
		AddUserToTeamResponseModel userteamResponse;
		ResponseCodes responseCode = new ResponseCodes();

		public AddUserToTeamService(IUnitOfWork<XendDbContext> unitOfWork, IAuditExtension iauditExtention, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
			_iauditExtension = iauditExtention;
		}

		public AddUserToTeamResponseModel AddUserToATeamService(UserTeamViewModel userTeam)
		{
			try
			{
				Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == userTeam.Company_Id && p.Status == EntityStatus.Active);
				if (checkIfCompanyExists != null)
				{
					User checkIfUserExists = UnitOfWork.GetRepository<User>().Single(p => p.Id == userTeam.User_Id && p.Status == EntityStatus.Active);
					if(checkIfUserExists != null)
					{
						Team checkIfTeamExists = UnitOfWork.GetRepository<Team>().Single(p => p.Id == userTeam.Team_Id && p.Status == EntityStatus.Active);
						if(checkIfTeamExists != null)
						{
							UserTeam checkIfUserAlreadyBelongsToTeam = UnitOfWork.GetRepository<UserTeam>().Single(p => p.User_Id == userTeam.User_Id && p.Team_Id == userTeam.Team_Id && p.Status == EntityStatus.Active);
							if(checkIfUserAlreadyBelongsToTeam != null)
							{
								userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "The User already belongs to this Team", code = responseCode.ErrorOccured };
								return userteamResponse;
							}
							else
							{
								//adding the user to the team
								var UserName = $"{checkIfUserExists.First_Name} {checkIfUserExists.Last_Name}";
								//Add Userand Company Names
								UserTeam addUserToTeam = new UserTeam()
								{
									Company_Id = userTeam.Company_Id,
									Team_Id = userTeam.Team_Id,
									User_Id = userTeam.User_Id,
									Company_Name = checkIfCompanyExists.Company_Name,
									Team_Name = checkIfTeamExists.Team_Name,
									User_Name = UserName,
									Status = EntityStatus.Active,
									CreatedAt = DateTime.Now,
									CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
									UpdatedAt = DateTime.Now,
									UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
								};

								UnitOfWork.GetRepository<UserTeam>().Add(addUserToTeam);
								UnitOfWork.SaveChanges();

								//Audit logging of the added user
								_iauditExtension.Auditlogger(userTeam.Company_Id, userTeam.User_Id, "You  were added to a Team");

								userteamResponse = new AddUserToTeamResponseModel() { userTeam = addUserToTeam, Message = " User Successfully Added to Team", code = responseCode.Successful };
								return userteamResponse;
							}
							
						}
						else
						{
							userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "Team Do Not Exist", code = responseCode.ErrorOccured };
							return userteamResponse;
						}
					}
					else
					{
						userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "User Do Not Exist", code = responseCode.ErrorOccured };
						return userteamResponse;
					}
				}
				else
				{
					userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "Company Do Not Exist", code = responseCode.ErrorOccured };
					return userteamResponse;
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		public AddUserToTeamResponseModel DeleteUserService(Guid id)
		{
			try
			{
				UserTeam userTeam = UnitOfWork.GetRepository<UserTeam>().Single(p => p.Id == id);
				if (userTeam == null)
				{
					userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
					return userteamResponse;
				}
				else
				{
					if (userTeam.Status == EntityStatus.Active)
					{
						userTeam.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<UserTeam>().Update(userTeam);
						UnitOfWork.SaveChanges();

						//Audit Logger
						_iauditExtension.Auditlogger(userTeam.Company_Id, userTeam.User_Id, "You were removed from a team");


						userteamResponse = new AddUserToTeamResponseModel() { userTeam = userTeam, Message = "Entity Deleted Successfully", code = responseCode.Successful  };
						return userteamResponse;
					}
					else
					{
						userteamResponse = new AddUserToTeamResponseModel() { userTeam = null, Message = "Entity Does Not Exist", code = responseCode.ErrorOccured };
						return userteamResponse;
					}
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		public async Task<IEnumerable<UserTeam>> GetAllTheUsersTeamService(Guid id)
		{
			try
			{
				//The id is the user's id to get all the teams the user is in
				IEnumerable<UserTeam> userTeam = await UnitOfWork.GetRepository<UserTeam>().GetListAsync(t => t.Status == EntityStatus.Active && t.User_Id == id);
				return userTeam;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}

		}
	}
}
