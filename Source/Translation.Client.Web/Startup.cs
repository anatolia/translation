using System;
using System.Configuration;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.DependencyInstallers;
using Translation.Common.Contracts;
using Translation.Integrations;
using Translation.Integrations.Providers;
using Translation.Service.Managers;

namespace Translation.Client.Web
{
    public class Startup
    {
        public ILifetimeScope AutofacContainer { get; private set; }
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddResponseCaching();

            services.Configure<ForwardedHeadersOptions>(x =>
            {
                x.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                // we can do this when we trust the network
                x.KnownNetworks.Clear();
                x.KnownProxies.Clear();
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
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new SettingAndHelperInstaller());
            builder.RegisterModule(new FactoryAndMapperInstaller());
            builder.RegisterModule(new RepositoryAndUnitOfWorkInstaller());
            builder.RegisterModule(new ServiceInstaller());
            builder.RegisterModule(new IntegrationsInstaller());

            builder.Register<ITextTranslateIntegration>(x =>
            {
                var googleTranslateProvider = x.Resolve<GoogleTranslateProvider>();
                var yandexTranslateProvider = x.Resolve<YandexTranslateProvider>();
                var cacheManager = x.Resolve<CacheManager>();

                return new TextTranslateIntegration(cacheManager, googleTranslateProvider, yandexTranslateProvider);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            new DbGeneratorHelper().Generate(AutofacContainer, env.WebRootPath);

            var forwardingOptions = new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto };
            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();
            app.UseForwardedHeaders(forwardingOptions);

            app.UseStaticFiles();

            if (env.IsDevelopment()
                || ConfigurationManager.AppSettings["UseDeveloperExceptionPage"] == "true")
            {
                app.UseDeveloperExceptionPage();

                if (env.IsDevelopment())
                {
                    app.UseHttpsRedirection();
                }
            }
            else
            {
                app.UseExceptionHandler(x =>
                {
                    x.Run(async (context) =>
                    {
                        var feature = context.Features.Get<IExceptionHandlerPathFeature>();

                        var exceptionHelper = new ExceptionLogHelper();
                        exceptionHelper.LogException(feature.Error, env.ContentRootPath);

                        await Task.Run(() => context.Response.Redirect("/views/error.html"));
                    });
                });
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("label", "{controller=Home}/{action=Index}/{project?}/{label?}");
            });
        }
    }
}
