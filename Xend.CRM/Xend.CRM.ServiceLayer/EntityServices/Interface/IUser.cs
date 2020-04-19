﻿using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface IUser
    {
		UserServiceResponseModel CreateUserService(UserViewModel user);
		UserServiceResponseModel UpdateUserService(UserViewModel user);
        void GetAllUsersService();
        void GetUserByIdService();
        void DeleteUserService();
    }
}
