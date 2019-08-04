using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
{
    public class LabelReadListRequestTests
    {
        [Test]
        public void LabelReadListRequest_Constructor()
        {
            var request = GetLabelReadListRequest(CurrentUserId, UidOne);

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
        public void LabelReadListRequest_Argument_Validations(long currentUserId, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelReadListRequest(currentUserId, projectUid); });
        }
    }
}