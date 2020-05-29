using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class CompanyViewModel : BASE_ENTITY
    {
		public Guid Createdby_Userid { get; set; }
        public string Company_Name { get; set; }
		public string Company_Description { get; set; }
	}
}
