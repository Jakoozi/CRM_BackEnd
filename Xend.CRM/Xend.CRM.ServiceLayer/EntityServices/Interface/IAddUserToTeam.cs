using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
	public interface IAddUserToTeam
	{
		AddUserToTeamResponseModel AddUserToATeamService(UserTeamViewModel userTeam);
		AddUserToTeamResponseModel DeleteUserService(Guid id);
		Task<IEnumerable<UserTeam>> GetAllTheUsersTeamService(Guid id);
	}
}
