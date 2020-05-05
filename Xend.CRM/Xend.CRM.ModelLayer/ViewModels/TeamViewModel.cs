using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class TeamViewModel : BASE_ENTITY
    {
        public Guid Company_Id { get; set; }
		public Guid Createdby_Userid { get; set; }
		public string Team_Name { get; set; }

        [ForeignKey("Company_Id")]
        public virtual Company Company { get; set; }
		[ForeignKey("Createdby_Userid")]
		public virtual User CreatedUser { get; set; }
	}
}
