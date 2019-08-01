using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Translation.Service;
using Translation.Service.Managers;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<CacheManager>());

            container.Register(Classes.FromAssemblyContaining(typeof(OrganizationService))
                                      .Where(x => x.Name.EndsWith("Service"))
                                      .WithService.DefaultInterfaces().LifestyleTransient());
        }
    }
}