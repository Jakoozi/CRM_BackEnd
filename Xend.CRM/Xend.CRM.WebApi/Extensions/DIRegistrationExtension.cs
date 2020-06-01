using Microsoft.Extensions.DependencyInjection;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.Core.MessageBroker;
using Xend.CRM.Core.ServiceLayer;
using Xend.CRM.Core.ServiceLayer.Bootstrapper;
using Xend.CRM.DataAccessLayer.Repository;
using Xend.CRM.ModelLayer.Appsetting;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ServiceLayer.Bootstrapper;
using Xend.CRM.ServiceLayer.EntityServices;
using Xend.CRM.ServiceLayer.EntityServices.Interface;
using Xend.CRM.ServiceLayer.Logger;
using Xend.CRM.ServiceLayer.MessageBroker;
using Xend.CRM.ServiceLayer.PaginationService;
using Xend.CRM.ServiceLayer.ServiceExtentions;

namespace Xend.CRM.WebApi.Extensions
{
    /// <summary>
    /// IServiceCollection class extensions for use in Startup.cs to register classes for Dependency injection
    /// </summary>
    public static class DIRegistrationExtension
    {
        private static IServiceCollection ConfigureBootstrappers(this IServiceCollection services)
        {
            //This is were services for dependency injection will be added
            services.AddTransient<IStartManager, StartManager>();
            services.AddTransient<IStopManager, StopManager>();
            services.AddTransient<ILoggerManager, LoggerManager>();
            services.AddTransient<IMessageSender, MessageSender>();
            services.AddTransient<IUnitOfWork<XendDbContext>, UnitOfWork<XendDbContext>>();
            services.AddTransient<IPaginatedResultService, PaginatedResultService>();

            //this are my  model services
            services.AddTransient<IAuditRail, AuditRailServices>();
            services.AddScoped<ICompany, CompanyServices>();
            services.AddTransient<ICustomer, CustomerServices>();
            services.AddTransient<ITeam, TeamServices>();
            services.AddTransient<ITicket, TicketServices>();
            services.AddTransient<IUser, UserServices>();
			services.AddTransient<ILogin, LoginService>();
			services.AddTransient<IEmailService, EmailService>();
			services.AddTransient<IAddUserToTeam, AddUserToTeamService>();

			//my Model extension registration
			services.AddTransient<ITicketExtension ,TicketServiceExtention>();
			services.AddTransient<IAuditExtension, AuditServiceExtension>();



			return services;
        }
        public static IServiceCollection IoCRootResolver(this IServiceCollection services, AppSetting appSetting)
        {
            services.AddSingleton(appSetting);
            services.ConfigureBootstrappers();
            return services;
        }
    }
}

