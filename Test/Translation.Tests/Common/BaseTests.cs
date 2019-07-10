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

        protected Mock<IStandardRepository<Organization, NpgsqlConnection>> MockOrganizationRepository { get; }
        protected Mock<IStandardRepository<User, NpgsqlConnection>> MockUserRepository { get; }
        protected Mock<ISendEmailLogRepository> MockSendEmailLogRepository { get; }
        protected Mock<IJournalRepository> MockJournalRepository { get; }
        protected Mock<IUserLoginLogRepository> MockUserLoginLogRepository { get; }
        protected Mock<ITokenRepository> MockTokenRepository { get; }
        protected Mock<ITokenRequestLogRepository> MockTokenRequestLogRepository { get; }
        protected Mock<IIntegrationRepository> MockIntegrationRepository { get; }
        protected Mock<IIntegrationClientRepository> MockIntegrationClientRepository { get; }

        protected Mock<ISignUpUnitOfWork> MockSignUpUnitOfWork { get; }
        protected Mock<ILogOnUnitOfWork> MockLogOnUnitOfWork { get; }

        protected UserFactory UserFactory { get; }
        protected OrganizationFactory OrganizationFactory { get; }

        protected BaseTests()
        {
            MockCacheManager = new Mock<CacheManager>();
            MockCryptoHelper = new Mock<CryptoHelper>();

            #region Repository

            MockTokenRepository = new Mock<ITokenRepository>();
            MockJournalRepository = new Mock<IJournalRepository>();
            MockUserRepository = new Mock<IStandardRepository<User, NpgsqlConnection>>();
            MockIntegrationRepository = new Mock<IIntegrationRepository>();
            MockUserLoginLogRepository = new Mock<IUserLoginLogRepository>();
            MockTokenRequestLogRepository = new Mock<ITokenRequestLogRepository>();
            MockOrganizationRepository = new Mock<IStandardRepository<Organization, NpgsqlConnection>>();
            MockSendEmailLogRepository = new Mock<ISendEmailLogRepository>();
            MockIntegrationClientRepository = new Mock<IIntegrationClientRepository>();

            #endregion

            #region Factory
            UserFactory = new UserFactory();
            OrganizationFactory = new OrganizationFactory();
            #endregion

            #region UnitOfWork
            MockSignUpUnitOfWork = new Mock<ISignUpUnitOfWork>();
            MockLogOnUnitOfWork = new Mock<ILogOnUnitOfWork>();
            #endregion

            ConfigureIocContainer();
        }

        public void ConfigureIocContainer()
        {
            Container.Register(Component.For<CacheManager>().Instance(MockCacheManager.Object));
            Container.Register(Component.For<CryptoHelper>().Instance(MockCryptoHelper.Object));

            #region Repository

            Container.Register(Component.For<IStandardRepository<Organization, NpgsqlConnection>>().Instance(MockOrganizationRepository.Object));
            Container.Register(Component.For<IStandardRepository<User, NpgsqlConnection>>().Instance(MockUserRepository.Object));

            Container.Register(Component.For<IUserLoginLogRepository>().Instance(MockUserLoginLogRepository.Object));
            Container.Register(Component.For<IJournalRepository>().Instance(MockJournalRepository.Object));
            Container.Register(Component.For<ITokenRequestLogRepository>().Instance(MockTokenRequestLogRepository.Object));

            Container.Register(Component.For<ISendEmailLogRepository>().Instance(MockSendEmailLogRepository.Object));

            Container.Register(Component.For<ITokenRepository>().Instance(MockTokenRepository.Object));
            Container.Register(Component.For<IIntegrationRepository>().Instance(MockIntegrationRepository.Object));
            Container.Register(Component.For<IIntegrationClientRepository>().Instance(MockIntegrationClientRepository.Object));

            #endregion

            #region Factory

            Container.Register(Component.For<OrganizationFactory>().Instance(OrganizationFactory));
            Container.Register(Component.For<UserFactory>().Instance(UserFactory));

            #endregion

            #region UnitOfWork

            Container.Register(Component.For<ISignUpUnitOfWork>().Instance(MockSignUpUnitOfWork.Object));
            Container.Register(Component.For<ILogOnUnitOfWork>().Instance(MockLogOnUnitOfWork.Object));

            #endregion

            #region registerType

            Container.Register(Component.For<IAdminService>().ImplementedBy<AdminService>());
            Container.Register(Component.For<IIntegrationService>().ImplementedBy<IntegrationService>());
            Container.Register(Component.For<IOrganizationService>().ImplementedBy<OrganizationService>());

            #endregion
        }
    }
}