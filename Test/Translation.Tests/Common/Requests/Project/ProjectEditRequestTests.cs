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
    public class ProjectEditRequestTests
    {
        [Test]
        public void ProjectEditRequest_Constructor()
        {
            var request = GetProjectEditRequest(CurrentUserId,UidOne,UidOne,
                                                StringOne,HttpUrl,StringTwo,
                                                StringTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidOne);
            request.ProjectName.ShouldBe(StringOne);
            request.Url.ShouldBe(HttpUrl);
            request.Description.ShouldBe(StringTwo);
            request.ProjectSlug.ShouldBe(StringTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne,
                                              StringOne, HttpUrl, StringTwo,
                                              StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid,
                                              StringOne, HttpUrl, StringTwo,
                                              StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                               EmptyString, HttpUrl, StringTwo,
                                               StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                              StringOne, StringTwo, StringTwo,
                                              StringTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                              StringOne, HttpUrl, StringTwo,
                                              EmptyString);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
                                                             string projectName, string url, string description,
                                                             string projectSlug)
        {
            Assert.Throws<ArgumentException>(() => { new ProjectEditRequest( currentUserId,  organizationUid,  projectUid,
                                                                             projectName,  url,  description,
                                                                             projectSlug);

            });
        }
    }
}