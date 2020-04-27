using DbManager.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DbManager.Infra.HttpServices.Extensions
{
    public static class StartupExtensions
    {
        public static void AddHttpServices(this IServiceCollection services)
        {
            services.AddTransient<IUserContextService, HttpUserContextService>();
        }
    }
}