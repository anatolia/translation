using NUnit.Framework;
using Shouldly;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Journal
{
    [TestFixture]
    public class AllJournalReadListRequestTest
    {
        [Test]
        public void AllJournalReadListRequest_Constructor()
        {
            var request = GetAllJournalReadListRequest(CurrentUserId);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.PagingInfo.IsAscending.ShouldBeFalse();
        }

    }
}