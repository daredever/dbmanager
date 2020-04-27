using DbManager.Infra.WebApi.Middleware;
using Microsoft.AspNetCore.Builder;

namespace DbManager.Infra.WebApi.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void UseMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}