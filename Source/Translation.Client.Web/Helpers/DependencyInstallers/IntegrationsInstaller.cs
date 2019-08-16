using System.Configuration;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Integrations;
using Translation.Integrations.Providers;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class IntegrationsInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For<ITextTranslateProvider>().ImplementedBy(typeof(GoogleTranslateProvider)).LifestyleTransient());

            container.Register(Component.For<ITextTranslateProvider>().ImplementedBy(typeof(YandexTranslateProvider)).LifestyleTransient());

            container.Register(Component.For<ITextTranslateIntegration>().ImplementedBy(typeof(TextTranslateIntegration)).LifestyleTransient());
        }
    }
}