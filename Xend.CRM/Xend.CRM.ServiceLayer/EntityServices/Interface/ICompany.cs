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
		Task<IEnumerable<Company>> GetDeletedCompaniesService();
		CompanyServiceResponseModel GetCompanyByIdService(Guid id);
		CompanyServiceResponseModel UpdateCompanyService(CompanyViewModel company);
		CompanyServiceResponseModel DeleteCompanyService(Guid id);
    }
}
