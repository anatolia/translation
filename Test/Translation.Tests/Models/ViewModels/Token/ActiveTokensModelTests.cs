using NUnit.Framework;

using Translation.Client.Web.Models.Token;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class ActiveTokensModelTests
    {
        public ActiveTokensModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetActiveTokensModel();
        }

        [Test]
        public void ActiveTokensModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "active_tokens_title");
        }
    }
}