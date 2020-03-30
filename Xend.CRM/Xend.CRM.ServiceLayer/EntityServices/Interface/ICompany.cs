using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ICompany
    {
        string CompanyCreationService(CompanyViewModel company);
        Task<IEnumerable<Company>> GetAllCompaniesService();
        CompanyViewModel GetCompanyByIdService(Guid id);
        string UpdateCompanyService(CompanyViewModel company);
        string DeleteCompanyService(Guid id);
    }
}
