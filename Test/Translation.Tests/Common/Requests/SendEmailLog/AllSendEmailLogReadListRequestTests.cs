using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.SendEmailLog;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.SendEmailLog
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