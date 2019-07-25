using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Common.Requests.Label
{
    public class LabelCreateRequestTests
    {
        [Test]
        public void LabelCreateRequest_Constructor()
        {
            var request = GetLabelCreateRequest(CurrentUserId, UidOne, UidOne,
                StringOne, StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidOne);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, StringOne, StringOne);

               
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCreateRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid projectUid,
                                                            string labelKey,string description)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCreateRequest(currentUserId, organizationUid, projectUid, labelKey, description); });
        }
    }
}