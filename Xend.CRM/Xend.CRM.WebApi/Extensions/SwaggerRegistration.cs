using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.IO;

namespace Xend.CRM.WebApi.Extensions
{
    public static class SwaggerRegistration
    {
        public static IServiceCollection RegisterSwagger(this IServiceCollection serviceCollection)
        {
            // Register the Swagger generator, defining one or more Swagger documents           

            serviceCollection.AddSwaggerGen(p =>
            {


                p.SwaggerDoc("v1", new OpenApiInfo { Title = "Xend CRM Api", Version = "v1" });
                p.DescribeAllEnumsAsStrings();
                Dictionary<string, IEnumerable<string>> security = new Dictionary<string, IEnumerable<string>>
            {
                    {"Bearer", new string[] { }},
            };

                OpenApiSecurityScheme openApiSecurityScheme = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,

                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                };

                OpenApiSecurityRequirement openAPiSecurityRequirement = new OpenApiSecurityRequirement();
                openAPiSecurityRequirement.Add(openApiSecurityScheme, new string[] { });

                p.AddSecurityDefinition("Bearer", openApiSecurityScheme);

                    //p.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    //{

                    //    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    //    Name = "Authorization",
                    //    In = "header",
                    //    Type = "apiKey"
                    //});

                    p.AddSecurityRequirement(openAPiSecurityRequirement);

            });

            return serviceCollection;
        }
    }
}
