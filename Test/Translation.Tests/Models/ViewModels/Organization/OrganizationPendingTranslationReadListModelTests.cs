using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public  class OrganizationPendingTranslationReadListModelTests
    {
        public OrganizationPendingTranslationReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationPendingTranslationReadListModel();
        }

        [Test]
        public void OrganizationPendingTranslationReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "organization_pending_translations_title");
        }

        [Test]
        public void OrganizationPendingTranslationReadListModel_Parameter()
        {
            SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
            SystemUnderTest.OrganizationName.ShouldBe(OrganizationOneName);
        }
    }
}