using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

using Castle.MicroKernel.Registration;
using Castle.Windsor;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;

using Translation.Client.Web.Controllers;
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
        public IWindsorContainer Container { get; set; }

        protected Mock<IWebHostEnvironment> MockHostingEnvironment { get; set; }

        public Mock<IOrganizationService> MockOrganizationService { get; set; }
        public Mock<IIntegrationService> MockIntegrationService { get; set; }
        public Mock<IAdminService> MockAdminService { get; set; }
        public Mock<ILanguageService> MockLanguageService { get; set; }
        public Mock<IProjectService> MockProjectService { get; set; }
        public Mock<ILabelService> MockLabelService { get; set; }
        public Mock<IJournalService> MockJournalService { get; set; }
        public Mock<ITranslationProviderService> MockTranslationProviderService { get; set; }

        public Mock<ITextTranslateIntegration> MockTextTranslateIntegration { get; set; }

        protected void InitializeComponents()
        {
            Container = new WindsorContainer();

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

        public void ConfigureIocContainer()
        {
            Container.Register(Component.For<IWebHostEnvironment>().Instance(MockHostingEnvironment.Object).LifestyleTransient());

            Container.Register(Component.For<IOrganizationService>().Instance(MockOrganizationService.Object).LifestyleTransient());
            Container.Register(Component.For<IIntegrationService>().Instance(MockIntegrationService.Object).LifestyleTransient());
            Container.Register(Component.For<ILanguageService>().Instance(MockLanguageService.Object).LifestyleTransient());
            Container.Register(Component.For<IAdminService>().Instance(MockAdminService.Object).LifestyleTransient());
            Container.Register(Component.For<IProjectService>().Instance(MockProjectService.Object).LifestyleTransient());
            Container.Register(Component.For<ILabelService>().Instance(MockLabelService.Object).LifestyleTransient());
            Container.Register(Component.For<IJournalService>().Instance(MockJournalService.Object).LifestyleTransient());
            Container.Register(Component.For<ITranslationProviderService>().Instance(MockTranslationProviderService.Object).LifestyleTransient());
            Container.Register(Component.For<ITextTranslateIntegration>().Instance(MockTextTranslateIntegration.Object).LifestyleTransient());
        }

        protected void Refresh()
        {
            InitializeComponents();
            ConfigureIocContainer();
            SetupCurrentUser();

            Container.Register(Component.For<AdminController>().LifestyleTransient());
            Container.Register(Component.For<DataController>().LifestyleTransient());
            Container.Register(Component.For<HomeController>().LifestyleTransient());
            Container.Register(Component.For<IntegrationController>().LifestyleTransient());
            Container.Register(Component.For<LabelController>().LifestyleTransient());
            Container.Register(Component.For<LanguageController>().LifestyleTransient());
            Container.Register(Component.For<OrganizationController>().LifestyleTransient());
            Container.Register(Component.For<ProjectController>().LifestyleTransient());
            Container.Register(Component.For<TokenController>().LifestyleTransient());
            Container.Register(Component.For<UserController>().LifestyleTransient());
            Container.Register(Component.For<TranslationProviderController>().LifestyleTransient());

            Container.Register(Component.For<CacheManager>());
        }

        private void SetupCurrentUser()
        {
            MockOrganizationService.Setup(x => x.GetCurrentUser(It.IsAny<CurrentUserRequest>())).Returns(FakeModelTestHelper.GetOrganizationOneCurrentUserOne());
        }

        public static void SetControllerContext(Controller controller)
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