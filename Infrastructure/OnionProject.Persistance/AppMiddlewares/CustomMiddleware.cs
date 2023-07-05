using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OnionProject.Domain.Entities;
using OnionProject.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace OnionProject.Persistance.AppMiddlewares
{

    public class ErrorInfo
    {
        public int StatusCode { get; set; } = 0;
        public string ErrorMessage { get; set; } = string.Empty;
    }


    public class ErrorHandler
    {
        private readonly RequestDelegate requestDelegate;

        public ErrorHandler(RequestDelegate requestDelegate)
        {
            this.requestDelegate = requestDelegate;

        }

        public async Task InvokeAsync(HttpContext context, OnionProjectDbContext dbContext)
        {


            context.Response.OnStarting(state =>
            {
                return Task.CompletedTask;
            },
            new
            {
                context,
            });

            Stream originalBody = context.Response.Body;

            string responseBody = "";

            try
            {
                using (var newStream = new MemoryStream())
                {
                    context.Response.Body = newStream;

                    await requestDelegate(context);
                    newStream.Position = 0;
                    responseBody = new StreamReader(newStream).ReadToEnd();

                    newStream.Position = 0;
                    await newStream.CopyToAsync(originalBody);
                }
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;

                var errorInfo = new ErrorInfo()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                var errorLog = new ErrorLogger()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorDetails = ex.Message,
                    LogDate = DateTime.Now
                };
                await dbContext.ErrorLogger.AddAsync(errorLog);
                await dbContext.SaveChangesAsync();


            }
            finally
            {
                if(context.Response.StatusCode != 200)
                {
                    var errorInfo = new ErrorInfo()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorMessage = responseBody,
                    };

                    var errorLog = new ErrorLogger()
                    {
                        StatusCode = context.Response.StatusCode,
                        ErrorDetails = responseBody,
                        LogDate = DateTime.Now
                    };
                    await dbContext.ErrorLogger.AddAsync(errorLog);
                    await dbContext.SaveChangesAsync();


                }
            }


        }
    }



    public static class MiddlewareRegistrationExtension
    {
        public static void UseAppException(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<ErrorHandler>();
        }
    }

}
