using NUnit.Framework;

using Translation.Client.Web.Models.User;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public sealed class ChangePasswordDoneModelTests
    {
        public ChangePasswordDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetChangePasswordDoneModel();
        }

        [Test]
        public void ChangePasswordDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_change_password_done_title");
        }
    }
}