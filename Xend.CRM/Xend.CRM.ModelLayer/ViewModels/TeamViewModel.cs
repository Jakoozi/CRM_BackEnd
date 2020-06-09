using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class TeamViewModel 
    {
		public Guid Id { get; set; }
		public Guid Company_Id { get; set; }
		public Guid Createdby_Userid { get; set; }
		public string Company_Name { get; set; }
		public string Team_Name { get; set; }
		public string Team_Description { get; set; }

	}
}
