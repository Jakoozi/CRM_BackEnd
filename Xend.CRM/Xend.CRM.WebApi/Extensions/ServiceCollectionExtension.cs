using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;
using Xend.CRM.ModelLayer.Appsetting;

namespace Xend.CRM.WebApi.Extensions
{
    /// <summary>
    /// IServiceCollection class extensions for use in the Startup.cs
    /// </summary>
    public static class ServiceCollectionExtension
    {
        private static void ConfigureAuth(this IServiceCollection services, AppSetting appSetting)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = appSetting.Auth.IdentityUrl;
                options.Audience = appSetting.Auth.Audience;
                options.RequireHttpsMetadata = appSetting.Auth.RequireHttps;
            });
        }

        public static IServiceCollection ConfigureCustomAppService(this IServiceCollection services, AppSetting appSetting)
        {
            ConfigureAuth(services, appSetting);
            return services;
        }
    }
}
