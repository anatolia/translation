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
    public class UserBaseRequestTests
    {
        [Test]
        public void UserBaseRequest_Constructor()
        {
            var request = GetUserBaseRequest(CurrentUserId, UidOne);

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
        public void UserBaseRequest_Argument_Validations(long currentUserId, Guid userUid)
        {
            Assert.Throws<ArgumentException>(() => { new UserBaseRequest(currentUserId, userUid); });
        }
    }
}