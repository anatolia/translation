using NUnit.Framework;

using Translation.Client.Web.Models.User;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.User
{
    [TestFixture]
    public sealed class InviteDoneModelTests
    {
        public InviteDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetInviteDoneModel();
        }

        [Test]
        public void InviteDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_invite_done_title");
        }
    }
}