using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
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