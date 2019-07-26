using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.User;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.User
{
    [TestFixture]
    public class UserRestoreRequestTests
    {
        [Test]
        public void UserRestoreRequest_Constructor()
        {
            var request = GetUserRestoreRequest(CurrentUserId, UidOne, One);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.UserUid.ShouldBe(UidOne);
            request.Revision.ShouldBe(One);
        }

    }

}