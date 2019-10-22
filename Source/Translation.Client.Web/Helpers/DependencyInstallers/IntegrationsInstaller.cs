using Autofac;

using Translation.Integrations.Providers;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class IntegrationsInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleTranslateProvider>().InstancePerDependency();
            builder.RegisterType<YandexTranslateProvider>().InstancePerDependency();
        }
    }
}