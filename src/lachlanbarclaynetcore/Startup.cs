using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using LachlanBarclayNet.DAO;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace lachlanbarclaynetcore
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
            services.AddResponseCaching();
            
            services.AddControllersWithViews(opt => {
                opt.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            }).AddRazorRuntimeCompilation();

            services.AddTransient<IPostDAO, PostDAO>();

            AppSettings appSettings = new AppSettings();
            Configuration.Bind(appSettings);
            services.AddSingleton(appSettings);

            services.AddDataProtection()
                .PersistKeysToFileSystem(new System.IO.DirectoryInfo(appSettings.DataProtectionDir))
                .SetApplicationName("SharedCookieApp");

            services.ConfigureApplicationCookie(options => {
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.Cookie.Path = "/";
            });

            services.AddAuthentication(appSettings.AuthScheme)
                .AddCookie(appSettings.AuthScheme, options =>
                {
                    options.Cookie.Name = ".AspNet.SharedCookie";
                });
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
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }

    public class AppSettings
    {
        public int NumberOfPostsOnHomeScreen { get; set; }
        public string AuthScheme { get; set; }
        public string DataProtectionDir { get; set; }
    }
}
