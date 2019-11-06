using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.LabelTranslation;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.LabelTranslation
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

        [Test]
        public void TranslationUploadFromCSVDoneModel_Parameter()
        {
            SystemUnderTest.LabelUid.ShouldBe(UidOne);
            SystemUnderTest.LabelKey.ShouldBe(StringOne);
            SystemUnderTest.AddedTranslationCount.ShouldBe(Zero);
            SystemUnderTest.UpdatedTranslationCount.ShouldBe(Zero);
            SystemUnderTest.CanNotAddedTranslationCount.ShouldBe(Zero);
            SystemUnderTest.TotalRowsProcessed.ShouldBe(Zero);
        }
    }
}
