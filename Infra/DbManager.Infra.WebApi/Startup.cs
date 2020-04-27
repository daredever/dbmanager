using DbManager.App.Services.Extensions;
using DbManager.Infra.SqlServerRepositories.Extensions;
using DbManager.Infra.WebApi.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DbManager.Infra.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private readonly string AllowOrigins = "allow_origins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddControllers();

            services.AddMsSqlRepositories();
            services.AddAppServices();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSwagger();

            // TODO move origins to config
            services.AddCors(options =>
            {
                options.AddPolicy(AllowOrigins,
                    buider =>
                    {
                        buider.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
                            .AllowCredentials();
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

            app.UseMiddlewares();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}