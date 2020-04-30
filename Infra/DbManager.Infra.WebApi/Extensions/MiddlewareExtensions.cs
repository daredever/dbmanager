using DbManager.Infra.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DbManager.Infra.WebApi.Extensions
{
    internal static class MiddlewareExtensions
    {
        public static void UseErrorMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}