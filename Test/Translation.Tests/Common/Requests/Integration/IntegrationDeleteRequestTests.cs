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