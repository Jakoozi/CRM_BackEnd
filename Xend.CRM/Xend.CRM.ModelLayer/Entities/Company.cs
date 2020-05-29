using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ModelLayer.Entities
{
    public class Company : BASE_ENTITY
    {

		public Guid Createdby_Userid { get; set; }
		public string Company_Name { get; set; }
		public string Company_Description { get; set; }

		//this is the datatable relationship
		[ForeignKey("Createdby_Userid")]
		public virtual User CreatedUser { get; set; }
	}
}
