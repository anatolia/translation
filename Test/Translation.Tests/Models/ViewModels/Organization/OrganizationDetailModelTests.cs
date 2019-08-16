using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Organization;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Organization
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
