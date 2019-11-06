using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using  static  Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class AdminInviteDoneModelTests
    {
        public AdminInviteDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAdminInviteDoneModel();
        }

        [Test]
        public void AdminInviteDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "admin_invite_done_tittle");
        }
    }
}