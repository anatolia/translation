using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class LabelUploadFromCSVDoneModelTests
    {
        public LabelUploadFromCSVDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelUploadFromCSVDoneModel();
        }

        [Test]
        public void LabelUploadFromCSVDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_upload_done_title");
        }
    
        [Test]
        public void LabelUploadFromCSVDoneModel_Parameter()
        {
           SystemUnderTest.ProjectUid.ShouldBe(UidOne);
           SystemUnderTest.ProjectName.ShouldBe(StringOne);
           SystemUnderTest.AddedLabelCount.ShouldBe(Zero);
           SystemUnderTest.CanNotAddedLabelCount.ShouldBe(Zero);
           SystemUnderTest.TotalLabelCount.ShouldBe(Zero);
           SystemUnderTest.CanNotAddedLabelTranslationCount.ShouldBe(Zero);
           SystemUnderTest.AddedLabelTranslationCount.ShouldBe(Zero);
           SystemUnderTest.UpdatedLabelTranslationCount.ShouldBe(Zero);
           SystemUnderTest.TotalRowsProcessed.ShouldBe(Zero);
        }
    }
}