using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface IUser
    {
        void CreateUserService();
        void UpdateUserService();
        void GetAllUsersService();
        void GetUserByIdService();
        void DeleteUserService();
    }
}
