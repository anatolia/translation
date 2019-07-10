using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Moq;
using Npgsql;

using StandardRepository;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service;
using Translation.Service.Managers;

namespace Translation.Tests.Common
{

    public class BaseTests
    {
        public IWindsorContainer Container { get; set; } = new WindsorContainer();

        protected Mock<CacheManager> MockCacheManager { get; }
        protected Mock<CryptoHelper> MockCryptoHelper { get; }

        protected Mock<IIntegrationClientRepository> MockIntegrationClientRepository { get; }
        protected Mock<IIntegrationRepository> MockIntegrationRepository { get; }
        protected Mock<IJournalRepository> MockJournalRepository { get; }
        protected Mock<ILabelRepository> MockLabelRepository { get; }
        protected Mock<ILabelTranslationRepository> MockLabelTranslationRepository { get; }
        protected Mock<ILanguageRepository> MockLanguageRepository { get; }
        protected Mock<IOrganizationRepository> MockOrganizationRepository { get; }
        protected Mock<IProjectRepository> MockProjectRepository { get; }
        protected Mock<ISendEmailLogRepository> MockSendEmailLogRepository { get; }
        protected Mock<ITokenRepository> MockTokenRepository { get; }
        protected Mock<ITokenRequestLogRepository> MockTokenRequestLogRepository { get; }
        protected Mock<IUserLoginLogRepository> MockUserLoginLogRepository { get; }
        protected Mock<IUserRepository> MockUserRepository { get; }
        
        protected Mock<ILabelUnitOfWork> MockLabelUnitOfWork { get; }
        protected Mock<ILogOnUnitOfWork> MockLogOnUnitOfWork { get; }
        protected Mock<IProjectUnitOfWork> MockProjectUnitOfWork { get; }
        protected Mock<ISignUpUnitOfWork> MockSignUpUnitOfWork { get; }

        protected BaseTests()
        {
            MockCacheManager = new Mock<CacheManager>();
            MockCryptoHelper = new Mock<CryptoHelper>();

            #region Repository

            MockIntegrationClientRepository = new Mock<IIntegrationClientRepository>();
            MockIntegrationRepository = new Mock<IIntegrationRepository>();
            MockJournalRepository = new Mock<IJournalRepository>();
            MockLabelRepository= new Mock<ILabelRepository>();
            MockLabelTranslationRepository = new Mock<ILabelTranslationRepository>();
            MockLanguageRepository = new Mock<ILanguageRepository>();
            MockOrganizationRepository = new Mock<IOrganizationRepository>();
            MockProjectRepository = new Mock<IProjectRepository>();
            MockSendEmailLogRepository = new Mock<ISendEmailLogRepository>();
            MockTokenRepository = new Mock<ITokenRepository>();
            MockTokenRequestLogRepository = new Mock<ITokenRequestLogRepository>();
            MockUserLoginLogRepository = new Mock<IUserLoginLogRepository>();
            MockUserRepository = new Mock<IUserRepository>();

            #endregion

            #region UnitOfWork
            MockLabelUnitOfWork = new Mock<ILabelUnitOfWork>();
            MockLogOnUnitOfWork = new Mock<ILogOnUnitOfWork>();
            MockProjectUnitOfWork = new Mock<IProjectUnitOfWork>();
            MockSignUpUnitOfWork = new Mock<ISignUpUnitOfWork>();
            #endregion

            ConfigureIocContainer();
        }

        public void ConfigureIocContainer()
        {
            Container.Register(Component.For<CryptoHelper>().Instance(MockCryptoHelper.Object));

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

            #endregion

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
            
            #endregion

            #region UnitOfWork

            Container.Register(Component.For<ILabelUnitOfWork>().Instance(MockLabelUnitOfWork.Object));
            Container.Register(Component.For<ILogOnUnitOfWork>().Instance(MockLogOnUnitOfWork.Object));
            Container.Register(Component.For<IProjectUnitOfWork>().Instance(MockProjectUnitOfWork.Object));
            Container.Register(Component.For<ISignUpUnitOfWork>().Instance(MockSignUpUnitOfWork.Object));

            #endregion

            #region registerType

            Container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>());
            Container.Register(Component.For<IIntegrationService>().ImplementedBy<IntegrationService>());
            Container.Register(Component.For<IJournalService>().ImplementedBy<JournalService>());
            Container.Register(Component.For<ILabelService>().ImplementedBy<LabelService>());
            Container.Register(Component.For<ILanguageService>().ImplementedBy<LanguageService>());
            Container.Register(Component.For<IOrganizationService>().ImplementedBy<OrganizationService>());
            Container.Register(Component.For<IProjectService>().ImplementedBy<ProjectService>());

            #endregion

            Container.Register(Component.For<CacheManager>());
        }
    }
}