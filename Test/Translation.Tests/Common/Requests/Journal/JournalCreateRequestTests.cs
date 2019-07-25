using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Journal;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Journal

{
    [TestFixture]
    public class JournalCreateRequestTests
    {

        [Test]
        public void JournalCreateRequest_Constructor()
        {
            var request = GetJournalCreateRequest(CurrentUserId,StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.Message.ShouldBe(StringOne);
        
        }

    }
}