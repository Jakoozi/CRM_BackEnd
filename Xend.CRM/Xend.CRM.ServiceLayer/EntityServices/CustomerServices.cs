using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ModelExtensions;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ServiceLayer.EntityServices.Interface;

namespace Xend.CRM.ServiceLayer.EntityServices
{
    public class CustomerServices : BaseService, ICustomer
    {
		ILoggerManager _loggerManager { get; }
		CustomerServiceResponseModel customerModel;
		public CustomerServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}

		//this service creates new customers
		public CustomerServiceResponseModel CreateCustomerService(CustomerViewModel customer)
		{
			try
			{
				//unit of work is used to replace _context.

				Customer customerToBeCreated = UnitOfWork.GetRepository<Customer>().Single(p => p.Email == customer.Email || p.Phonenumber == customer.Phonenumber || p.XendCode == customer.XendCode);
				if (customerToBeCreated != null)
				{
					customerModel = new CustomerServiceResponseModel() { customer = customerToBeCreated, Message = "Entity Already Exists", code = "001" };
					return customerModel;
				}
				else
				{
					Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == customer.Company_Id && p.Status == EntityStatus.Active);
					if (checkIfCompanyExists != null)
					{
						customerToBeCreated = new Customer
						{
							Company_Id = customer.Company_Id,
							First_Name = customer.First_Name,
							Last_Name = customer.Last_Name,
							Email = customer.Email,
							Phonenumber = customer.Phonenumber,
							XendCode = customer.XendCode,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
						};
						UnitOfWork.GetRepository<Customer>().Add(customerToBeCreated);
						UnitOfWork.SaveChanges();

						customerModel = new CustomerServiceResponseModel() { customer = customerToBeCreated, Message = "Entity Created Successfully", code = "002" };
						return customerModel;
					}
					else
					{
						customerModel = new CustomerServiceResponseModel() { customer = customerToBeCreated, Message = "Company Do Not Exist", code = "005" };
						return customerModel;
					}

				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service updates customer details
		public CustomerServiceResponseModel UpdateCustomerService(CustomerViewModel customer)
		{

			try
			{
				Customer toBeUpdatedCustomer = UnitOfWork.GetRepository<Customer>().Single(p => p.Id == customer.Id);
				if (toBeUpdatedCustomer == null)
				{
					customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
					return customerModel;
				}
				else
				{
					if (toBeUpdatedCustomer.Status == EntityStatus.Active)
					{
						Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == customer.Company_Id && p.Status == EntityStatus.Active);
						if (checkIfCompanyExists != null)
						{

							//here i will assign directly what i want to update to the model instead of creating a new instance
							//toBeUpdatedCustomer.Company_Id = user.Company_Id;
							toBeUpdatedCustomer.First_Name = customer.First_Name;
							toBeUpdatedCustomer.Last_Name = customer.Last_Name;
							toBeUpdatedCustomer.Email = customer.Email;
							toBeUpdatedCustomer.Phonenumber = customer.Phonenumber;
							toBeUpdatedCustomer.XendCode = customer.XendCode;
							toBeUpdatedCustomer.Status = EntityStatus.Active;
							toBeUpdatedCustomer.UpdatedAt = DateTime.Now;
							toBeUpdatedCustomer.UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp();
							UnitOfWork.GetRepository<Customer>().Update(toBeUpdatedCustomer); ;
							UnitOfWork.SaveChanges();

							customerModel = new CustomerServiceResponseModel() { customer = toBeUpdatedCustomer, Message = "Entity Updated Successfully", code = "002" };
							return customerModel;
						}
						else
						{
							customerModel = new CustomerServiceResponseModel() { customer = toBeUpdatedCustomer, Message = "Company Do Not Exist", code = "005" };
							return customerModel;
						}
					}
					else
					{
						customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
						return customerModel;
					}

				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}
		//this service deletes customers by there id
		public CustomerServiceResponseModel DeleteCustomerService(Guid id)
		{
			try
			{

				Customer customer = UnitOfWork.GetRepository<Customer>().Single(p => p.Id == id);
				if (customer == null)
				{
					customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
					return customerModel;
				}
				else
				{
					if (customer.Status == EntityStatus.Active)
					{
						customer.Status = EntityStatus.InActive;
						UnitOfWork.GetRepository<Customer>().Update(customer);
						UnitOfWork.SaveChanges();

						customerModel = new CustomerServiceResponseModel() { customer = customer, Message = "Entity Deleted Successfully", code = "002" };
						return customerModel;
					}
					else
					{
						customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
						return customerModel;
					}


				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

		//this service gets customers by there id
		public CustomerServiceResponseModel GetCustomerByIdService(Guid id)
		{
			try
			{
				Customer customer = UnitOfWork.GetRepository<Customer>().Single(p => p.Id == id);

				//since i cant send company directly, i get the company and pass the values i need into the companyViewModel which i then return
				//CompanyViewModel companyViewModel = new CompanyViewModel
				//{
				//    Company_Name = company.Company_Name,
				//    Id = company.Id
				//};

				if (customer != null)
				{
					if (customer.Status == EntityStatus.Active)
					{
						customerModel = new CustomerServiceResponseModel() { customer = customer, Message = "Entity Fetched Successfully", code = "002" };
						return customerModel;
					}
					else
					{
						customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
						return customerModel;
					}
				}
				customerModel = new CustomerServiceResponseModel() { customer = null, Message = "Entity Does Not Exist", code = "001" };
				return customerModel;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}

		//this method gets all custmers
		public async Task<IEnumerable<Customer>> GetAllCustomerService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Customer> customers = await UnitOfWork.GetRepository<Customer>().GetListAsync(t => t.Status == EntityStatus.Active);
				return customers;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		public async Task<IEnumerable<Customer>> GetCustomerByCompanyIdService(Guid id)
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Customer> customers = await UnitOfWork.GetRepository<Customer>().GetListAsync(t =>t.Company_Id == id && t.Status == EntityStatus.Active);
				return customers;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
		//this service fetches deleted customers
		public async Task<IEnumerable<Customer>> GetDeletedCustomerService()
		{
			try
			{
				//i am meant to await that response and asign it to an ienumerable
				IEnumerable<Customer> customers = await UnitOfWork.GetRepository<Customer>().GetListAsync(t => t.Status == EntityStatus.InActive);
				return customers;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw ex;
			}
		}
	}
}
