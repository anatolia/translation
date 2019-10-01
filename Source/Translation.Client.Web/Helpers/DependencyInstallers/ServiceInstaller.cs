using System.Reflection;

using Autofac;

using Translation.Service;
using Translation.Service.Managers;

using Module = Autofac.Module;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class ServiceInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CacheManager>();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(OrganizationService)))
                   .Where(t => t.Name.EndsWith("Service"))
                   .AsImplementedInterfaces();
        }
    }
}