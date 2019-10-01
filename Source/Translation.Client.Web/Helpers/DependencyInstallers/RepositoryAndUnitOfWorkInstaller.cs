using System.Reflection;

using Autofac;

using Translation.Data.Repositories;
using Translation.Data.UnitOfWorks;

using Module = Autofac.Module;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class RepositoryAndUnitOfWorkInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(OrganizationRepository)))
                   .Where(t => t.Name.EndsWith("Repository"))
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(SignUpUnitOfWork)))
                   .Where(t => t.Name.EndsWith("UnitOfWork"))
                   .AsImplementedInterfaces();
        }
    }
}