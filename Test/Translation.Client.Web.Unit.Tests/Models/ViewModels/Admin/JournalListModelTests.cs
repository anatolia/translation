using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class JournalListModelTests
    {
        public JournalListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetJournalListModel();
        }

        [Test]
        public void JournalListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "journal_list_title");
        }
    }
}