using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Journal
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