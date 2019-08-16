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
    public class ResetPasswordDoneModelTests
    {
        public ResetPasswordDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetResetPasswordDoneModel();
        }

        [Test]
        public void ProjectCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_reset_password_done");
        }

    }
}