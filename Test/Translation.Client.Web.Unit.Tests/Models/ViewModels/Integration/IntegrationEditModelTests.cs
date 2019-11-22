using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using StandardUtils.Helpers;

using Translation.Client.Web.Models.Integration;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Integration
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
            AssertHiddenInputModel(SystemUnderTest.IntegrationInput, "IntegrationUid");
        }

        [Test]
        public void IntegrationEditModel_IntegrationNameInput()
        {
            AssertInputModel(SystemUnderTest.IntegrationNameInput, "Name", "name", true);
        }

        [Test]
        public void IntegrationEditModel_DescriptionInput()
        {
            AssertInputModel(SystemUnderTest.DescriptionInput, "Description", "description");
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

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}
