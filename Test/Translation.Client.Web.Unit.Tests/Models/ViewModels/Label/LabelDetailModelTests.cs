using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Label
{
    [TestFixture]
    public class LabelDetailModelTests
    {
        public LabelDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelDetailModel();
        }

        [Test]
        public void LabelDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_detail_title");
        }

        [Test]
        public void LabelDetailModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelDetailModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void LabelDetailModel_LabelInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelInput, "LabelUid");
        }

        [Test]
        public void LabelDetailModel_IsActiveInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsActiveInput, "IsActive", "is_active", true, true);
        }

        [Test]
        public void LabelDetailModel_Parameter()
        {
            SystemUnderTest.LabelTranslationCount.ShouldBe(Zero);
        }

        [Test]
        public void LabelDetailModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.LabelInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
        }
    }
}
