using NUnit.Framework;

using Translation.Client.Web.Models.User;
using Translation.Tests.TestHelpers;

namespace Translation.Tests.Client.Models.ViewModels.User
{
    [TestFixture]
    public sealed class ChangePasswordDoneModelTests
    {
        public ChangePasswordDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = FakeModelTestHelper.GetChangePasswordDoneModel();
        }

        [Test]
        public void ChangePasswordDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_change_password_done_title");
        }
    }
}