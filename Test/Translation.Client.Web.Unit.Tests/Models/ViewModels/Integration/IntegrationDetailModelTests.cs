using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Integration
{
    [TestFixture]
    public class IntegrationDetailModelTests
    {
        public IntegrationDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetIntegrationDetailModel(UidOne, StringOne, UidTwo, StringTwo, StringThree);
        }

        [Test]
        public void IntegrationDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "integration_detail_title");
        }

        [Test]
        public void IntegrationDetailModel_Parameters()
        {
            SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
            SystemUnderTest.OrganizationName.ShouldBe(StringOne);
            SystemUnderTest.IntegrationUid.ShouldBe(UidTwo);
            SystemUnderTest.Name.ShouldBe(StringTwo);
            SystemUnderTest.Description.ShouldBe(StringThree);
        }
    }
}
