using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ICompany
    {
        void CompanyCreationService();
        void GetAllCompaniesService();
        void GetCompanyByIdService();
        void UpdateCompanyService();
        void DeleteCompanyService();
    }
}
