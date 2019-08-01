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
                                                StringOne, HttpUrl, StringTwo,
                                                One, Two, BooleanTrue,
                                                StringThree, UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(OrganizationOneUid);
            request.CloningProjectUid.ShouldBe(UidOne);
            request.Name.ShouldBe(StringOne);
            request.Url.ShouldBe(HttpUrl);
            request.Description.ShouldBe(StringTwo);
            request.LabelCount.ShouldBe( One);
            request.LabelTranslationCount.ShouldBe( Two);
            request.IsSuperProject.ShouldBe(BooleanTrue);
            request.Slug.ShouldBe(StringThree);
            request.LanguageUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne,
                                              StringOne, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne, UidOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, EmptyUid,
                                               StringOne, HttpUrl, StringOne,
                                               One, Two, BooleanTrue,
                                               StringOne, UidOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              EmptyString, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne, UidOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              StringOne, StringOne, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne, UidOne);
                yield return new TestCaseData(CurrentUserId, OrganizationOneUid, UidOne,
                                              StringOne, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              EmptyString, UidOne);
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne,
                                              StringOne, HttpUrl, StringOne,
                                              One, Two, BooleanTrue,
                                              StringOne, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void ProjectCloneRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid cloningProjectUid,
                                                             string name, string url, string description,
                                                             int labelCount, int labelTranslationCount, bool isSuperProject,
                                                             string slug, Guid languageUid)
        {
            Assert.Throws<ArgumentException>(() => {new ProjectCloneRequest(currentUserId, organizationUid, cloningProjectUid,
                                                                             name,  url,  description, 
                                                                             labelCount,  labelTranslationCount, isSuperProject,
                                                                             slug, languageUid);
            });
        }
    }
}