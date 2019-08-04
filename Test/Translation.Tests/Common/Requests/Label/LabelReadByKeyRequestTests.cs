using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelReadByKeyRequestTests
    {
        [Test]
        public void LabelReadByKeyRequest_Constructor()
        {
            var request = GetLabelReadByKeyRequest(CurrentUserId, StringOne, StringOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelKey.ShouldBe(StringOne);
            request.ProjectName.ShouldBe(StringOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, StringOne, EmptyString);
                yield return new TestCaseData(CurrentUserId, EmptyString, StringOne);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelReadByKeyRequest_Argument_Validations(long currentUserId, string labelKey, string projectName)
        {
            Assert.Throws<ArgumentException>(() => { new LabelReadByKeyRequest(currentUserId, labelKey, projectName); });
        }
    }
}