using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Autofac;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Data.Factories;

using Module = Autofac.Module;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class FactoryAndMapperInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var mappers = Assembly.GetAssembly(typeof(OrganizationMapper)).GetTypes().Where(x => x.IsPublic && x.Namespace != null && x.Namespace.Contains("Mappers")).ToList();
            RegisterInstance(builder, mappers);

            var factories = Assembly.GetAssembly(typeof(OrganizationFactory)).GetTypes().Where(x => x.IsPublic && x.Namespace != null && x.Namespace.Contains("Factories")).ToList();
            RegisterInstance(builder, factories);
        }

        private static void RegisterInstance(ContainerBuilder builder, List<Type> types)
        {
            for (var i = 0; i < types.Count; i++)
            {
                var type = types[i];
                builder.RegisterType(type);
            }
        }
    }
}