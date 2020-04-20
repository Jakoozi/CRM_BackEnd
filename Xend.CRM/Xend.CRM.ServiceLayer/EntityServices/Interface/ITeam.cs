using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ITeam
    {
		TeamServiceResponseModel CreateTeamService(TeamViewModel ticket);

		TeamServiceResponseModel UpdateTeamService(TeamViewModel team);
		TeamServiceResponseModel DeleteTeamService(Guid id);
		Task<IEnumerable<Team>> GetAllTeamsService();
		TeamServiceResponseModel GetTeamByIdService(Guid id);

	}
}
