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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class CompanyServices : BaseService, ICompany
    {
        ILoggerManager _loggerManager { get; }
		CompanyServiceResponseModel companyModel;
		public CompanyServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
        {
            _loggerManager = loggerManager;
        }

        //this service creates companies
        public CompanyServiceResponseModel CompanyCreationService  (CompanyViewModel company)
        {
            try
            {
                //unit of work is used to replace _context.
                Company createdCompany = UnitOfWork.GetRepository<Company>().Single(p => p.Company_Name == company.Company_Name);
                if (createdCompany != null)
                {
					
					companyModel = new CompanyServiceResponseModel() { company = createdCompany, Message = "Entity Already Exists", code = "001" };
					return companyModel;
                }
                else
                {
                    createdCompany = new Company
                    {
                        Company_Name = company.Company_Name,
                        Status = EntityStatus.Active,
                        CreatedAt = DateTime.Now,
                        CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
                        UpdatedAt = DateTime.Now,
                        UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
                        
                    };
                    UnitOfWork.GetRepository<Company>().Add(createdCompany);
                    UnitOfWork.SaveChanges();

					companyModel = new CompanyServiceResponseModel() { company = createdCompany, Message = "Entity Created Successfully", code = "002" };
					return companyModel;
					
                }
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw;
            }

        }

		//this service updates a company by its id
		public CompanyServiceResponseModel UpdateCompanyService(CompanyViewModel company)
        {
			
            try
            {
                Company toBeUpdatedCompany = UnitOfWork.GetRepository<Company>().Single(p => p.Id == company.Id);
                if (toBeUpdatedCompany == null)
                {
					companyModel = new CompanyServiceResponseModel() { company = null, Message = "Entity Does Not Exist", code = "001" };
					return companyModel;
				}
                else
                {
					Company ifCompanyNameExistsCheck = UnitOfWork.GetRepository<Company>().Single(p => p.Company_Name == company.Company_Name);
					if(ifCompanyNameExistsCheck == null)
					{
						//here i will assign directly what i want to update to the model instead of creating a new instance
						toBeUpdatedCompany.Company_Name = company.Company_Name;
						toBeUpdatedCompany.UpdatedAt = DateTime.Now;
						toBeUpdatedCompany.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
						UnitOfWork.GetRepository<Company>().Update(toBeUpdatedCompany); ;
						UnitOfWork.SaveChanges();

						companyModel = new CompanyServiceResponseModel() { company = toBeUpdatedCompany, Message = "Entity Updated Successfully", code = "002" };
						return companyModel;
					}
					else
					{
						companyModel = new CompanyServiceResponseModel() { company = toBeUpdatedCompany, Message = "Entity Already Exists", code = "005" };
						return companyModel;
					}
					
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
        public CompanyServiceResponseModel DeleteCompanyService(Guid id)
        {
            try
            {
				
				Company company = UnitOfWork.GetRepository<Company>().Single(p => p.Id == id);
                if (company == null)
                {
					companyModel = new CompanyServiceResponseModel() { company = null, Message = "Entity Does Not Exist", code = "001" };
					return companyModel;
                }
                else
                {
					if(company.Status == EntityStatus.Active)
					{
						company.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Company>().Update(company);
						UnitOfWork.SaveChanges();

						companyModel = new CompanyServiceResponseModel() { company = company, Message = "Entity Deleted Successfully", code = "002" };
						return companyModel;
					}
					else
					{
						companyModel = new CompanyServiceResponseModel() { company = null, Message = "Entity Does Not Exist", code = "001" };
						return companyModel;
					}
					
					
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
                IEnumerable<Company> company = await UnitOfWork.GetRepository<Company>().GetListAsync(t => t.Status == EntityStatus.Active);
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
		public CompanyServiceResponseModel GetCompanyByIdService(Guid id)
        {
            try
            {
                Company company = UnitOfWork.GetRepository<Company>().Single(p => p.Id == id);

                //since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
                //CompanyViewModel companyViewModel = new CompanyViewModel
                //{
                //    Company_Name = company.Company_Name,
                //    Id = company.Id
                //};

                if (company != null)
                {
					if(company.Status == EntityStatus.Active)
					{
						companyModel = new CompanyServiceResponseModel() { company = company, Message = "Entity Fetched Successfully", code = "002" };
						return companyModel;
					}
					else
					{
						companyModel = new CompanyServiceResponseModel() { company = null, Message = "Entity Does Not Exist", code = "001" };
						return companyModel;
					}
				}
				companyModel = new CompanyServiceResponseModel() {company = null, Message = "Entity Does Not Exist", code = "001" };
                return companyModel;
            }
            catch (Exception ex)
            {
                _loggerManager.LogError(ex.Message);
                throw ex;
            }
        }


    }
}
