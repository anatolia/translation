using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models
{
    [TestFixture]
    public sealed class HomeModelTests
    {
        public HomeModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetHomeModel();
        }

        [Test]
        public void AllUserListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "welcome_to_translation_service");
        }
        [Test]
        public void AllUserListModel_Parameter()
        {
          SystemUnderTest.IsAuthenticated.ShouldBe(BooleanTrue);
          SystemUnderTest.IsSuperAdmin.ShouldBe(BooleanTrue);
        }
    }
}