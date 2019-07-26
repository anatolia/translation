using NUnit.Framework;

using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public class OrganizationRevisionReadListModelTests
    {
        public OrganizationRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationRevisionReadListModel();
        }

        [Test]
        public void OrganizationRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "organization_revision_list_title");
        }
    }
}