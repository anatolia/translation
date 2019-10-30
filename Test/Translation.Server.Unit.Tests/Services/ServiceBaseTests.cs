using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Moq;

using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Server.Unit.Tests.RepositorySetupHelpers;
using Translation.Service;
using Translation.Service.Managers;

namespace Translation.Server.Unit.Tests.Services
{
    public class ServiceBaseTests
    {
        public IWindsorContainer Container { get; set; }

        protected Mock<IIntegrationClientRepository> MockIntegrationClientRepository { get; set; }
        protected Mock<IIntegrationRepository> MockIntegrationRepository { get; set; }
        protected Mock<IJournalRepository> MockJournalRepository { get; set; }
        protected Mock<ILabelRepository> MockLabelRepository { get; set; }
        protected Mock<ILabelTranslationRepository> MockLabelTranslationRepository { get; set; }
        protected Mock<ILanguageRepository> MockLanguageRepository { get; set; }
        protected Mock<IOrganizationRepository> MockOrganizationRepository { get; set; }
        protected Mock<IProjectRepository> MockProjectRepository { get; set; }
        protected Mock<ISendEmailLogRepository> MockSendEmailLogRepository { get; set; }
        protected Mock<ITokenRepository> MockTokenRepository { get; set; }
        protected Mock<ITokenRequestLogRepository> MockTokenRequestLogRepository { get; set; }
        protected Mock<IUserLoginLogRepository> MockUserLoginLogRepository { get; set; }
        protected Mock<IUserRepository> MockUserRepository { get; set; }
        protected Mock<ITranslationProviderRepository> MockTranslationProviderRepository { get; set; }

        protected Mock<ILabelUnitOfWork> MockLabelUnitOfWork { get; set; }
        protected Mock<ILogOnUnitOfWork> MockLogOnUnitOfWork { get; set; }
        protected Mock<IProjectUnitOfWork> MockProjectUnitOfWork { get; set; }
        protected Mock<ISignUpUnitOfWork> MockSignUpUnitOfWork { get; set; }

        protected Mock<ITextTranslateIntegration> MockTextTranslateIntegration { get; set; }

        protected void InitializeComponents()
        {
            Container = new WindsorContainer();

            #region Repository
            MockIntegrationClientRepository = new Mock<IIntegrationClientRepository>();
            MockIntegrationRepository = new Mock<IIntegrationRepository>();
            MockJournalRepository = new Mock<IJournalRepository>();
            MockLabelRepository = new Mock<ILabelRepository>();
            MockLabelTranslationRepository = new Mock<ILabelTranslationRepository>();
            MockLanguageRepository = new Mock<ILanguageRepository>();
            MockOrganizationRepository = new Mock<IOrganizationRepository>();
            MockProjectRepository = new Mock<IProjectRepository>();
            MockSendEmailLogRepository = new Mock<ISendEmailLogRepository>();
            MockTokenRepository = new Mock<ITokenRepository>();
            MockTokenRequestLogRepository = new Mock<ITokenRequestLogRepository>();
            MockUserLoginLogRepository = new Mock<IUserLoginLogRepository>();
            MockUserRepository = new Mock<IUserRepository>();
            MockTranslationProviderRepository = new Mock<ITranslationProviderRepository>();
            #endregion

            #region UnitOfWork
            MockLabelUnitOfWork = new Mock<ILabelUnitOfWork>();
            MockLogOnUnitOfWork = new Mock<ILogOnUnitOfWork>();
            MockProjectUnitOfWork = new Mock<IProjectUnitOfWork>();
            MockSignUpUnitOfWork = new Mock<ISignUpUnitOfWork>();
            #endregion

            MockTextTranslateIntegration = new Mock<ITextTranslateIntegration>();
        }

        public void ConfigureIocContainer()
        {

            Container.Register(Component.For<CryptoHelper>());

            #region Repository
            Container.Register(Component.For<IIntegrationClientRepository>().Instance(MockIntegrationClientRepository.Object));
            Container.Register(Component.For<IIntegrationRepository>().Instance(MockIntegrationRepository.Object));
            Container.Register(Component.For<IJournalRepository>().Instance(MockJournalRepository.Object));
            Container.Register(Component.For<ILabelRepository>().Instance(MockLabelRepository.Object));
            Container.Register(Component.For<ILabelTranslationRepository>().Instance(MockLabelTranslationRepository.Object));
            Container.Register(Component.For<ILanguageRepository>().Instance(MockLanguageRepository.Object));
            Container.Register(Component.For<IOrganizationRepository>().Instance(MockOrganizationRepository.Object));
            Container.Register(Component.For<IProjectRepository>().Instance(MockProjectRepository.Object));
            Container.Register(Component.For<ISendEmailLogRepository>().Instance(MockSendEmailLogRepository.Object));
            Container.Register(Component.For<ITokenRepository>().Instance(MockTokenRepository.Object));
            Container.Register(Component.For<ITokenRequestLogRepository>().Instance(MockTokenRequestLogRepository.Object));
            Container.Register(Component.For<IUserLoginLogRepository>().Instance(MockUserLoginLogRepository.Object));
            Container.Register(Component.For<IUserRepository>().Instance(MockUserRepository.Object));
            Container.Register(Component.For<ITranslationProviderRepository>().Instance(MockTranslationProviderRepository.Object));
            #endregion

            Container.Register(Component.For<ITextTranslateIntegration>().Instance(MockTextTranslateIntegration.Object));

            #region UnitOfWork
            Container.Register(Component.For<ILabelUnitOfWork>().Instance(MockLabelUnitOfWork.Object));
            Container.Register(Component.For<ILogOnUnitOfWork>().Instance(MockLogOnUnitOfWork.Object));
            Container.Register(Component.For<IProjectUnitOfWork>().Instance(MockProjectUnitOfWork.Object));
            Container.Register(Component.For<ISignUpUnitOfWork>().Instance(MockSignUpUnitOfWork.Object));
            #endregion
        }

        protected void Refresh()
        {
            InitializeComponents();
            ConfigureIocContainer();

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