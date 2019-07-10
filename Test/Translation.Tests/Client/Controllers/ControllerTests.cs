using System;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;

using Moq;
using Castle.MicroKernel.Registration;

using Translation.Client.Web.Controllers;
using Translation.Common.Contracts;
using Translation.Common.Models.Requests.User;
using Translation.Tests.Common;
using Translation.Tests.TestFakes;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Client.Controllers
{
    public class ControllerTests : BaseTests
    {
        public Mock<IHostingEnvironment> MockHostingEnvironment { get; }
        public Mock<IOrganizationService> MockOrganizationService { get; }
        public Mock<IIntegrationService> MockIntegrationService { get; }
        public Mock<IAdminService> MockAdminService { get; }
        public Mock<ILanguageService> MockLanguageService { get; }
        public Mock<IProjectService> MockProjectService { get; }
        public Mock<ILabelService> MockLabelService { get; }

        public ControllerTests()
        {
            MockHostingEnvironment = new Mock<IHostingEnvironment>();
            MockOrganizationService = new Mock<IOrganizationService>();
            MockIntegrationService = new Mock<IIntegrationService>();
            MockAdminService = new Mock<IAdminService>();
            MockLanguageService = new Mock<ILanguageService>();
            MockProjectService = new Mock<IProjectService>();
            MockLabelService = new Mock<ILabelService>();

            SetupCurrentUser();

            Container.Register(Component.For<IHostingEnvironment>().Instance(MockHostingEnvironment.Object).LifestyleTransient());
            Container.Register(Component.For<IOrganizationService>().Instance(MockOrganizationService.Object).LifestyleTransient());
            Container.Register(Component.For<IIntegrationService>().Instance(MockIntegrationService.Object).LifestyleTransient());
            Container.Register(Component.For<ILanguageService>().Instance(MockLanguageService.Object).LifestyleTransient());
            Container.Register(Component.For<IAdminService>().Instance(MockAdminService.Object).LifestyleTransient());
            Container.Register(Component.For<IProjectService>().Instance(MockProjectService.Object).LifestyleTransient());
            Container.Register(Component.For<ILabelService>().Instance(MockLabelService.Object).LifestyleTransient());

            Container.Register(Component.For<HomeController>().LifestyleTransient());
            Container.Register(Component.For<AdminController>().LifestyleTransient());
            Container.Register(Component.For<LanguageController>().LifestyleTransient());
            Container.Register(Component.For<UserController>().LifestyleTransient());
            Container.Register(Component.For<IntegrationController>().LifestyleTransient());
            Container.Register(Component.For<TokenController>().LifestyleTransient());
            Container.Register(Component.For<OrganizationController>().LifestyleTransient());
            Container.Register(Component.For<ProjectController>().LifestyleTransient());
            Container.Register(Component.For<LabelController>().LifestyleTransient());
        }

        private void SetupCurrentUser()
        {
            MockOrganizationService.Setup(x => x.GetCurrentUser(It.IsAny<CurrentUserRequest>())).Returns(GetOrganizationOneCurrentUserOne());
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