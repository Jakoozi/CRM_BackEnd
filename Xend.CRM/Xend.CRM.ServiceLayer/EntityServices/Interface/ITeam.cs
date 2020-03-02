using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ITeam
    {
        void CreateTeamService();
        void UpdateTeamService();
        void GetAllTeamsService();
        void GetTeamByIdService();
        void DeleteTeamService();
    }
}
