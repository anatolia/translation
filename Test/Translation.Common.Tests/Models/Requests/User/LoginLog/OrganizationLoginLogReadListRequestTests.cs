using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.User.LoginLog;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.User.LoginLog
{
    [TestFixture]
    public class OrganizationLoginLogReadListRequestTests
    {
        [Test]
        public void ProjectReadRequest_Constructor()
        {
            var request = GetOrganizationLoginLogReadListRequest(CurrentUserId, UidOne);
            
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
        public void ProjectReadRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() => { new OrganizationLoginLogReadListRequest(currentUserId, organizationUid); });
        }
    }
}