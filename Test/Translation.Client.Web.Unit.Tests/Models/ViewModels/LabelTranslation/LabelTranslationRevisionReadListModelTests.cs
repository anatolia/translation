using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace TTranslation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
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
        [Test]
        public void LabelTranslationRevisionReadListModel_Parameter()
        {
            SystemUnderTest.LabelTranslationName.ShouldBe(StringOne);
            SystemUnderTest.LabelTranslationUid.ShouldBe(UidOne);
        }
    }
}
