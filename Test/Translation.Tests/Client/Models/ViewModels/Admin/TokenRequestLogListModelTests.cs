using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using static Translation.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Tests.Client.Models.ViewModels.Admin
{
    [TestFixture]
    public sealed class TokenRequestLogListModelTests
    {
        public TokenRequestLogListModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetTokenRequestLogListModel();
        }

        [Test]
        public void TokenRequestLogListModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "token_request_log_list_title");
        }
    }
}