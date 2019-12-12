using Autofac;
using Moq;

using StandardUtils.Helpers;

using Translation.Common.Contracts;
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
        public ContainerBuilder Builder { get; set; }

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
            Builder = new ContainerBuilder();

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
            Builder.RegisterType<CryptoHelper>();

            #region Repositories
            Builder.RegisterInstance(MockIntegrationClientRepository.Object).As<IIntegrationClientRepository>();
            Builder.RegisterInstance(MockIntegrationRepository.Object).As<IIntegrationRepository>();
            Builder.RegisterInstance(MockJournalRepository.Object).As<IJournalRepository>();
            Builder.RegisterInstance(MockLabelRepository.Object).As<ILabelRepository>();
            Builder.RegisterInstance(MockLabelTranslationRepository.Object).As<ILabelTranslationRepository>();
            Builder.RegisterInstance(MockLanguageRepository.Object).As<ILanguageRepository>();
            Builder.RegisterInstance(MockOrganizationRepository.Object).As<IOrganizationRepository>();
            Builder.RegisterInstance(MockProjectRepository.Object).As<IProjectRepository>();
            Builder.RegisterInstance(MockSendEmailLogRepository.Object).As<ISendEmailLogRepository>();
            Builder.RegisterInstance(MockTokenRepository.Object).As<ITokenRepository>();
            Builder.RegisterInstance(MockTokenRequestLogRepository.Object).As<ITokenRequestLogRepository>();
            Builder.RegisterInstance(MockUserLoginLogRepository.Object).As<IUserLoginLogRepository>();
            Builder.RegisterInstance(MockUserRepository.Object).As<IUserRepository>();
            Builder.RegisterInstance(MockTranslationProviderRepository.Object).As<ITranslationProviderRepository>();
            #endregion

            Builder.RegisterInstance(MockTextTranslateIntegration.Object).As<ITextTranslateIntegration>();

            #region UnitOfWork

            Builder.RegisterInstance(MockLabelUnitOfWork.Object).As<ILabelUnitOfWork>();
            Builder.RegisterInstance(MockLogOnUnitOfWork.Object).As<ILogOnUnitOfWork>();
            Builder.RegisterInstance(MockProjectUnitOfWork.Object).As<IProjectUnitOfWork>();
            Builder.RegisterInstance(MockSignUpUnitOfWork.Object).As<ISignUpUnitOfWork>();
            
            #endregion
        }

        protected void Refresh()
        {
            InitializeComponents();
            ConfigureIocContainer();

            #region Services
            Builder.RegisterType<AdminService>().As<IAdminService>();
            Builder.RegisterType<IntegrationService>().As<IIntegrationService>();
            Builder.RegisterType<JournalService>().As<IJournalService>();
            Builder.RegisterType<LabelService>().As<ILabelService>();
            Builder.RegisterType<LanguageService>().As<ILanguageService>();
            Builder.RegisterType<OrganizationService>().As<IOrganizationService>();
            Builder.RegisterType<ProjectService>().As<IProjectService>();
            Builder.RegisterType<TranslationProviderService>().As<ITranslationProviderService>();
            #endregion

            #region Factory
            Builder.RegisterType<IntegrationClientFactory>();
            Builder.RegisterType<IntegrationFactory>();
            Builder.RegisterType<JournalFactory>();
            Builder.RegisterType<LabelFactory>();
            Builder.RegisterType<LabelTranslationFactory>();
            Builder.RegisterType<LanguageFactory>();
            Builder.RegisterType<OrganizationFactory>();
            Builder.RegisterType<ProjectFactory>();
            Builder.RegisterType<SendEmailLogFactory>();
            Builder.RegisterType<TokenFactory>();
            Builder.RegisterType<TokenRequestLogFactory>();
            Builder.RegisterType<UserFactory>();
            Builder.RegisterType<UserLoginLogFactory>();
            Builder.RegisterType<TranslationProviderFactory>();
            #endregion

            Builder.RegisterType<CacheManager>();

            #region Setup For Cache
            MockUserRepository.Setup_SelectById_Returns_OrganizationOneUserOne();
            MockUserRepository.Setup_Select_Returns_OrganizationOneUserOne();
            MockOrganizationRepository.Setup_SelectById_Returns_OrganizationOne();
            MockOrganizationRepository.Setup_Select_Returns_OrganizationOne();
            #endregion
        }

    }
}