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
    public class ProjectDeleteRequestTests
    {
       
        [Test]
        public void ProjectDeleteRequest_Constructor()
        {
            var request = GetProjectDeleteRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.ProjectUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectDeleteRequest_Argument_Validations(long currentUserId, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectDeleteRequest(currentUserId, projectUid); });
        }
    }
}