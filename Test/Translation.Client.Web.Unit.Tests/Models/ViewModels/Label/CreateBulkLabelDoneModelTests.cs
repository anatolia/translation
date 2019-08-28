using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Label
{
    [TestFixture]
    public sealed class CreateBulkLabelDoneModelTests
    {
        public CreateBulkLabelDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetCreateBulkLabelDoneModel();
        }

        [Test]
        public void CreateBulkLabelDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "create_bulk_label_done_title");
        }


        [Test]
        public void CreateBulkLabelDoneModel_Parameter()
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