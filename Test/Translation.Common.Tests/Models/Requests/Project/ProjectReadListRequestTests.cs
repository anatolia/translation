using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Project;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Project
{
    [TestFixture]
    public class ProjectReadListRequestTests
    {
        [Test]
        public void ProjectReadListRequest_Constructor()
        {
            var request =GetProjectReadListRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId,EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectReadListRequest_Argument_Validations(long currentUserId, Guid organizationUid)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectReadListRequest(currentUserId, organizationUid); });
        }
    }
}