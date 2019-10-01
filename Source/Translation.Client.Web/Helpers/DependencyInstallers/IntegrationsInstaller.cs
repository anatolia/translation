using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Input;
using Autofac;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.ProjectModel;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Integrations;
using Translation.Integrations.Providers;

namespace Translation.Client.Web.Helpers.DependencyInstallers
{
    public class IntegrationsInstaller : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GoogleTranslateProvider>().As<ITextTranslateProvider>().WithMetadata("Name", nameof(GoogleTranslateProvider)).InstancePerDependency();
            builder.RegisterType<YandexTranslateProvider>().As<ITextTranslateProvider>().WithMetadata("Name", nameof(YandexTranslateProvider)).InstancePerDependency();
        }
    }
}