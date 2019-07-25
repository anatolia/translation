using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelSearchListRequestTests
    {
        [Test]
        public void LabelSearchListRequest_Constructor()
        {
            var request = GetLabelSearchListRequest();

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.SearchTerm.ShouldBe(StringOne);
        }
    }
}