using System;
using System.Collections;

using NUnit.Framework;
using Shouldly;

using Translation.Common.Models.Requests.Journal;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
namespace Translation.Tests.Common.Requests.Journal

{
    [TestFixture]
    public class UserJournalReadListRequestTests
    {

        [Test]
        public void UserJournalReadListRequest_Constructor()
        {
            var request = GetUserJournalReadListRequest(CurrentUserId,UidOne);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.UserUid.ShouldBe(UidOne);
            request.PagingInfo.IsAscending.ShouldBeFalse();
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
            Assert.Throws<ArgumentException>(() => { new UserJournalReadListRequest(currentUserId, userUid); });
        }
    }
}