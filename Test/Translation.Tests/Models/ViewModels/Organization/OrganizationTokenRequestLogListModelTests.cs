using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public class OrganizationTokenRequestLogListModelTests
    {
        public OrganizationTokenRequestLogListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationTokenRequestLogListModel();
        }

        [Test]
        public void OrganizationTokenRequestLogListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "token_request_logs_title");
        }

        [Test]
        public void OrganizationTokenRequestLogListModel_Parameter()
        {
            SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
        }
    }
}