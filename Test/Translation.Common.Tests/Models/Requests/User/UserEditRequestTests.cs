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
    public class UserEditRequestTests
    {
        [Test]
        public void UserEditRequest_Constructor()
        {
            var request = GetUserEditRequest(CurrentUserId, UidOne, StringOne, StringTwo, UidTwo);

            request.CurrentUserId.ShouldBe(CurrentUserId);
            request.UserUid.ShouldBe(UidOne);
            request.FirstName.ShouldBe(StringOne);
            request.LastName.ShouldBe(StringTwo);
            request.LanguageUid.ShouldBe(UidTwo);
        }

        public static IEnumerable ArgumentTestCases
        {
            get
            {
                yield return new TestCaseData(CurrentUserId, UidOne, EmptyString, StringTwo, UidTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, EmptyString, UidTwo);
                yield return new TestCaseData(CurrentUserId, UidOne, StringOne, StringTwo, EmptyUid);
            }
        }

        [TestCaseSource(nameof(ArgumentTestCases))]
        public void UserEditRequest_Argument_Validations(long currentUserId, Guid userUid, string firsName,
                                                         string lastName, Guid languageUid)
        {
            Assert.Throws<ArgumentException>(() => { new UserEditRequest(currentUserId, userUid, firsName, lastName, languageUid); });
        }
    }
}