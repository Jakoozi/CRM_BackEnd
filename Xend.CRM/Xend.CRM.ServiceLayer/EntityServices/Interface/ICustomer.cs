using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface ICustomer
    {
        void CreateCustomerService();
        void DeleteCustomerService();
        void UpdateCustomerService();
        void GetAllCustomerService();
        void GetCustomerByIdService();
    }
}
