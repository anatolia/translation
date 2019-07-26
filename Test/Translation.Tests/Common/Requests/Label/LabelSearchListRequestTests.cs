using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelSearchListRequestTests
    {
        [Test]
        public void LabelSearchListRequest_Constructor()
        {
            var request = GetLabelSearchListRequest(CurrentUserId,StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.SearchTerm.ShouldBe(StringOne);
        }
    }
}