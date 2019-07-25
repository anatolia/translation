using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Integration
{
    [TestFixture]
    public class IntegrationClientTokenRequestLogsModelTests
    {
        public IntegrationClientTokenRequestLogsModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetIntegrationClientTokenRequestLogsModel(UidOne);
        }

        [Test]
        public void IntegrationClientTokenRequestLogsModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "token_request_logs_title");
        }

        [Test]
        public void IntegrationClientTokenRequestLogsModel_Parameters()
        {
            SystemUnderTest.IntegrationClientUid.ShouldBe(UidOne);
        }
    }
}
