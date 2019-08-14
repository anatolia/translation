using NUnit.Framework;
using Translation.Client.Web.Models;
using Translation.Tests.TestHelpers;

namespace Translation.Tests.Client.Models
{
    [TestFixture]
    public sealed class AccessDeniedModelTests
    {
        public AccessDeniedModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = FakeModelTestHelper.GetAccessDeniedModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "access_denied_title");
        }
    }
}