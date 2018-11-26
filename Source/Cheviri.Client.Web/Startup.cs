using System;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cheviri.Client.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(x =>
                {
                    x.Filters.Add(new RequireHttpsAttribute());
                    x.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                }).AddViews()
                .AddRazorViewEngine()
                .AddAuthorization();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = "cheviri";
                    x.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                    x.Cookie.SameSite = SameSiteMode.Strict;
                    x.Cookie.HttpOnly = true;
                    x.Cookie.IsEssential = true;
                    x.SlidingExpiration = true;
                    x.ExpireTimeSpan = TimeSpan.FromHours(8);
                    x.LoginPath = "/User/LogOn";
                    x.LogoutPath = "/User/LogOff";
                    x.AccessDeniedPath = "/User/AccessDenied";
                });
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(x =>
                {
                    x.UseStaticFiles();
                    x.Use((context, next) =>
                    {
                        context.Request.Path = new PathString("/views/error.html");
                        return next();
                    });
                });
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(x => { x.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });
        }
    }
}
