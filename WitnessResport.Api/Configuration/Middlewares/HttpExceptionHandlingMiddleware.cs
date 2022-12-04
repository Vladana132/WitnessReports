using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using WitnessReports.Model.HttpException;

namespace WitnessReports.Api.Configuration.Middlewares
{
    public class HttpExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public HttpExceptionHandlingMiddleware( RequestDelegate next)
        {
              _next  =  next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                if (exception is CustomNotFoundException)
                {
                    await HandleExceptionAsync(context, exception, HttpStatusCode.NotFound);
                }
                else
                {
                    await HandleExceptionAsync(context, exception, HttpStatusCode.InternalServerError);
                }
            }

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            await context.Response.WriteAsync(new ExceptionResponse()
            {
                StatusCode = context.Response.StatusCode,
                Message = exception.Message
            }.ToString());
        }
    }
}
