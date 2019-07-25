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
    public class IntegrationReadRequestTests
    {
        [Test]
        public void IntegrationReadRequest_Constructor()
        {
            var request = GetIntegrationReadRequest(CurrentUserId,UidOne);

        }

    }
}