﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ICompany
    {
		CompanyServiceResponseModel CompanyCreationService(CompanyViewModel company);
        Task<IEnumerable<Company>> GetAllCompaniesService();
        CompanyViewModel GetCompanyByIdService(Guid id);
		CompanyServiceResponseModel UpdateCompanyService(CompanyViewModel company);
        string DeleteCompanyService(Guid id);
    }
}
