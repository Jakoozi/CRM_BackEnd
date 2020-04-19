using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Xend.CRM.ModelLayer.Entities;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ModelLayer.ResponseModel.ServiceModels;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.DbContexts;
using AutoMapper;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.ModelLayer.ViewModels;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.ModelExtensions;

namespace Xend.CRM.ServiceLayer.EntityServices
{
	
	public class UserServices : BaseService, IUser
	{

		ILoggerManager _loggerManager { get; }
		UserServiceResponseModel userModel;
		public UserServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}
		//this service creates new users
		public UserServiceResponseModel CreateUserService(UserViewModel user)
        {
			try
			{
				//unit of work is used to replace _context.

				User userToBeCreated = UnitOfWork.GetRepository<User>().Single(p => p.Email == user.Email);
				if(userToBeCreated != null)
				{
					userModel = new UserServiceResponseModel() {user = userToBeCreated, Message = "Entity Already Exists", code = "001" };
					return userModel;
				}
				else
				{
					Company checkIfCompanyExists = UnitOfWork.GetRepository<Company>().Single(p => p.Id == user.Company_Id);
					if(checkIfCompanyExists != null)
					{
						userToBeCreated = new User
						{
							Company_Id = user.Company_Id,
							First_Name = user.First_Name,
							Last_Name = user.Last_Name,
							Email = user.Email,
							Phonenumber = user.Phonenumber,
							User_Role = user.User_Role,
							XendCode = user.XendCode,
							Status = EntityStatus.Active,
							CreatedAt = DateTime.Now,
							CreatedAtTimeStamp = DateTime.Now.ToTimeStamp(),
							UpdatedAt = DateTime.Now,
							UpdatedAtTimeStamp = DateTime.Now.ToTimeStamp()
						};
						UnitOfWork.GetRepository<User>().Add(userToBeCreated);
						UnitOfWork.SaveChanges();

						userModel = new UserServiceResponseModel() { user = userToBeCreated, Message = "Entity Created Successfully", code = "002" };
						return userModel;
					}
					else
					{
						userModel = new UserServiceResponseModel() { user = userToBeCreated, Message = "Company Do Not Exist", code = "005" };
						return userModel;
					}
					
				}
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}
		}

        //this service updates user
        public void UpdateUserService()
        {

        }

        //this service fetches all the user
        public void GetAllUsersService()
        {

        }

        //this service fetches users by there id
        public void GetUserByIdService()
        {

        }

        //this service deletes user
        public void DeleteUserService()
        {

        }
    }
}
