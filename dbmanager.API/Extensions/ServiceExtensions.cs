using System;
using dbmanager.Common.Repositories;
using dbmanager.Common.Services;
using Microsoft.Extensions.DependencyInjection;

namespace dbmanager.API.Exctensions
{
    public static class ServiceExtensions
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddTransient<IDbInfoService, DbInfoService>();
            services.AddTransient<IGenerateScriptService, GenerateScriptService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IDbInfoRepository, DbInfoRepository>();
        }
    }
}
