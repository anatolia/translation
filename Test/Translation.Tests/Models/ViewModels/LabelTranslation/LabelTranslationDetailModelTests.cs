using NUnit.Framework;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class LabelTranslationDetailModelTests
    {
        public LabelTranslationDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelTranslationDetailModel();
        }

        [Test]
        public void LabelTranslationDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_translation_detail_title");
        }
    }
}
