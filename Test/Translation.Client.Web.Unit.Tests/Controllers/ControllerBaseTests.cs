using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;

using Translation.Client.Web.Controllers;
using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Unit.Tests.TestHelpers;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.User;
using Translation.Common.Tests.TestFakes;
using Translation.Service.Managers;

using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Controllers
{
    public class ControllerBaseTests
    {
        private ContainerBuilder Builder { get; set; }
        protected IContainer Container { get; set; }

        protected Mock<IWebHostEnvironment> MockHostingEnvironment { get; set; }

        protected Mock<IOrganizationService> MockOrganizationService { get; set; }
        protected Mock<IIntegrationService> MockIntegrationService { get; set; }
        protected Mock<IAdminService> MockAdminService { get; set; }
        protected Mock<ILanguageService> MockLanguageService { get; set; }
        protected Mock<IProjectService> MockProjectService { get; set; }
        protected Mock<ILabelService> MockLabelService { get; set; }
        protected Mock<IJournalService> MockJournalService { get; set; }
        protected Mock<ITranslationProviderService> MockTranslationProviderService { get; set; }

        protected Mock<ITextTranslateIntegration> MockTextTranslateIntegration { get; set; }

        protected void InitializeComponents()
        {
            Builder = new ContainerBuilder();

            MockHostingEnvironment = new Mock<IWebHostEnvironment>();

            #region Services
            MockOrganizationService = new Mock<IOrganizationService>();
            MockIntegrationService = new Mock<IIntegrationService>();
            MockAdminService = new Mock<IAdminService>();
            MockLanguageService = new Mock<ILanguageService>();
            MockProjectService = new Mock<IProjectService>();
            MockLabelService = new Mock<ILabelService>();
            MockJournalService = new Mock<IJournalService>();
            MockTranslationProviderService = new Mock<ITranslationProviderService>();
            #endregion

            MockTextTranslateIntegration = new Mock<ITextTranslateIntegration>();
        }

        private void ConfigureIocContainer()
        {
            Builder.RegisterInstance(MockHostingEnvironment.Object).As<IWebHostEnvironment>();

            Builder.RegisterInstance(MockOrganizationService.Object).As<IOrganizationService>();
            Builder.RegisterInstance(MockIntegrationService.Object).As<IIntegrationService>();
            Builder.RegisterInstance(MockAdminService.Object).As<IAdminService>();
            Builder.RegisterInstance(MockLabelService.Object).As<ILabelService>();
            Builder.RegisterInstance(MockLanguageService.Object).As<ILanguageService>();
            Builder.RegisterInstance(MockProjectService.Object).As<IProjectService>();
            Builder.RegisterInstance(MockJournalService.Object).As<IJournalService>();
            Builder.RegisterInstance(MockTranslationProviderService.Object).As<ITranslationProviderService>();

            Builder.RegisterInstance(MockTextTranslateIntegration.Object).As<ITextTranslateIntegration>();

        }

        protected void Refresh()
        {
            InitializeComponents();
            ConfigureIocContainer();
            SetupCurrentUser();

            Builder.RegisterType<AdminController>();
            Builder.RegisterType<DataController>();
            Builder.RegisterType<HomeController>();
            Builder.RegisterType<IntegrationController>();
            Builder.RegisterType<LabelController>();
            Builder.RegisterType<LanguageController>();
            Builder.RegisterType<OrganizationController>();
            Builder.RegisterType<ProjectController>();
            Builder.RegisterType<TokenController>();
            Builder.RegisterType<UserController>();
            Builder.RegisterType<TranslationProviderController>();

            Builder.RegisterType<CacheManager>();

            Builder.RegisterType<AdminMapper>();
            Builder.RegisterType<IntegrationMapper>();
            Builder.RegisterType<LabelMapper>();
            Builder.RegisterType<LanguageMapper>();
            Builder.RegisterType<OrganizationMapper>();
            Builder.RegisterType<ProjectMapper>();
            Builder.RegisterType<TranslationProviderMapper>();
            Builder.RegisterType<UserMapper>();

            Container = Builder.Build();
        }

        private void SetupCurrentUser()
        {
            MockOrganizationService.Setup(x => x.GetCurrentUser(It.IsAny<CurrentUserRequest>())).Returns(FakeModelTestHelper.GetOrganizationOneCurrentUserOne());
        }

        protected static void SetControllerContext(Controller controller)
        {
            var authenticationServiceMock = new Mock<IAuthenticationService>();
            authenticationServiceMock.Setup(a => a.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                                     .Returns(Task.CompletedTask);

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock.Setup(s => s.GetService(typeof(IAuthenticationService)))
                               .Returns(authenticationServiceMock.Object);

            var fakeClaimsPrincipal = new FakeClaimsPrincipal(new Claim(ClaimTypes.Name, OrganizationOneUserOneName), new Claim(ClaimTypes.Email, OrganizationOneUserOneEmail));

            var controllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = fakeClaimsPrincipal,
                    RequestServices = serviceProviderMock.Object,
                    Connection = { RemoteIpAddress = IPAddress.Parse(IpOne) }
                },
                RouteData = new RouteData(),
                ActionDescriptor = new ControllerActionDescriptor()
            };

            controller.ControllerContext = controllerContext;
            controller.TempData = new Mock<ITempDataDictionary>().Object;
        }
    }
}