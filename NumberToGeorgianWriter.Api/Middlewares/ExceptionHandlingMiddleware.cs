using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace NumberToGeorgianWriter.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var problemDetails = new ProblemDetails
                {
                    Status = 500,
                    Title = ex.Message
                };

                httpContext.Response.StatusCode = 500;
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
