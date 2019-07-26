using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Project;
using Translation.Common.Helpers;
using Translation.Tests.TestHelpers;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Project
{
    [TestFixture]
    public class ProjectCreateModelTests
    {
        public ProjectCreateModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = FakeModelTestHelper.GetOrganizationOneProjectOneCreateModel();
        }

        [Test]
        public void ProjectCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "project_create_title");
        }

        [Test]
        public void ProjectCreateModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void ProjectCreateModel_NameInput()
        {
            AssertInputModel(SystemUnderTest.NameInput, "Name", "name", true);
        }

        [Test]
        public void ProjectCreateModel_SlugInput()
        {
            AssertInputModel(SystemUnderTest.SlugInput, "Slug", "slug", true);
        }

        [Test]
        public void ProjectCreateModel_UrlInput()
        {
            AssertInputModel(SystemUnderTest.UrlInput, "Url", "url");
        }

        [Test]
        public void ProjectCreateModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void ProjectCreateModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.NameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.SlugInput.Value.ShouldBe(SystemUnderTest.Slug);
            SystemUnderTest.UrlInput.Value.ShouldBe(SystemUnderTest.Url);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(FakeConstantTestHelper.CaseOne,
                                              FakeConstantTestHelper.UidOne, FakeConstantTestHelper.StringOne, FakeConstantTestHelper.SlugOne,
                                              FakeConstantTestHelper.HttpsUrl,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(FakeConstantTestHelper.CaseTwo,
                                              FakeConstantTestHelper.EmptyUid, FakeConstantTestHelper.EmptyString, FakeConstantTestHelper.EmptySlug,
                                              FakeConstantTestHelper.InvalidUrl,
                                              new[] { "organization_uid_is_not_valid" },
                                              new[] { "project_name_required_error_message",
                                                      "project_slug_required_error_message",
                                                      "url_is_not_valid_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void ProjectCreateModel_InputErrorMessages(string caseName,
                                                          Guid organizationUid, string name, string slug,
                                                          string url,
                                                          string[] errorMessages,
                                                          string[] inputErrorMessages,
                                                          bool result)
        {
            var model = FakeModelTestHelper.GetProjectCreateModel(organizationUid, name, slug, url);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}