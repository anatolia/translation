using NUnit.Framework;

using Translation.Client.Web.Models.Organization;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Organization
{
    [TestFixture]
    public sealed class OrganizationListModelTests
    {
        public OrganizationListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationListModel();
        }

        [Test]
        public void OrganizationJournalListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "organization_list_title");
        }
    }
}