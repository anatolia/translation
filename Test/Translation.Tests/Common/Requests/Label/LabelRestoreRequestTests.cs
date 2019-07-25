using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Label;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.Label
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