using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class AllAllLabelReadListRequestTests
    {
        [Test]
        public void AllLabelReadListRequest_Constructor_Token()
        {
            var request = GetAllLabelReadListRequest(UidOne, UidTwo);

            request.Token.ShouldBe(UidOne);
            request.ProjectUid.ShouldBe(UidTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(EmptyUid, UidTwo);
                yield return new TestCaseData(UidOne, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void AllLabelReadListRequest_Argument_Validations(Guid token, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new AllLabelReadListRequest(token, projectUid); });
        }

        [Test]
        public void AllLabelReadListRequest_Constructor_CurrentUserId()
        {
            var request = GetAllLabelReadListRequest(CurrentUserId, UidTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.ProjectUid.ShouldBe(UidTwo);
        }

        public static IEnumerable ArgumentTestCasesCurrentUserId
        {
            get
            {
                yield return new TestCaseData(LongZero, UidTwo);
                yield return new TestCaseData(LongOne, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCasesCurrentUserId))]
        public void AllLabelReadListRequest_Argument_Validations(long currentUserId, Guid projectUid)
        {
            Assert.Throws<ArgumentException>(() => { new AllLabelReadListRequest(currentUserId, projectUid); });
        }
    }
}