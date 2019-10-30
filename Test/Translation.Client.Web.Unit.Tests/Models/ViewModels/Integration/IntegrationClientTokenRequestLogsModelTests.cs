using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Integration
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
