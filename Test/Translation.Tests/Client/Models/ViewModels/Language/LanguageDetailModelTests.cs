using NUnit.Framework;

using Translation.Client.Web.Models.Language;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;


namespace Translation.Tests.Client.Models.ViewModels.Language
{
    [TestFixture]
    public class LanguageDetailModelTests
    {
        public LanguageDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLanguageDetailModel();
        }

        [Test]
        public void LanguageDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "language_detail_title");
        }

    }
}
