using NUnit.Framework;

using Translation.Client.Web.Models.Language;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;


namespace Translation.Tests.Client.Models.ViewModels.Language
{
    [TestFixture]
    public class LanguageRevisionReadListTests
    {
        public LanguageRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLanguageRevisionReadListModel();
        }

        [Test]
        public void LanguageRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "language_revision_list_title");
        }

    }
}
