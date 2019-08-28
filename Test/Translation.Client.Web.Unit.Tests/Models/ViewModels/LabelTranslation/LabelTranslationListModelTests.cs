using NUnit.Framework;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
{
    [TestFixture]
    public class LabelTranslationListModelTests
    {
        public LabelTranslationListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelTranslationListModel();
        }

        [Test]
        public void LabelTranslationListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_translations_list_title");
        }
    }
}
