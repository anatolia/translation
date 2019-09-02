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
    public class  ProjectRevisionReadListRequestTests
    {

        [Test]
        public void  ProjectRevisionReadListRequest_Constructor()
        {
            var request = GetProjectRevisionReadListRequest(CurrentUserId,UidOne);

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
        public void  ProjectRevisionReadListRequest_Argument_Validations(long currentUserId, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new  ProjectRevisionReadListRequest(currentUserId, projectUid); });
        }
    }
}