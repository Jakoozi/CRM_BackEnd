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
    public class TeamServices : BaseService, ITeam
    {
		ILoggerManager _loggerManager { get; }
		TeamServiceResponseModel teamModel;
		public TeamServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}
		//this service creates new teams
		public TeamServiceResponseModel CreateTeamService(TeamViewModel team)
		{
			try
			{
				//unit of work is used to replace _context.
				Company teamsCompany = UnitOfWork.GetRepository<Company>().Single(p => p.Id == team.Company_Id);
				if (teamsCompany != null)
				{
					Team tobeCreatedTeam = UnitOfWork.GetRepository<Team>().Single(p => p.Team_Name == team.Team_Name);
					if(tobeCreatedTeam == null)
					{
						tobeCreatedTeam = new Team
						{
			
							Company_Id = team.Company_Id,
							Team_Name = team.Team_Name,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()

						};
						UnitOfWork.GetRepository<Team>().Add(tobeCreatedTeam);
						UnitOfWork.SaveChanges();

						teamModel = new TeamServiceResponseModel() { team = tobeCreatedTeam, Message = "Entity Created Successfully", code = "002" };
						return teamModel;
					}
					else
					{
						teamModel = new TeamServiceResponseModel() { team = tobeCreatedTeam, Message = "Entity Already Exists", code = "001" };
						return teamModel;
					}
					
				}
				else
				{
					teamModel = new TeamServiceResponseModel() { team = null, Message = "The Company Does Not Exist", code = "005" };
					return teamModel;


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}

		}

		//this service updates teams
		public TeamServiceResponseModel UpdateTeamService(TeamViewModel team)
		{

			try
			{
				//eRROR IS THROWING HERE, I THINK ITS THE EF MIGGRATION ISSUE
				Team toBeUpdatedTeam = UnitOfWork.GetRepository<Team>().Single(p => p.Id == team.Id);
				if (toBeUpdatedTeam == null)
				{
					teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
					return teamModel;
				}
				else
				{
					if(toBeUpdatedTeam.Status == EntityStatus.Active)
					{
						//here i will assign directly what i want to update to the model instead of creating a new instance
						toBeUpdatedTeam.Team_Name = team.Team_Name;
						toBeUpdatedTeam.UpdatedAt = DateTime.Now;
						toBeUpdatedTeam.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
						UnitOfWork.GetRepository<Team>().Update(toBeUpdatedTeam); ;
						UnitOfWork.SaveChanges();

						teamModel = new TeamServiceResponseModel() { team = toBeUpdatedTeam, Message = "Entity Updated Successfully", code = "002" };
						return teamModel;
					}
					else
					{
						teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
						return teamModel;
					}
					
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service deletes teams
		public TeamServiceResponseModel DeleteTeamService(Guid id)
		{
			try
			{

				Team team = UnitOfWork.GetRepository<Team>().Single(p => p.Id == id);
				if (team == null)
				{
					teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
					return teamModel;
				}
				else
				{
					if (team.Status == EntityStatus.Active)
					{
						team.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Team>().Update(team);
						UnitOfWork.SaveChanges();

						teamModel = new TeamServiceResponseModel() { team = team, Message = "Entity Deleted Successfully", code = "002" };
						return teamModel;
					}
					else
					{
						teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
						return teamModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		//this service fetches teams by there id
		public TeamServiceResponseModel GetTeamByIdService(Guid id)
		{
			try
			{
				Team team = UnitOfWork.GetRepository<Team>().Single(p => p.Id == id);

				//since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
				//CompanyViewModel companyViewModel = new CompanyViewModel
				//{
				//    Company_Name = company.Company_Name,
				//    Id = company.Id
				//};

				if (team != null)
				{
					if (team.Status == EntityStatus.Active)
					{
						teamModel = new TeamServiceResponseModel() { team = team, Message = "Entity Fetched Successfully", code = "002" };
						return teamModel;
					}
					else
					{
						teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
						return teamModel;
					}
				}
				teamModel = new TeamServiceResponseModel() { team = null, Message = "Entity Does Not Exist", code = "001" };
				return teamModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this service fetches all the teams
		public async Task<IEnumerable<Team>> GetAllTeamsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Team> teams = await UnitOfWork.GetRepository<Team>().GetListAsync(t => t.Status == EntityStatus.Active);
				return teams;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this is the service for fetching teams by there company id's
		public async Task<IEnumerable<Team>> GetTeamsByCompanyIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Team> teams = await UnitOfWork.GetRepository<Team>().GetListAsync(t =>t.Company_Id == id && t.Status == EntityStatus.Active);
				return teams;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this fetches all deleted teams
		public async Task<IEnumerable<Team>> GetDeletedTeamsService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Team> teams = await UnitOfWork.GetRepository<Team>().GetListAsync(t => t.Status == EntityStatus.InActive);
				return teams;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
	}
}
