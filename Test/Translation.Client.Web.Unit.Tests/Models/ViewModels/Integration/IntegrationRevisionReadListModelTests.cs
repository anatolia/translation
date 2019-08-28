using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Integration
{
    [TestFixture]
    public class IntegrationRevisionReadListModelTests
    {
        public IntegrationRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetIntegrationRevisionReadListModel(UidOne, StringOne);
        }

        [Test]
        public void IntegrationRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "integration_revision_list_title");
        }

        [Test]
        public void IntegrationClientTokenRequestLogsModel_Parameters()
        {
            SystemUnderTest.IntegrationUid.ShouldBe(UidOne);
            SystemUnderTest.IntegrationName.ShouldBe(StringOne);
        }
    }
}
