using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class AdminAcceptInviteDoneModelTests
    {
        public AdminAcceptInviteDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAdminAcceptInviteDoneModel();
        }

        [Test]
        public void AdminAcceptInviteDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "admin_accept_invite_done_title");
        }
    }
}