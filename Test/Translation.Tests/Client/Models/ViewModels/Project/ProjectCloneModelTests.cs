using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Project
{
    [TestFixture]
    public class ProjectCloneModelTests
    {
        public ProjectCloneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationOneProjectOneCloneModel();
        }

        [Test]
        public void ProjectCloneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_clone_title");
        }

        [Test]
        public void ProjectCloneModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void ProjectCloneModel_CloningProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.CloningProjectInput, "CloningProjectUid");
        }

        [Test]
        public void ProjectCloneModel_NameInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name");
        }

        [Test]
        public void ProjectCloneModel_SlugInput()
        {
            AssertInputModel(SystemUnderTest.SlugInput, "Slug", "slug");
        }

        [Test]
        public void ProjectCloneModel_UrlInput()
        {
            AssertInputModel(SystemUnderTest.UrlInput, "Url", "url");
        }

        [Test]
        public void ProjectCloneModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void ProjectCloneModel_LabelCountInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelCountInput, "LabelCount");
        }

        [Test]
        public void ProjectCloneModel_LabelTranslationCountInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelTranslationCountInput, "LabelTranslationCount");
        }

        [Test]
        public void ProjectCloneModel_IsSuperProjectInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsSuperProjectInput, "IsSuperProject", "is_super_project");
        }

        [Test]
        public void ProjectCloneModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.CloningProjectInput.Value.ShouldBe(SystemUnderTest.CloningProjectUid.ToUidString());
            SystemUnderTest.NameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.SlugInput.Value.ShouldBe(SystemUnderTest.Slug);
            SystemUnderTest.UrlInput.Value.ShouldBe(SystemUnderTest.Url);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
            SystemUnderTest.LabelCountInput.Value.ShouldBe(SystemUnderTest.LabelCount.ToString());
            SystemUnderTest.LabelTranslationCountInput.Value.ShouldBe(SystemUnderTest.LabelTranslationCount.ToString());
            SystemUnderTest.IsSuperProjectInput.Value.ShouldBe(SystemUnderTest.IsSuperProject);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne, 
                                              UidOne, UidTwo, StringOne, 
                                              SlugOne, HttpsUrl,
                                              null,
                                              null, 
                                              true);

                yield return new TestCaseData(CaseTwo, 
                                              EmptyUid, EmptyUid, EmptyString, 
                                              EmptySlug, InvalidUrl,
                                              new[] { "organization_uid_is_not_valid",
                                                      "cloning_project_uid_is_not_valid" },
                                              new[] { "project_name_required_error_message",
                                                      "project_slug_required_error_message",
                                                      "url_is_not_valid_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void ProjectCloneModel_InputErrorMessages(string caseName, 
                                                         Guid organizationUid, Guid cloningProjectUid, string name, 
                                                         string slug, string url,
                                                         string[] errorMessages, 
                                                         string[] inputErrorMessages,
                                                         bool result)
        {
            var model = GetProjectCloneModel(organizationUid, cloningProjectUid, name, slug, url);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
