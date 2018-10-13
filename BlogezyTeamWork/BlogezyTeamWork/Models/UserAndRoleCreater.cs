using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogezyTeamWork.Models
{
    public class UserAndRoleCreater
    {
        public static async Task CreateAsync(IServiceScope scope,IdentityDbContext<AppUser> db)
        {
            if (!db.Users.Any() && !db.Roles.Any())
            {

                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                AppUser user = new AppUser()
                {
                    Email = "admin@gmail.com"
                };
                var identityResult =  await userManager.CreateAsync(user, "Admin");
                if (identityResult.Succeeded)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "Admin"});
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                    await userManager.AddToRoleAsync(user,"Admin");
                }

                await db.SaveChangesAsync();
            }
        }
    }
}
