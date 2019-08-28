using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration
{
    [TestFixture]
    public class IntegrationRestoreRequestTests
    {
        [Test]
        public void IntegrationRestoreRequest_Constructor()
        {
            var request = GetIntegrationRestoreRequest(CurrentUserId,UidOne,One);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.IntegrationUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }

    }
}