using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Label
{
    [TestFixture]
    public class LabelCreateModelTests
    {
        public LabelCreateModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelCreateModel();
        }

        [Test]
        public void LabelCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_create_title");
        }

        [Test]
        public void LabelCreateModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelCreateModel_ProjectInput()
        {
            AssertHiddenInputModel(SystemUnderTest.ProjectInput, "ProjectUid");
        }

        [Test]
        public void LabelCreateModel_KeyInput()
        {
            AssertInputModel(SystemUnderTest.KeyInput, "Key", "key");
        }

        [Test]
        public void LabelCreateModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void LabelCreateModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.ProjectInput.Value.ShouldBe(SystemUnderTest.ProjectUid.ToUidString());
            SystemUnderTest.KeyInput.Value.ShouldBe(SystemUnderTest.Key);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              StringTwo,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              EmptyString,
                                              new[] { "organization_uid_not_valid",
                                                      "project_uid_not_valid" },
                                              new[] { "key_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelCreateModel_InputErrorMessages(string caseName,
                                                       Guid organizationUid, Guid projectUid, string projectName,
                                                       string key,
                                                       string[] errorMessages,
                                                       string[] inputErrorMessages,
                                                       bool result)
        {
            var model = GetLabelCreateModel(organizationUid, projectUid, projectName,
                                            key);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
