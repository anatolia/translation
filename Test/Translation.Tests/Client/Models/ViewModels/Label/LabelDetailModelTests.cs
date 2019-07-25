using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
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
        public void LabelDetailModel_OrganizationUidInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationUidInput, "OrganizationUid");
        }

        [Test]
        public void LabelDetailModel_ProjectUidInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectUidInput, "ProjectUid");
        }

        [Test]
        public void LabelDetailModel_LabelUidInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelUidInput, "LabelUid");
        }

        [Test]
        public void LabelDetailModel_IsActiveInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsActiveInput, "IsActive", "is_active", true, true);
        }

        [Test]
        public void LabelDetailModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationUidInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectUidInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.LabelUidInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
        }
    }
}
