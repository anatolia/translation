using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using Translation.Client.Web.Models.User;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Project
{
    [TestFixture]
    public class UserRevisionReadListModelTests
    {
        public UserRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUserRevisionReadListModel();
        }

        [Test]
        public void UserRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_revision_list_title");
        }

        [Test]
        public void UserRevisionReadListModel_Parameter()
        {
          SystemUnderTest.UserUid.ShouldBe(UidOne);
          SystemUnderTest.UserName.ShouldBe(StringOne);
        }
    }
}
