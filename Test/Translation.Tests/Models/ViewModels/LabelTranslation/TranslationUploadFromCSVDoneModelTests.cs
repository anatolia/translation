using NUnit.Framework;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class TranslationUploadFromCSVDoneModelTests
    {
        public TranslationUploadFromCSVDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetTranslationUploadFromCSVDoneModel();
        }

        [Test]
        public void TranslationUploadFromCSVDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "translation_upload_done_title");
        }
    }
}
