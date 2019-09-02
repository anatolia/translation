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
    class ProjectReadBySlugRequestTests
    {
        [Test]
        public void ProjectReadBySlugRequest_Constructor()
        {
            var request = GetProjectReadBySlugRequest(CurrentUserId,StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
          request.ProjectSlug.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectReadBySlugRequest_Argument_Validations(long currentUserId, string projectSlug)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectReadBySlugRequest(currentUserId, projectSlug); });
        }
    }
}
