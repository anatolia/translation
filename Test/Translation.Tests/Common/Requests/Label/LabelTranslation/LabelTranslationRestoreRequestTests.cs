using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label.LabelTranslation
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