using NUnit.Framework;
using Translation.Client.Web.Models;
using Translation.Tests.TestHelpers;

namespace Translation.Tests.Client.Models
{
    [TestFixture]
    public sealed class HomeModelTests
    {
        public HomeModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = FakeModelTestHelper.GetHomeModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "welcome_to_translation_service");
        }
    }
}