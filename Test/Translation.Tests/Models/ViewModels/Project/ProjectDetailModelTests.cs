using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Project
{
    [TestFixture]
    public class ProjectDetailModelTests
    {
        public ProjectDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationOneProjectOneDetailModel();
        }

        [Test]
        public void ProjectCloneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_detail_title");
        }

        [Test]
        public void ProjectCloneModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void ProjectDetailModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void ProjectDetailModel_IsActiveInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsActiveInput, "IsActive", "is_active", false, true);
        }


        [Test]
        public void ProjectDetailModel_Parameter()
        {
           SystemUnderTest.LanguageName.ShouldBe(StringTwo);
           SystemUnderTest.LanguageIconUrl.ShouldBe(HttpsUrl);
        }

        [Test]
        public void ProjectCloneModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
        }
    }
}
