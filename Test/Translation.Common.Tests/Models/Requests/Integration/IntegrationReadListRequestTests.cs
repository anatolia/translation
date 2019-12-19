using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Integration;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Integration
{
    [TestFixture]
    public class IntegrationReadListRequestTests
    {
        [Test]
        public void IntegrationReadListRequest_Constructor()
        {
            var request = GetIntegrationReadListRequest(CurrentUserId,OrganizationOneUid);

            request.CurrentUserId.ShouldBe(CurrentUserId);

        }
    }
}