using NUnit.Framework;

using Translation.Client.Web.Models.User;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.User
{
    [TestFixture]
    public sealed class DemandPasswordResetDoneModelTests
    {
        public DemandPasswordResetDoneModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetDemandPasswordResetDoneModel();
        }

        [Test]
        public void DemandPasswordResetDoneModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_demand_password_reset_done_title");
        }
    }
}