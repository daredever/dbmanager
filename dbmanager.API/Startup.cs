using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dbmanager.API.Middleware;
using dbmanager.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dbmanager.API
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

            services.AddRepositories();
            services.AddCommonServices();

            services.AddDistributedMemoryCache();
            services.AddSession();

            services.AddSwagger();

            // TODO move origins to config
            services.AddCors(options =>
            {
                options.AddPolicy(AllowOrigins,
                    buider =>
                    {
                        buider.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
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

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
