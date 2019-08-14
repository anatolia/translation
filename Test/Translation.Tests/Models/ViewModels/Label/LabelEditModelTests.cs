using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Label;
using Translation.Common.Helpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Label
{
    [TestFixture]
    public class LabelEditModelTests
    {
        public LabelEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLabelEditModel();
        }

        [Test]
        public void LabelEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "label_edit_title");
        }

        [Test]
        public void LabelEditModel_OrganizationInput()
        {
            AssertHiddenInputModel(SystemUnderTest.OrganizationInput, "OrganizationUid");
        }

        [Test]
        public void LabelEditModel_LabelInput()
        {
            AssertHiddenInputModel(SystemUnderTest.LabelInput, "LabelUid");
        }

        [Test]
        public void LabelEditModel_KeyInput()
        {
            AssertInputModel(SystemUnderTest.KeyInput, "Key", "key");
        }

        [Test]
        public void LabelEditModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void LabelEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.OrganizationInput.Value.ShouldBe(SystemUnderTest.OrganizationUid.ToUidString());
            SystemUnderTest.LabelInput.Value.ShouldBe(SystemUnderTest.LabelUid.ToUidString());
            SystemUnderTest.KeyInput.Value.ShouldBe(SystemUnderTest.Key);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, UidTwo, StringOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyUid, EmptyString,
                                              new[] { "organization_uid_not_valid",
                                                      "label_uid_is_not_valid" },
                                              new[] { "key_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void LabelEditModel_InputErrorMessages(string caseName,
                                                       Guid organizationUid, Guid labelUid, string key,
                                                       string[] errorMessages,
                                                       string[] inputErrorMessages,
                                                       bool result)
        {
            var model = GetLabelEditModel(organizationUid, labelUid, key);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
