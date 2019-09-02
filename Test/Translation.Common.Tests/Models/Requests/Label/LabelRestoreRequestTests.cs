using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label
{
    public class LabelRestoreRequestTests
    {
        [Test]
        public void LabelRestoreRequest_Constructor()
        {
            var request = GetLabelRestoreRequest(CurrentUserId,UidOne,One);
            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
            
        }
    }
}