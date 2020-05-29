using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ResponseModel.ServiceModels
{
	public class AddUserToTeamResponseModel
	{
		public UserTeam userTeam = new UserTeam();
		public string Message { get; set; }
		public string code { get; set; }
	}
}
