using NUnit.Framework;

using Translation.Client.Web.Models.User;
using Translation.Tests.TestHelpers;

namespace Translation.Tests.Client.Models.ViewModels.User
{
    [TestFixture]
    public sealed class AllUserListModelTests
    {
        public AllUserListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = FakeModelTestHelper.GetAllUserListModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_list_title");
        }
    }
}