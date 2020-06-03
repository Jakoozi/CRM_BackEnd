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
		public string Company_Name { get; set; }
		public string Team_Name { get; set; }
		public string User_Name { get; set; }
	}
}
