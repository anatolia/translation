using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration
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