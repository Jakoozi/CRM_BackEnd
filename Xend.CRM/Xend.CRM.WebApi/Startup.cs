
using AutoMapper;
using GreenPipes;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using Xend.CRM.Core.Logger;
using Xend.CRM.Core.ServiceLayer.Bootstrapper;
using Xend.CRM.ModelLayer.Appsetting;
using Xend.CRM.ModelLayer.Config_Model;
using Xend.CRM.ModelLayer.DbContexts;
using Xend.CRM.ModelLayer.Mappings;
using Xend.CRM.ServiceLayer.HostedServices;
using Xend.CRM.ServiceLayer.MessageBroker.DummyConsumer;
using Xend.CRM.WebApi.Extensions;

namespace Xend.CRM.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        private IConfiguration _Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            AppSetting appSetting = new AppSetting();
			AuthMessageSenderOptions senderOptions = new AuthMessageSenderOptions();
			_Configuration.GetSection("AppSetting").Bind(appSetting);
            services.ConfigureCustomAppService(appSetting);
            services.IoCRootResolver(appSetting);

			//sendgrid
			_Configuration.GetSection("SendGrid").Bind(senderOptions);
			services.AddSingleton(senderOptions);


			services.AddAutoMapper(

               opt =>
               {
                   opt.AllowNullCollections = true;


               }, new List<Type> { typeof(AutoMapperMappings) }, ServiceLifetime.Transient

               );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1).AddJsonOptions(
           options =>
           {
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
           }
           );

            services.RegisterSwagger();

            //Sql Server
            services.AddDbContext<XendDbContext>(options =>
            {
                options.EnableSensitiveDataLogging();
                options.UseLazyLoadingProxies().UseSqlServer(_Configuration.GetValue<string>("ConnectionString:DefaultConnection"), b => b.MigrationsAssembly("Xend.CRM.WebApi"));
                
                options.ConfigureWarnings(c => c.Log(CoreEventId.DetachedLazyLoadingWarning));

            }
            );

            string connectstring = _Configuration.GetValue<string>("ConnectionString:DefaultConnection");



            //Mass Transit Config
            services.AddScoped<DummyConsumer>();
            //services.AddMassTransit(c =>
            //{
            //    c.AddConsumer<DummyConsumer>();
            //});
            //services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(
            //                         cfg =>
            //                         {
            //                             IRabbitMqHost host = cfg.Host("localhost", "/", h => { });

            //                             cfg.ReceiveEndpoint(host, "xend-boilerplate", e =>
            //                             {
            //                                 e.PrefetchCount = 16;
            //                                 e.UseMessageRetry(x => x.Interval(2, 100));

            //                                 e.LoadFrom(provider);
            //                                 EndpointConvention.Map<DummyConsumer>(e.InputAddress);


            //                             });
            //                         }));

            //services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            //services.AddHostedService<BusService>();


            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                builder => {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });


            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowAllOrigins"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime, IStartManager startManager, IStopManager stopManager, ILoggerManager logger, AppSetting appSetting)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigureExceptionHandler(logger);

            app.UseSwagger(x =>
            {
                x.SerializeAsV2 = true;
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xend CRM Api V1");
            });
            app.UseCors("AllowAllOrigins");
            app.UseMvc();
            lifetime.ApplicationStarted.Register(() => startManager.Start());
            lifetime.ApplicationStopping.Register(() => stopManager.Stop());
        }
    }
}
