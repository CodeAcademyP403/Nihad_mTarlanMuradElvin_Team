using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogezyTeamWork.Data;
using BlogezyTeamWork.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogezyTeamWork
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BlogezyDbContext>(options =>
            {
                options.UseLazyLoadingProxies().UseSqlServer(Configuration["ConnectionString:DefaultConnection"]);
            });


            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(15);//You can set Time   
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<BlogezyDbContext>()
                .AddDefaultTokenProviders();


            services.Configure<IdentityOptions>(options=> {

                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireDigit = true;

                options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
                options.User.RequireUniqueEmail = true;

            });

            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            else
                app.UseExceptionHandler("/Home/error");


            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "Admin",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
