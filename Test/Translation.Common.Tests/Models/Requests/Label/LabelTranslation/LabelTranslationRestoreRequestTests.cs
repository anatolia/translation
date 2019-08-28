using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Label.LabelTranslation
{
    public class LabelTranslationRestoreRequestTests
    {
        [Test]
        public void LabelTranslationRestoreRequest_Constructor()
        {
            var request = GetLabelTranslationRestoreRequest(CurrentUserId, UidOne, One);
            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LabelTranslationUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }
    }
}