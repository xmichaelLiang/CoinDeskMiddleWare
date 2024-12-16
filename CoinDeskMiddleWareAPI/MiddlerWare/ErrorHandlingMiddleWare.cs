using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CoinDeskMiddleWareAPI.MiddlerWare
{
    public class ErrorHandlingMiddleWare
    {
         
            private readonly RequestDelegate _next;
            public ErrorHandlingMiddleWare(RequestDelegate next)
            {
                _next = next;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }

            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                var code = HttpStatusCode.InternalServerError; // 500 if unexpected
                var apiResultModel = new
                {
                    code = code.ToString(),
                    message = exception.Message,
                    data = (object)null
                };
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(apiResultModel );
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }
    }
}