using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class RepositoryAndUnitOfWorkInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromAssemblyContaining(typeof(IOrganizationRepository))
                                      .Where(x => x.Name.EndsWith("Repository"))
                                      .WithService.DefaultInterfaces());

            container.Register(Classes.FromAssemblyContaining(typeof(ISignUpUnitOfWork))
                                      .Where(x => x.Name.EndsWith("UnitOfWork"))
                                      .WithService.DefaultInterfaces());
        }
    }
}