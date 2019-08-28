using System;
using System.Collections;
using NUnit.Framework;
using Shouldly;
using Translation.Common.Models.Requests.Label;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelChangeActivationRequestTests
    {
        
        [Test]
        public void LabelChangeActivationRequest_Constructor()
        {
            var request =GetLabelChangeActivationRequest(CurrentUserId,UidOne,UidTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.OrganizationUid.ShouldBe(UidOne);
            request.LabelUid.ShouldBe(UidTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid, UidTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyUid);


            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void LabelChangeActivationRequest_Argument_Validations(long currentUserId, Guid organizationUid, Guid labelUid)
        {
            Assert.Throws<ArgumentException>(() => { new LabelChangeActivationRequest(currentUserId, organizationUid, labelUid); });
        }
    }
}
