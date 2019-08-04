using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Translation.Common.Contracts;
using Translation.Integrations;
using Translation.Integrations.Providers;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class IntegrationsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<IGoogleTranslateProvider>().ImplementedBy(typeof(GoogleTranslateProvider)).LifestyleTransient());
            container.Register(Component.For<IYandexTranslateProvider>().ImplementedBy(typeof(YandexTranslateProvider)).LifestyleTransient());
            container.Register(Component.For<ITextTranslateIntegration>().ImplementedBy(typeof(TextTranslateIntegration)).LifestyleTransient());
        }
    }
}