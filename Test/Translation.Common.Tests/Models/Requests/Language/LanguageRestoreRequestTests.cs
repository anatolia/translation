using NUnit.Framework;
using Shouldly;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Common.Tests.Models.Requests.Language
{
    [TestFixture]
    public class LanguageRestoreRequestTests
    {
        [Test]
        public void LanguageRestoreRequest_Constructor()
        {
            var request = GetLanguageRestoreRequest(CurrentUserId, UidOne, One);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.LanguageUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }

    }
}