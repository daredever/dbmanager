using DbManager.App.Services.Extensions;
using DbManager.Infra.HttpServices.Extensions;
using DbManager.Infra.SqlServerRepos.Extensions;
using DbManager.Infra.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DbManager.Infra.WebApi
{
    internal sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string AllowOrigins = "allow_origins";
        private const string UiHost = "UI_HOST";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddMsSqlRepositories();
            services.AddAppServices();
            services.AddHttpServices();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSwagger();

            services.AddCors(options =>
            {
                options.AddPolicy(AllowOrigins,
                    builder => builder
                        .WithOrigins(Configuration.GetValue<string>(key: UiHost))
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                );
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSession();
            app.UseHttpsRedirection();
            app.UseSwaggerMiddleware();
            app.UseRouting();
            app.UseCors(AllowOrigins);
            app.UseAuthorization();

            app.UseErrorMiddleware();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}