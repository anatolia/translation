using Microsoft.AspNetCore.Hosting;

using Moq;
using Castle.Windsor;
using Castle.MicroKernel.Registration;
using Translation.Common.Contracts;
using Translation.Common.Helpers;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Tests.SetupHelpers;

namespace Translation.Tests.Common
{

    public class BaseTests
    {
        public IWindsorContainer Container { get; set; }

        protected Mock<IHostingEnvironment> MockHostingEnvironment { get; set; }

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

        protected Mock<ILabelUnitOfWork> MockLabelUnitOfWork { get; set; }
        protected Mock<ILogOnUnitOfWork> MockLogOnUnitOfWork { get; set; }
        protected Mock<IProjectUnitOfWork> MockProjectUnitOfWork { get; set; }
        protected Mock<ISignUpUnitOfWork> MockSignUpUnitOfWork { get; set; }

        protected Mock<ITextTranslateIntegration> MockTextTranslateIntegration { get; set; }

        protected void Refresh()
        {
            InitializeComponents();
            ConfigureIocContainer();
        }

        protected void InitializeComponents()
        {
            Container = new WindsorContainer();

            MockHostingEnvironment = new Mock<IHostingEnvironment>();

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

            #endregion

            #region UnitOfWork
            MockLabelUnitOfWork = new Mock<ILabelUnitOfWork>();
            MockLogOnUnitOfWork = new Mock<ILogOnUnitOfWork>();
            MockProjectUnitOfWork = new Mock<IProjectUnitOfWork>();
            MockSignUpUnitOfWork = new Mock<ISignUpUnitOfWork>();
            #endregion

            #region Services

            #endregion

            MockTextTranslateIntegration = new Mock<ITextTranslateIntegration>();
        }

        public void ConfigureIocContainer()
        {

            Container.Register(Component.For<IHostingEnvironment>().Instance(MockHostingEnvironment.Object).LifestyleTransient());

            Container.Register(Component.For<CryptoHelper>());

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

            #region Setup For Cache
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_SelectById_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            #endregion

            Container.Register(Component.For<ITextTranslateIntegration>().Instance(MockTextTranslateIntegration.Object));
        }
    }
}