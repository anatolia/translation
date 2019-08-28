using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration.IntegrationClient
{
    [TestFixture]
    public class IntegrationClientChangeActivationRequestTests
    {
        [Test]
        public void IntegrationClientChangeActivationRequest_Constructor()
        {
            var request = GetIntegrationClientChangeActivationRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationClientUid.ShouldBe(UidOne);
        }

    }
}