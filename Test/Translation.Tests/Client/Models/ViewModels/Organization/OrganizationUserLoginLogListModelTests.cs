using NUnit.Framework;

using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class OrganizationUserLoginLogListModelTests
    {
        public OrganizationUserLoginLogListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationUserLoginLogListModel();
        }

        [Test]
        public void OrganizationUserLoginLogListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_login_logs_title");
        }
    }
}