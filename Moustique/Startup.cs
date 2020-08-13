using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moustique.Context;
using Moustique.Models.Db;
using Moustique.Services;
using Moustique.Services.Interfaces;
using Moustique.Tools;

namespace Moustique
{
    
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup()
        {
            Configuration = AppVariableConfiguration.ConfigurationRoot();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var cs = Configuration.GetSection("SQL");
            services.AddDbContext<MoustiqueContext>(builder => builder.UseMySql(Configuration.GetConnectionString("SQL")));
            services.AddIdentity<User, IdentityRole<int>>().AddEntityFrameworkStores<MoustiqueContext>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddScoped<IAddressService, AddressService>();

            services.AddScoped<ILoggerService, LoggerService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/About");
            }

            app.UseStaticFiles();

            app.SeedAdminUser();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
