using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Project;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Project
{
    [TestFixture]
    public class ProjectReadRequestTests
    {
      
        [Test]
        public void ProjectReadRequest_Constructor()
        {
            var request = GetProjectReadRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.ProjectUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId,EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectReadRequest_Argument_Validations(long currentUserId,Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectReadRequest(currentUserId, projectUid); });
        }
    }
}