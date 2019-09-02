using NUnit.Framework;

using Translation.Client.Web.Models;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models
{
    [TestFixture]
    public sealed class AccessDeniedModelTests
    {
        public AccessDeniedModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetAccessDeniedModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "access_denied_title");
        }
    }
}