using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Translation.Data.Factories;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class FactoryAndMapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining(typeof(OrganizationFactory))
                                      .Where(x => x.IsPublic && x.Namespace.Contains("Factories")));

        }
    }
}