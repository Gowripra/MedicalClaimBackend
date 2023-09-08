using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Medical_Claim.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        public RequestDelegate requestDelegate;
        public ExceptionHandlingMiddleware(RequestDelegate requestDelegate)
        {
             this.requestDelegate = requestDelegate;
        }
        public async Task Invoke(HttpContext context,ILogger<ExceptionHandlingMiddleware> logger)
        {
           try
           {
             await requestDelegate(context);
           }
           catch (Exception ex)
           {
             await HandleException(context, ex,logger);
           }
        }
        private static Task HandleException(HttpContext context, Exception ex,ILogger<ExceptionHandlingMiddleware> logger)
        {
            logger.LogError(ex.ToString());
            var errorMessage = JsonConvert.SerializeObject(new { Message = ex.Message, Code = "GE" });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(errorMessage);
        }

    }
}
