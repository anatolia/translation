using System;
using System.Collections.Generic;
using System.Configuration;

using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
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
            container.Register(Component.For<ITextTranslateProvider>().ImplementedBy<GoogleTranslateProvider>()
                .Named(nameof(GoogleTranslateProvider)).LifestyleTransient());

            container.Register(Component.For<ITextTranslateProvider>().ImplementedBy<YandexTranslateProvider>()
                .Named(nameof(YandexTranslateProvider)).LifestyleTransient());

            ITextTranslateProvider google = container.Resolve<ITextTranslateProvider>(nameof(GoogleTranslateProvider));
            ITextTranslateProvider yandex = container.Resolve<ITextTranslateProvider>(nameof(YandexTranslateProvider));
            ITextTranslateProvider[] translationProviders = new ITextTranslateProvider[2]{google,yandex} ;
            
            container.Register(Component.For<ITextTranslateIntegration>().ImplementedBy<TextTranslateIntegration>()
                .DependsOn(Dependency.OnValue(typeof(ITextTranslateProvider[]), translationProviders)).LifestyleTransient());
        }
    }
}