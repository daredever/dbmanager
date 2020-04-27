using DbManager.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("DbManager.App.Services.Tests")]

namespace DbManager.App.Services.Extensions
{
    public static class StartupExtensions
    {
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddTransient<IDbSchemaService, DbSchemaService>();
            services.AddTransient<IDbScriptsService, DbScriptsService>();
        }
    }
}