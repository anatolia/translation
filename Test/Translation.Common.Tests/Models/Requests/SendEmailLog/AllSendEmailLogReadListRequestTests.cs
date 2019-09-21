using NUnit.Framework;
using Shouldly;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.SendEmailLog
{
    [TestFixture]
    public class AllSendEmailLogReadListRequestTests
    {
        [Test]
        public void AllSendEmailLogReadListRequest_Constructor()
        {
            var request = GetAllSendEmailLogReadListRequest(CurrentUserId);

            request.CurrentUserId.ShouldBe(CurrentUserId);
        }
    }
}