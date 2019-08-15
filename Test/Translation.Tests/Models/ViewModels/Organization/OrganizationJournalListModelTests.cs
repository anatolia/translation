using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class OrganizationJournalListModelTests
    {
        public OrganizationJournalListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationJournalListModel();
        }

        [Test]
        public void OrganizationJournalListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "journal_list_title");
        }

        [Test]
        public void OrganizationJournalListModel_Parameter()
        {
            SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
        }
    }
}