using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.ModelLayer.ViewModels;

namespace Xend.CRM.ServiceLayer.EntityServices.Interface
{
    public interface IUser
    {
		UserServiceResponseModel CreateUserService(UserViewModel user);
		UserServiceResponseModel UpdateUserService(UserViewModel user);
		UserServiceResponseModel DeleteUserService(Guid id);
		Task<IEnumerable<User>> GetAllUsersService();
		Task<IEnumerable<User>> GetUsersByCompanyIdService(Guid id);
		Task<IEnumerable<User>> GetUsersByRoleService(User_Role role);
		Task<IEnumerable<User>> GetDeletedUsersService();
		UserServiceResponseModel GetUserByIdService(Guid id);


		//UserServiceResponseModel AgentLogin(UserViewModel user);



	}
}
