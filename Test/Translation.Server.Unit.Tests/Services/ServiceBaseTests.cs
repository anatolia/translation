using Castle.MicroKernel.Registration;

using Translation.Common.Contracts;
using Translation.Common.Tests;
using Translation.Data.Factories;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;
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

            #region Factory
            Container.Register(Component.For<IntegrationClientFactory>());
            Container.Register(Component.For<IntegrationFactory>());
            Container.Register(Component.For<JournalFactory>());
            Container.Register(Component.For<LabelFactory>());
            Container.Register(Component.For<LabelTranslationFactory>());
            Container.Register(Component.For<LanguageFactory>());
            Container.Register(Component.For<OrganizationFactory>());
            Container.Register(Component.For<ProjectFactory>());
            Container.Register(Component.For<SendEmailLogFactory>());
            Container.Register(Component.For<TokenFactory>());
            Container.Register(Component.For<TokenRequestLogFactory>());
            Container.Register(Component.For<UserFactory>());
            Container.Register(Component.For<UserLoginLogFactory>());
            Container.Register(Component.For<TranslationProviderFactory>());
            #endregion

            Container.Register(Component.For<CacheManager>());

            #region Setup For Cache
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_SelectById_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            #endregion
        }
    }
}