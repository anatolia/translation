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
    public class ProjectChangeActivationRequestTests
    {
        [Test]
        public void ProjectChangeActivationRequest_Constructor()
        {
            var request = GetProjectChangeActivationRequest(LongOne, UidOne, UidTwo);

            request.CurrentUserId.ShouldBe(LongOne);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(LongOne, EmptyUid, UidTwo);
                yield return new TestCaseData(LongOne, UidOne, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ProjectChangeActivationRequestTests.ArgumentTestCases))]
        public void ProjectChangeActivationRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectChangeActivationRequest(currentUserId, organizationUid, projectUid); });
        }
    }
}
