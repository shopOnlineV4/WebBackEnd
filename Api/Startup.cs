using Api.Config;
using Api.Models;
using AutoMapper;
using Domain.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using UnitOfWork;
using WebApi.Config;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSetting"));
            services.AddControllers();
            services.AddDbContext<WebOnlineDbContext>(config =>
                config.UseSqlServer(Configuration.GetConnectionString("SqlServerConnection")));
            services.AddControllers().AddNewtonsoftJson();
            services.ConfigureCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("AppData", new OpenApiInfo()
                {
                    Title = "My api",
                    Version = "0.1"
                });
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddTransient<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.ConfigIdentity();
            services.ConfigAuthen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                   );
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/AppData/swagger.json", "Shop Online Api");
            });
        }
    }
}
