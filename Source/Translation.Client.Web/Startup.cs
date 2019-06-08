using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Castle.Facilities.AspNetCore;
using Castle.Windsor;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.DependencyInstallers;

namespace Translation.Client.Web
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }
        private IHostingEnvironment CurrentEnvironment { get; set; }
        private WindsorContainer Container { get; } = new WindsorContainer();

        public Startup()
        {
            var configurationBuilder = new ConfigurationBuilder();
            Configuration = configurationBuilder.Build();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            CurrentEnvironment = env;

            var forwardingOptions = new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto };
            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardingOptions);

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHttpsRedirection();
            }
            else
            {
                app.UseExceptionHandler(x =>
                {
                    x.Run(async (context) =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                        var exceptionHelper = new ExceptionLogHelper();
                        exceptionHelper.LogException(feature.Error, CurrentEnvironment.ContentRootPath);

                        await Task.Run(() => context.Response.Redirect("/views/error.html"));
                    });
                });
            }

            app.UseAuthentication();
            app.UseMvc(x => { x.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            Container.Install(new SettingAndHelperInstaller());
            Container.Install(new FactoryAndMapperInstaller());
            Container.Install(new RepositoryAndUnitOfWorkInstaller());
            Container.Install(new ServiceInstaller());

            DbGeneratorHelper.Generate(Container);
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore(x =>
            {
                // SSL/TLS termination in production is managed at proxy server (NGINX)
                if (CurrentEnvironment.IsDevelopment())
                {
                    x.Filters.Add(new RequireHttpsAttribute());
                }

                x.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            })
            .AddViews()
            .AddJsonFormatters()
            .AddRazorViewEngine()
            .AddAuthorization();

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // we do this because we trust the network
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = ConstantHelper.APP_NAME;
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

            return services.AddWindsor(Container);
        }
    }
}
