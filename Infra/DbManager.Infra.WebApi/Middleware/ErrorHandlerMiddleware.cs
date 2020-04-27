using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace DbManager.Infra.WebApi.Middleware
{
    /// <summary>
    /// Handling runtime exceptions middleware
    /// </summary>
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var code = (int) HttpStatusCode.InternalServerError;
                var error = JsonConvert.SerializeObject(new {message = ex.Message, code});
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = code;
                await context.Response.WriteAsync(error);
            }
        }
    }
}