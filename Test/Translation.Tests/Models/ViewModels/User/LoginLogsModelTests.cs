using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.User;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.AssertViewModelTestHelper;

namespace Translation.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class LoginLogsModelTests
    {
        public LoginLogsModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetLoginLogsModel();
        }

        [Test]
        public void LoginLogsModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_login_logs_title");
        }

        [Test]
        public void LoginLogsModel_UserInput()
        {
            AssertHiddenInputModel(SystemUnderTest.UserInput, "UserUid");
        }

        public static IEnumerable MessageTestCases
        {
            get
            {
                yield return new TestCaseData(CaseOne,
                                              UidOne,
                                              null,
                                              null,
                                              true);

                yield return new TestCaseData(CaseTwo,
                                              EmptyUid,
                                              new[] { "user_uid_is_not_valid" },
                                              null,
                                              false);
            }
        }

        [TestCaseSource(nameof(MessageTestCases))]
        public void InviteModel_InputErrorMessages(string caseName,
                                                   Guid userUid, 
                                                   string[] errorMessages,
                                                   string[] inputErrorMessages,
                                                   bool result)
        {
            var model = GetLoginLogsModel(userUid);
            model.IsValid().ShouldBe(result);
            model.IsNotValid().ShouldBe(!result);

            AssertMessages(model.ErrorMessages, errorMessages);
            AssertMessages(model.InputErrorMessages, inputErrorMessages);
        }
    }
}