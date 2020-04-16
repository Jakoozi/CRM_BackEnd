using System.Linq;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.Core.DataAccessLayer.Repository;
using AutoMapper;
using Xend.CRM.Core.Logger;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.ModelExtensions;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class CompanyServices : BaseService, ICompany
    {
        ILoggerManager _loggerManager { get; }
        public CompanyServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
        {
            _loggerManager = loggerManager;
        }

        //this service creates companies
        public string CompanyCreationService  (CompanyViewModel company)
        {
            try
            {
                //unit of work is used to replace _context.
                Company comp = UnitOfWork.GetRepository<Company>().Single(p => p.Company_Name == company.Company_Name);
                if (comp != null)
                {
                    return "Entity Already Exists";
                }
                else
                {
                    comp = new Company
                    {
                        Company_Name = company.Company_Name,
                        Status = EntityStatus.Active,
                        CreatedAt = DateTime.UtcNow,
                        CreatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp(),
                        UpdatedAt = DateTime.UtcNow,
                        UpdatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp()
                        
                    };
                    UnitOfWork.GetRepository<Company>().Add(comp);
                    UnitOfWork.SaveChanges();
					return "Entity Created Successfully";
					
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw;
            }

        }

		//this service updates a company by its id
		public string UpdateCompanyService(CompanyViewModel company)
        {
			CompanyServiceResponseModel companymodel = new CompanyServiceResponseModel();
            try
            {
                Company comp = UnitOfWork.GetRepository<Company>().Single(p => p.Id == company.Id);
                if (comp == null)
                {
                    return "Entity Does Not Exist";
                }
                else
                {
                    //here i will assign directly what i want to update to the model instead of creating a new instance
                    comp.Company_Name = company.Company_Name;
                    comp.UpdatedAt = DateTime.UtcNow;
                    comp.UpdatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp();
					UnitOfWork.GetRepository<Company>().Update(comp); ;
					UnitOfWork.SaveChanges();

                    return "Entity Updated Successfully";
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw;
            }
        }

        //this service deletes companies from the CRM. It does not actually delete this services,
        //It only changes there status from Active to Inactive.
        public string DeleteCompanyService(Guid id)
        {
            try
            {
                Company company = UnitOfWork.GetRepository<Company>().Single(p => p.Id == id);
                if (company == null)
                {
                    return "Entity Does Not Exist";
                }
                else
                {
                    company.Status = EntityStatus.Active;
                    //UnitOfWork.GetRepository<Company>().Update(company);
                    UnitOfWork.SaveChanges();

                    return "Entity Deleted Successfully";
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw;
            }
        }

        //this service fetches all companies
        public async Task<IEnumerable<Company>> GetAllCompaniesService()
        {
            try
            {
                //i am meant to await that response and asign it to an ienumerable
                IEnumerable<Company> company = await UnitOfWork.GetRepository<Company>().GetListAsync();
                return company;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw ex;
            }

        }

        //this service fetches companies by there id
        //I have an issue knowing what to return and the return type to use in this method
        public CompanyViewModel GetCompanyByIdService(Guid id)
        {
            try
            {
                Company company = UnitOfWork.GetRepository<Company>().Single(p => p.Id == id);

                //since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
                CompanyViewModel companyViewModel = new CompanyViewModel
                {
                    Company_Name = company.Company_Name,
                    Id = company.Id
                };

                if (company != null)
                {
                    return companyViewModel;
                }
                return null;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw ex;
            }
        }


    }
}
