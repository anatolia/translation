using NUnit.Framework;
using Shouldly;
using Translation.Tests.Common.Requests.Organization;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.SendEmailLog
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