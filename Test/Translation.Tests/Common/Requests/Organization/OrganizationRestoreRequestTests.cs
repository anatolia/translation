using NUnit.Framework;
using Shouldly;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Organization
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

