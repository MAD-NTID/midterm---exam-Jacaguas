using System;
using System.Threading.Tasks;
using Coinbase.Services;
using Microsoft.AspNetCore.Http;

namespace Coinbase.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILoggerManager logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next(context);
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status = 500;
            string msg = "Internal Error";
            
            //we want the response to output as json
            context.Response.ContentType = "application/json";
            string errorString = "";
            await context.Response.WriteAsync(errorString);
            if (exception is IUserErrorException)
            {
                UserErrorException error = (UserErrorException)exception;
                msg = error.GetMessage();
                status = error.GetStatusCode();


            }


        }
    }
}