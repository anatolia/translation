using NUnit.Framework;
using Shouldly;
using Translation.Client.Web.Models.Token;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Token
{
    [TestFixture]
    public sealed class ActiveTokensDataModelTests
    {
        public ActiveTokensDataModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetActiveTokensDataModel();
        }


        [Test]
        public void ActiveTokensModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "active_tokens_table_title");
        }

        [Test]
        public void ActiveTokensModel_Parameter()
        {
            SystemUnderTest.TokenUid.ShouldBe(UidStringOne);
            SystemUnderTest.Ip.ShouldBe(IpOne);
            SystemUnderTest.CreatedAt.ShouldBe(StringOne);
            SystemUnderTest.ExpiresAt.ShouldBe(StringTwo);
        }
    }
}