using Microsoft.Extensions.DependencyInjection;
using Xend.CRM.Core.DataAccessLayer.Repository;
using Xend.CRM.Core.Logger;
using Xend.CRM.Core.MessageBroker;
using Xend.CRM.Core.ServiceLayer.Bootstrapper;
using Xend.CRM.DataAccessLayer.Repository;
using Xend.CRM.ModelLayer.Appsetting;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ServiceLayer.Bootstrapper;
using Xend.CRM.ServiceLayer.Logger;
using Xend.CRM.ServiceLayer.MessageBroker;

namespace Xend.CRM.WebApi.Extensions
{
    /// <summary>
    /// IServiceCollection class extensions for use in Startup.cs to register classes for Dependency injection
    /// </summary>
    public static class DIRegistrationExtension
    {
        private static IServiceCollection ConfigureBootstrappers(this IServiceCollection services)
        {
            services.AddTransient<IStartManager, StartManager>();
            services.AddTransient<IStopManager, StopManager>();
            services.AddTransient<ILoggerManager, LoggerManager>();

            services.AddTransient<IMessageSender, MessageSender>();


            services.AddScoped<IUnitOfWork, UnitOfWork<XendDbContext>>();




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
