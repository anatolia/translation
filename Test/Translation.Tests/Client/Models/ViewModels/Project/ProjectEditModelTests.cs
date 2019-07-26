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
    public class ProjectEditModelTests
    {
        public ProjectEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetOrganizationOneProjectOneEditModel();
        }

        [Test]
        public void ProjectEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_edit_title");
        }

        [Test]
        public void ProjectEditModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void ProjectEditModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void ProjectEditModel_NameInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name");
        }

        [Test]
        public void ProjectEditModel_SlugInput()
        {
            AssertInputModel(SystemUnderTest.SlugInput, "Slug", "slug");
        }

        [Test]
        public void ProjectEditModel_UrlInput()
        {
            AssertInputModel(SystemUnderTest.UrlInput, "Url", "url");
        }

        [Test]
        public void ProjectEditModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
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
            SystemUnderTest.NameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.SlugInput.Value.ShouldBe(SystemUnderTest.Slug);
            SystemUnderTest.UrlInput.Value.ShouldBe(SystemUnderTest.Url);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
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
                                                      "project_uid_is_not_valid" },
                                              new[] { "project_name_required_error_message",
                                                      "project_slug_required_error_message",
                                                      "url_is_not_valid_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void ProjectEditModel_InputErrorMessages(string caseName, 
                                                         Guid organizationUid, Guid projectUid, string name, 
                                                         string slug, string url,
                                                         string[] errorMessages, 
                                                         string[] inputErrorMessages,
                                                         bool result)
        {
            var model = GetProjectEditModel(organizationUid, projectUid, name, slug, url);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
