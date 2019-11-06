using NUnit.Framework;

using Translation.Client.Web.Models.User;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class ValidateEmailDoneModelTests
    {
        public ValidateEmailDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetValidateEmailDoneModel();
        }

        [Test]
        public void ProjectCreateModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "validate_email_done_title");
        }

    }
}