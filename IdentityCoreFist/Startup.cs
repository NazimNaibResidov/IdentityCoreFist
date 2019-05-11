using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IdentityCoreFist.IdentityCore;
using IdentityCoreFist.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

namespace IdentityCoreFist
{
    public class Startup
    {

        private IConfiguration Configuration;
        public Startup(IConfiguration Configuration)
        {

            this.Configuration = Configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Identity")));

            //services.ConfigureApplicationCookie(opt => opt.LoginPath = "/Memeber/Login");
            services.AddIdentity<ApplicationUser, IdentityRole>(
                options =>
                {
                  
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 4;
                    
                }
               
                
            
            )
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddMvc();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseFileServer(new FileServerOptions()
            {
                FileProvider = new PhysicalFileProvider(
          Path.Combine(Directory.GetCurrentDirectory(), @"node_modules")),
                RequestPath = new PathString("/node_modules"),
                EnableDirectoryBrowsing = true
            });
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            
        }
    }
}
