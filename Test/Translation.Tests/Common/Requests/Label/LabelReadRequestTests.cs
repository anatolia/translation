using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelReadRequestTests
    {
        [Test]
        public void LabelReadRequest_Constructor()
        {
            var request = GetLabelReadRequest(CurrentUserId,UidOne);
            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelReadRequest_Argument_Validations(long currentUserId, Guid labelUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelReadRequest(currentUserId, labelUid); });
        }
    }
}