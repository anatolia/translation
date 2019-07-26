using NUnit.Framework;

using Translation.Client.Web.Models.Organization;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

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
    }
}