using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.SendEmailLog
{
    [TestFixture]
    public class OrganizationSendEmailLogReadListRequestTests
    {
        [Test]
        public void OrganizationSendEmailLogReadListRequest_Constructor()
        {
            var request = GetOrganizationSendEmailLogReadListRequest(CurrentUserId, OrganizationOneUid);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
        }
    }
}