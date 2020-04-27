using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ICustomer
    {
		CustomerServiceResponseModel CreateCustomerService(CustomerViewModel customer);
		CustomerServiceResponseModel UpdateCustomerService(CustomerViewModel customer);
		CustomerServiceResponseModel DeleteCustomerService(Guid id);
		Task<IEnumerable<Customer>> GetAllCustomerService();
		Task<IEnumerable<Customer>> GetCustomerByCompanyIdService(Guid id);
		Task<IEnumerable<Customer>> GetDeletedCustomerService();
		CustomerServiceResponseModel GetCustomerByIdService(Guid id);

	}
}
