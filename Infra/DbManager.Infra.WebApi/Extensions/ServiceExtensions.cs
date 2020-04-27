using DbManager.App.Services;
using DbManager.Domain.Repositories;
using DbManager.Domain.Services;
using DbManager.Infra.SqlServerRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace DbManager.Infra.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IDbSchemaService, DbSchemaService>();
            services.AddTransient<IDbScriptsService, DbScriptsService>();
            services.AddTransient<IHttpContextService, HttpContextService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISchemaRepository, MsSqlSchemaRepository>();
        }
    }
}