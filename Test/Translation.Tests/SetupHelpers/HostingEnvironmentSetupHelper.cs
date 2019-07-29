using Microsoft.AspNetCore.Hosting;

using Moq;

using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.SetupHelpers
{
    public static class HostingEnvironmentSetupHelper
    {
        public static void Setup_WebRootPath_Returns_WebRootPath(this Mock<IHostingEnvironment> environment)
        {
            environment.Setup(x => x.WebRootPath).Returns(GetWebRootPath());
        }

        public static void Verify_WebRootPath(this Mock<IHostingEnvironment> environment)
        {
            environment.Verify(x => x.WebRootPath);
        }
    }
}