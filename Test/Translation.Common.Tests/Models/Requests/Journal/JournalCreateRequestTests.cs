using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Journal

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