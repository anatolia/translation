using NUnit.Framework;

using Translation.Client.Web.Models.User;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public sealed class AllUserListModelTests
    {
        public AllUserListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAllUserListModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_list_title");
        }
    }
}