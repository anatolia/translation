using NUnit.Framework;
using Shouldly;

using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Organization
{
    [TestFixture]
    public class OrganizationPendingTranslationReadListRequestTests
    {
        [Test]
        public void OrganizationPendingTranslationReadListRequest_Constructor()
        {
            var request = GetOrganizationPendingTranslationReadListRequest(CurrentUserId);

            request.CurrentUserId.ShouldBe(CurrentUserId);
        }
    }

}

