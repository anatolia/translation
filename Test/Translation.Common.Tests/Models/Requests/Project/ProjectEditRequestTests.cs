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
    public class ProjectEditRequestTests
    {
        [Test]
        public void ProjectEditRequest_Constructor()
        {
            var request = GetProjectEditRequest(CurrentUserId, UidOne, UidTwo,
                                                StringOne, HttpUrl, StringTwo,
                                                StringThree, UidThree);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
            request.ProjectName.ShouldBe(StringOne);
            request.Url.ShouldBe(HttpUrl);
            request.Description.ShouldBe(StringTwo);
            request.ProjectSlug.ShouldBe(StringThree);
            request.LanguageUid.ShouldBe(UidThree);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne,
                                              StringOne, HttpUrl, StringTwo,
                                              StringTwo, UidOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid,
                                              StringOne, HttpUrl, StringTwo,
                                              StringTwo, UidOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                               EmptyString, HttpUrl, StringTwo,
                                               StringTwo, UidOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                              StringOne, StringTwo, StringTwo,
                                              StringTwo, UidOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne,
                                              StringOne, HttpUrl, StringTwo,
                                              EmptyString, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectEditRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
                                                             string projectName, string url, string description,
                                                             string projectSlug, Guid languageUid)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new ProjectEditRequest(currentUserId, organizationUid, projectUid,
                                        projectName, url, description,
                                        projectSlug, languageUid);

            });
        }
    }
}