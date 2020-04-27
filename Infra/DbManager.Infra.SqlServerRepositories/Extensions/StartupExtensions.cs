using DbManager.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DbManager.Infra.SqlServerRepositories.Extensions
{
    public static class StartupExtensions
    {
        public static void AddMsSqlRepositories(this IServiceCollection services)
        {
            services.AddTransient<ISchemaRepository, MsSqlSchemaRepository>();
        }
    }
}