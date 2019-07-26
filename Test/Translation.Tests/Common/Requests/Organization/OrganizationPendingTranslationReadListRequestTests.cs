using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Organization;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Organization
{
    [TestFixture]
    public class OrganizationPendingTranslationReadListRequestTests
    {
        [Test]
        public void OrganizationPendingTranslationReadListRequest_Constructor()
        {
            var request = GetOrganizationPendingTranslationReadListRequest(CurrentUserId, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void OrganizationPendingTranslationReadListRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new OrganizationPendingTranslationReadListRequest(currentUserId, organizationUid);
            });
        }
    }

}

