using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class UserLoginLogListModelTests
    {
        public UserLoginLogListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUserLoginLogListModel();
        }

        [Test]
        public void UserLoginLogListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_login_log_list_title");
        }
    }
}