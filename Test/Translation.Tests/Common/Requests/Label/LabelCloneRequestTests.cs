using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelCloneRequestTests
    {
        [Test]
        public void LabelCloneRequest_Constructor()
        {
            var request = GetLabelCloneRequest(CurrentUserId, UidOne, UidOne,
                UidOne, StringOne, StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.CloningLabelUid.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidOne);
            request.LabelKey.ShouldBe(StringOne);
            request.Description.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidOne, UidOne, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid, UidOne, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne, EmptyUid, StringOne, StringOne);
                yield return new TestCaseData(CurrentUserId, UidOne, UidOne, UidOne, EmptyString, StringOne);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelCloneRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid cloningLabelUid, Guid projectUid,
                                                           string labelKey,string description)
        {
            Assert.Throws<ArgumentException>(() => { new LabelCloneRequest(currentUserId, organizationUid, cloningLabelUid, projectUid, labelKey, description); });
        }
    }
}