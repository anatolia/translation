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
    public class ProjectCloneRequestTests
    {
        [Test]
        public void ProjectCloneRequest_Constructor()
        {
            var request =GetProjectCloneRequest(CurrentUserId, OrganizationOneUid, UidOne,
                                                StringOne, HttpUrl, StringOne,
                                                One, Two, BooleanTrue,
                                                StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
            request.CloningProjectUid.ShouldBe(UidOne);
            request.Name.ShouldBe(StringOne);
            request.Url.ShouldBe(HttpUrl);
            request.Description.ShouldBe(StringOne);
            request.LabelCount.ShouldBe( One);
            request.LabelTranslationCount.ShouldBe( Two);
            request.IsSuperProject.ShouldBe(BooleanTrue);
            request.Slug.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne,
                                              StringOne, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, EmptyUid,
                                               StringOne, HttpUrl, StringOne,
                                               One, Two, BooleanTrue,
                                               StringOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              StringEmpty, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              StringOne, StringOne, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              StringOne, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringEmpty);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectCloneRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid cloningProjectUid,
                                                             string name, string url, string description,
                                                             int labelCount, int labelTranslationCount, bool isSuperProject,
                                                             string slug)
        {
            Assert.Throws<ArgumentException>(() => {new ProjectCloneRequest(currentUserId, organizationUid, cloningProjectUid,
                                                                             name,  url,  description, 
                                                                             labelCount,  labelTranslationCount, isSuperProject,
                                                                             slug);
            });
        }
    }
}