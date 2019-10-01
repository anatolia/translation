using Microsoft.AspNetCore.Hosting;

using Moq;

using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Common.Tests.SetupHelpers
{
    public static class WebHostEnvironmentSetupHelper
    {
        public static void Setup_WebRootPath_Returns_TestWebRootPath(this Mock<IWebHostEnvironment> environment)
        {
            environment.Setup(x => x.WebRootPath)
                       .Returns(GetTestWebRootPath());
        }

        public static void Setup_WebRootPath_Returns_TestWebRootPath_NotExists(this Mock<IWebHostEnvironment> environment)
        {
            environment.Setup(x => x.WebRootPath)
                       .Returns(GetTestWebRootPathNotExists());
        }

        public static void Verify_WebRootPath(this Mock<IWebHostEnvironment> environment)
        {
            environment.Verify(x => x.WebRootPath);
        }
    }
}