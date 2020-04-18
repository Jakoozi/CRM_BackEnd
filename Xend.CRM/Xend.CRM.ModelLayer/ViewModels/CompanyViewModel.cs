using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.ViewModels
{
    public class CompanyViewModel : BASE_ENTITY
    {
        public Guid? Id { get; set; }
        [Required]
        public string Company_Name { get; set; }

		public static implicit operator CompanyViewModel(Company v)
		{
			throw new NotImplementedException();
		}
	}
}
