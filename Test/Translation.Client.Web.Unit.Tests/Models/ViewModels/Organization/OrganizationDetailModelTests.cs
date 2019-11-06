using NUnit.Framework;

using Translation.Client.Web.Models.Organization;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Organization
{
    [TestFixture]
    public class OrganizationDetailModelTests
    {
        public OrganizationDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationDetailModel();
        }

        [Test]
        public void OrganizationDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "organization_detail_title");
        }

        [Test]
        public void OrganizationDetailModel_IsActiveInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsActiveInput, "IsActive", "is_active", false, true);
        }

    }
}
