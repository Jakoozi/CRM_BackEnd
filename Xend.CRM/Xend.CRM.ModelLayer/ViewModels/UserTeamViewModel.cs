using System;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
	public class UserTeamViewModel : BASE_ENTITY
	{
		public Guid Company_Id { get; set; }
		public Guid Team_Id { get; set; }
		public Guid User_Id { get; set; }
	}
}
