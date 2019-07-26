using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.User.LoginLog;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Tests.Common.Requests.User.LoginLog
{
    [TestFixture]
    public class UserLoginLogReadListRequestTests
    {
        [Test]
        public void ProjectReadRequest_Constructor()
        {
            var request = GetUserLoginLogReadListRequest(CurrentUserId, UidOne);
          
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
        public void ProjectReadRequest_Argument_Validations(long currentUserId, Guid userUid)
        {
            Assert.Throws<ArgumentException>(() => { new UserLoginLogReadListRequest(currentUserId, userUid); });
        }
    }
}