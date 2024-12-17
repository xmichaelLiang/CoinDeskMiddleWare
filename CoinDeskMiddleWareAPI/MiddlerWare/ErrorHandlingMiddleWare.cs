using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace CoinDeskMiddleWareAPI.MiddlerWare
{
    public class ErrorHandlingMiddleWare
    {
         
            private readonly RequestDelegate _next;
           private readonly ILogger<ErrorHandlingMiddleWare> _logger;
            private readonly IStringLocalizer _localizer;
            public ErrorHandlingMiddleWare(RequestDelegate next, ILogger<ErrorHandlingMiddleWare> logger,IStringLocalizerFactory localizerFactory)
            {
                _next = next;
                _logger = logger;
                _localizer = localizerFactory.Create("Localization", typeof(Program).Assembly.GetName().Name) ;
            }

            public async Task Invoke(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An unhandled exception has occurred. Request path: {Path}, Query string: {QueryString}", context.Request.Path, context.Request.QueryString);
                    await HandleExceptionAsync(context, ex);
                }
            }

            private  Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                var code = HttpStatusCode.InternalServerError; // 500 if unexpected
                var apiResultModel = new
                {
                    code = code.ToString(),
                    message = _localizer["UnexpectedError"],
                    data = (object)null
                };
                var result = Newtonsoft.Json.JsonConvert.SerializeObject(apiResultModel );
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                return context.Response.WriteAsync(result);
            }
    }
}