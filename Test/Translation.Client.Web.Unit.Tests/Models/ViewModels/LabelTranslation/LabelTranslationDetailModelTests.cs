using NUnit.Framework;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
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
