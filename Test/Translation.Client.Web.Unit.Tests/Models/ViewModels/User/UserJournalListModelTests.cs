using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.User;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public sealed class UserJournalListModelTests
    {
        public UserJournalListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUserJournalListModel();
        }

        [Test]
        public void UserJournalListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_journal_list_title");
        }

        [Test]
        public void UserJournalListModel_Parameter()
        {
           SystemUnderTest.UserUid.ShouldBe(UidOne);
        }
    }
}