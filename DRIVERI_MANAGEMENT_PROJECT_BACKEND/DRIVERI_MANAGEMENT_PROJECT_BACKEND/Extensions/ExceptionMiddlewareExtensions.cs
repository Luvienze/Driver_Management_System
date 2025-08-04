using Entities.ErrorModel;
using Entities.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Services.Contracts;
using System.Net;

namespace DRIVERI_MANAGEMENT_PROJECT_BACKEND.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app,
            ILoggerService logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature is not null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            PersonNotFoundException => StatusCodes.Status404NotFound,
                            ChiefNotFoundException => StatusCodes.Status404NotFound,
                            DriverNotFoundException => StatusCodes.Status404NotFound,
                            GarageNotFoundException => StatusCodes.Status404NotFound,
                            LineNotFoundException => StatusCodes.Status404NotFound,
                            RoleNotFoundException => StatusCodes.Status404NotFound,
                            RouteNotFoundException => StatusCodes.Status404NotFound,
                            TaskNotFoundException => StatusCodes.Status404NotFound,
                            VehicleNotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error.Message}");
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
