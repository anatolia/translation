using NUnit.Framework;

using Translation.Client.Web.Models.Language;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;


namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Language
{
    [TestFixture]
    public class LanguageListModelTests
    {
        public LanguageListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLanguageListModel();
        }

        [Test]
        public void LanguageListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "language_list_title");
        }

    }
}
