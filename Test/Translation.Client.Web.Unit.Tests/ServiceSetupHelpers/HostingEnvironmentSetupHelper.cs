using Microsoft.AspNetCore.Hosting;

using Moq;

using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Client.Web.Unit.Tests.ServiceSetupHelpers
{
    public static class HostingEnvironmentSetupHelper
    {
        public static void Setup_WebRootPath_Returns_TestWebRootPath(this Mock<IHostingEnvironment> environment)
        {
            environment.Setup(x => x.WebRootPath)
                       .Returns(GetTestWebRootPath());
        }

        public static void Setup_WebRootPath_Returns_TestWebRootPath_NotExists(this Mock<IHostingEnvironment> environment)
        {
            environment.Setup(x => x.WebRootPath)
                       .Returns(GetTestWebRootPathNotExists());
        }

        public static void Verify_WebRootPath(this Mock<IHostingEnvironment> environment)
        {
            environment.Verify(x => x.WebRootPath);
        }
    }
}