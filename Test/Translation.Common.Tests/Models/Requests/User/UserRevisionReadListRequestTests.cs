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
    public class UserRevisionReadListRequestTests
    {

        [Test]
        public void UserRevisionReadListRequest_Constructor()
        {
            var request = GetUserRevisionReadListRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.UserUid.ShouldBe(UidOne);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void UserRevisionReadListRequest_Argument_Validations(long currentUserId, Guid userUid)
        {
            Assert.Throws<ArgumentException>(() => { new UserRevisionReadListRequest(currentUserId, userUid); });
        }
    }
}