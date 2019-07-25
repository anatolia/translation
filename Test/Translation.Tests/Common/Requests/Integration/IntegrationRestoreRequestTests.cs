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