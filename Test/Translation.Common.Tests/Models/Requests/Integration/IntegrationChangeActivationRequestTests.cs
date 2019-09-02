using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration
{
    [TestFixture]
    public class IntegrationChangeActivationRequestTests
    {
        [Test]
        public void IntegrationChangeActivationRequest_Constructor()
        {
            var request = GetIntegrationChangeActivationRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationUid.ShouldBe(UidOne);
        }

    }
}