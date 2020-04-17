﻿using System.Linq;
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
                        CreatedAt = DateTime.UtcNow,
                        CreatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp(),
                        UpdatedAt = DateTime.UtcNow,
                        UpdatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp()
                        
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
                Company updatedCompany = UnitOfWork.GetRepository<Company>().Single(p => p.Id == company.Id);
                if (updatedCompany == null)
                {
					companyModel = new CompanyServiceResponseModel() { company = null, Message = "Entity Does Not Exist", code = "001" };
					return companyModel;
				}
                else
                {
					//here i will assign directly what i want to update to the model instead of creating a new instance
					updatedCompany.Company_Name = company.Company_Name;
					updatedCompany.UpdatedAt = DateTime.UtcNow;
					updatedCompany.UpdatedAtTimeStamp = DateTime.UtcNow.ToTimeStamp();
					UnitOfWork.GetRepository<Company>().Update(updatedCompany); ;
					UnitOfWork.SaveChanges();

					companyModel = new CompanyServiceResponseModel() { company = updatedCompany, Message = "Entity Updated Successfully", code = "002" };
                    return companyModel;
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
