using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Integration;
using Translation.Common.Helpers;
using Translation.Tests.TestHelpers;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Integration
{
    [TestFixture]
    public class IntegrationEditModelTests
    {
        public IntegrationEditModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetIntegrationEditModel();
        }

        [Test]
        public void IntegrationEditModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "integration_edit_title");
        }

        [Test]
        public void IntegrationEditModel_IntegrationInput()
        {
            AssertViewModelTestHelper.AssertHiddenInputModel(SystemUnderTest.IntegrationInput, "IntegrationUid");
        }

        [Test]
        public void IntegrationEditModel_IntegrationNameInput()
        {
            AssertViewModelTestHelper.AssertInputModel(SystemUnderTest.IntegrationNameInput, "Name", "name", true);
        }

        [Test]
        public void IntegrationEditModel_DescriptionInput()
        {
            AssertViewModelTestHelper.AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
        }

        [Test]
        public void IntegrationEditModel_SetInputModelValues()
        {
            // arrange

            // act
            SystemUnderTest.SetInputModelValues();

            // assert
            SystemUnderTest.IntegrationInput.Value.ShouldBe(SystemUnderTest.IntegrationUid.ToUidString());
            SystemUnderTest.IntegrationNameInput.Value.ShouldBe(SystemUnderTest.Name);
            SystemUnderTest.DescriptionInput.Value.ShouldBe(SystemUnderTest.Description);
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne, StringOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid, EmptyString,
                                              new[] { "integration_uid_is_not_valid" },
                                              new[] { "integration_name_required_error_message" },
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void IntegrationEditModel_InputErrorMessages(string caseName,
                                                            Guid integrationUid, string integrationName,
                                                            string[] errorMessages,
                                                            string[] inputErrorMessages,
                                                            bool result)
        {
            var model = GetIntegrationEditModel(integrationUid, integrationName);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertViewModelTestHelper.AssertMessages(model.ErrorMessages, errorMessages);
            AssertViewModelTestHelper.AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
