using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Organization
{
    [TestFixture]
    public class OrganizationRestoreRequestTests
    {
        [Test]
        public void OrganizationRestoreRequest_Constructor()
        {
            var request = GetOrganizationRestoreRequest(CurrentUserId, UidOne, One);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }

    }
}

