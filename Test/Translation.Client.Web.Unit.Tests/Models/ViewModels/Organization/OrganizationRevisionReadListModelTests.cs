using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Organization;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Organization
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

        [Test]
        public void OrganizationRevisionReadListModel_Parameter()
        {
           SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
           SystemUnderTest.OrganizationName.ShouldBe(OrganizationOneName);
        }
    }
}