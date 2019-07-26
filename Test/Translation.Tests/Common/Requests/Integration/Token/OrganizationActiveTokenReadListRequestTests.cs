using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Integration.Token;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Integration.Token
{
    [TestFixture]
    public class OrganizationActiveTokenReadListRequestTests
    {
        [Test]
        public void OrganizationActiveTokenReadListRequest_Constructor()
        {
            var request = GetOrganizationActiveTokenReadListRequest(CurrentUserId, OrganizationOneUid);
            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectCreateRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() => { new OrganizationActiveTokenReadListRequest(currentUserId, organizationUid); });
        }
    }
}