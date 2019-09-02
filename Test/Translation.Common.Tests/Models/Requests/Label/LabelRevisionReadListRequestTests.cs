using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelRevisionReadListRequestTests
    {
        [Test]
        public void LabelRevisionReadListRequest_Constructor()
        {
            var request = GetLabelRevisionReadListRequest(CurrentUserId,UidOne);

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
        public void LabelRevisionReadListRequest_Argument_Validations(long currentUserId, Guid labelUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelRevisionReadListRequest(currentUserId, labelUid); });
        }
    }
}