using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moustique.Models.Db;
using Moustique.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moustique.Context
{
    public static class DataSeeder
    {
        public static IApplicationBuilder SeedAdminUser(this IApplicationBuilder app)
        {
            using var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var moustiqueContext = service.ServiceProvider.GetRequiredService<MoustiqueContext>();
            var userManager = service.ServiceProvider.GetRequiredService<UserManager<User>>();

            var configuration = AppVariableConfiguration.ConfigurationRoot();

            if (moustiqueContext.Users.Any()) return app;

            var user = new User()
            {
                UserName = configuration.GetSection("AdminUser:AdminLogin").Value,
                Name = configuration.GetSection("AdminUser:AdminName").Value,
                Email = configuration.GetSection("AdminUser:AdminEmail").Value
            };
            var userTask = userManager.CreateAsync(user, configuration.GetSection("AdminUser:AdminPassword").Value);
            Task.WaitAll(userTask);

            var roleTask = userManager.AddToRoleAsync(user, "admin");
            Task.WaitAll(roleTask);
            return app;
        }
    }
}
