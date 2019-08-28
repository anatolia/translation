using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.Token;
using static Translation.Common.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.Token
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

        [Test]
        public void ActiveTokensModel_Parameter()
        {
          SystemUnderTest.ClientUid.ShouldBe(UidStringOne);
          SystemUnderTest.IntegrationUid.ShouldBe(UidStringTwo);
          SystemUnderTest.IntegrationName.ShouldBe(OrganizationOneIntegrationOneName);
        }
    }
}