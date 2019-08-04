using NUnit.Framework;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class LabelTranslationRevisionReadListModelTests
    {
        public LabelTranslationRevisionReadListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelTranslationRevisionReadListModel();
        }

        [Test]
        public void LabelTranslationRevisionReadListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_translation_revision_list_title");
        }
    }
}
