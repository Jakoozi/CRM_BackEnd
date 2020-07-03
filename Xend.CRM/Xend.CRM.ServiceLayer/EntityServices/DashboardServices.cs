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
using Xend.CRM.ServiceLayer.ServiceExtentions;
using Xend.CRM.ModelLayer.ResponseModel;
using Xend.CRM.ModelLayer.ViewModels.Put_View_Models;

namespace Xend.CRM.ServiceLayer.EntityServices
{
	public class DashboardServices : BaseService, IDashboard
	{
		ILoggerManager _loggerManager { get; }
		ResponseCodes responseCode = new ResponseCodes();
		DashboardResponseModel dashboardResponse;

		public DashboardServices(IUnitOfWork<XendDbContext> unitOfWork, IMapper mapper, ILoggerManager loggerManager) : base(unitOfWork, mapper)
		{
			_loggerManager = loggerManager;
		}

		public async Task<DashboardResponseModel> GetDashboardValuesService(Guid id)
		{
			try
			{
				//getting ticket values
				IEnumerable<Ticket> tickets = await UnitOfWork.GetRepository<Ticket>().GetListAsync();
				int NumberOf_NewTickets = tickets.Where(t => t.Ticket_Status == Ticket_Status.New && t.Company_Id == id).Count();
				int NumberOf_ResolvedTickets = tickets.Where(t => t.Ticket_Status == Ticket_Status.Resolved && t.Company_Id == id).Count();
				int NumberOf_ClosedTickets = tickets.Where(t => t.Ticket_Status == Ticket_Status.Closed && t.Company_Id == id).Count();
				int NumberOf_Tickets = tickets.Where(t => t.Company_Id == id).Count();
				int TotalNumberOfTickets = tickets.Count();

				//getting user values
				IEnumerable<User> users = await UnitOfWork.GetRepository<User>().GetListAsync();
				int NumberOf_Agents = users.Where(u => u.User_Role == User_Role.Agent && u.Company_Id == id).Count();
				int NumberOf_Admins = users.Where(u => u.User_Role == User_Role.Admin ).Count();	
				int TotalNumberOfUsers = users.Count();

				//Getting companies metrics
				IEnumerable<Company> companies = await UnitOfWork.GetRepository<Company>().GetListAsync(t => t.Status == EntityStatus.Active);
				int TotalNumberOfCompanies = companies.Count();

				//Getting customers metrics
				IEnumerable<Customer> customers = await UnitOfWork.GetRepository<Customer>().GetListAsync();
				int NumberOf_Customers = customers.Count(t => t.Company_Id == id);
				int TotalNumberOfCustomers = customers.Count();

				//Getting Teams metrics
				IEnumerable<Team> teams = await UnitOfWork.GetRepository<Team>().GetListAsync();
				int NumberOf_Teams = teams.Count(t => t.Company_Id == id);
 				int TotalNumberOfTeams = teams.Count();


				DashboardViewModel dashboardReturnValues = new DashboardViewModel()
				{
					NumberOf_NewTickets = NumberOf_NewTickets,
					NumberOf_ResolvedTickets = NumberOf_ResolvedTickets,
					NumberOf_ClosedTickets = NumberOf_ClosedTickets,
					NumberOf_Tickets = NumberOf_Tickets,
					TotalNumberOfTickets = TotalNumberOfTickets,
					NumberOf_Admins = NumberOf_Admins,
					NumberOf_Agents = NumberOf_Agents,
					TotalNumberOfUsers = TotalNumberOfUsers,
					TotalNumberOfCompanies = TotalNumberOfCompanies,
					TotalNumberOfCustomers = TotalNumberOfCustomers,
					NumberOf_Customers = NumberOf_Customers,
					TotalNumberOfTeams = TotalNumberOfTeams,
					NumberOf_Teams = NumberOf_Teams,
				};

				dashboardResponse = new DashboardResponseModel() { dashboardViewModel = dashboardReturnValues, code = responseCode.Successful, Message = "successful" };


				return dashboardResponse;
			}
			catch (Exception ex)
			{
				_loggerManager.LogError(ex.Message);
				throw;
			}

		}
	}


}
