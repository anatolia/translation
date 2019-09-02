using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration
{
    [TestFixture]
    public class IntegrationDeleteRequestTests
    {
        [Test]
        public void IntegrationDeleteRequest_Constructor()
        {
            var request = GetIntegrationDeleteRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationUid.ShouldBe(UidOne);
        }
    }
}