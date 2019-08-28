using NUnit.Framework;

using Translation.Client.Web.Models.Admin;
using  static  Translation.Common.Tests.TestHelpers.FakeModelTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Admin
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