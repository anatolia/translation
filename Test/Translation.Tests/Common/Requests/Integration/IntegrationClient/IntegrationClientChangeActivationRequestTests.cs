using NUnit.Framework;
using Shouldly;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration.IntegrationClient
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