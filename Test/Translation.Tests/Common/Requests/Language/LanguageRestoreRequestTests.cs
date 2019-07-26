using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Language
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