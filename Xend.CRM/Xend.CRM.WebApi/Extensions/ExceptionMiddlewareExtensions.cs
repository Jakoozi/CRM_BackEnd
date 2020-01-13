using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using Xend.CRM.Core.Logger;
using Xend.CRM.ModelLayer.Enums;
using Xend.CRM.ModelLayer.Exceptions;

namespace Xend.CRM.WebApi.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");

                        if (contextFeature.Error.GetType() == typeof(InvalidOperationException))
                        {

                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                Status = ResponseStatus.AppError,
                                Message = contextFeature.Error.Message,
                            }.ToString());

                        }
                        else
                        {
                            await context.Response.WriteAsync(new ErrorDetails
                            {
                                Status = ResponseStatus.FatalError,
                                //Message = "Oops, Something Went Wrong"
                                Message = contextFeature.Error.Message,

                            }.ToString());
                        }
                    }
                });
            });
        }
    }

}
