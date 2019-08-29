using Castle.MicroKernel.Registration;

using Translation.Common.Contracts;
using Translation.Common.Tests;
using Translation.Service;
using Translation.Service.Managers;

namespace Translation.Server.Unit.Tests.Services
{
    public class ServiceBaseTests : BaseTests
    {
        protected new void Refresh()
        {
            base.Refresh();

            #region Services

            Container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>());
            Container.Register(Component.For<IIntegrationService>().ImplementedBy<IntegrationService>());
            Container.Register(Component.For<IJournalService>().ImplementedBy<JournalService>());
            Container.Register(Component.For<ILabelService>().ImplementedBy<LabelService>());
            Container.Register(Component.For<ILanguageService>().ImplementedBy<LanguageService>());
            Container.Register(Component.For<IOrganizationService>().ImplementedBy<OrganizationService>());
            Container.Register(Component.For<IProjectService>().ImplementedBy<ProjectService>());
            Container.Register(Component.For<ITranslationProviderService>().ImplementedBy<TranslationProviderService>());

            #endregion

            Container.Register(Component.For<CacheManager>());
        }
    }
}