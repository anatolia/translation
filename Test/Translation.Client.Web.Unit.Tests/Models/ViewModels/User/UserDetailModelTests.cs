using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Models.User;
using static Translation.Client.Web.Unit.Tests.TestHelpers.FakeModelTestHelper;
using static Translation.Client.Web.Unit.Tests.TestHelpers.AssertViewModelTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;

namespace Translation.Client.Web.Unit.Tests.Models.ViewModels.User
{
    [TestFixture]
    public class UserDetailModelTests
    {
        public UserDetailModel SystemUnderTest { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            SystemUnderTest = GetUserDetailModel();
        }

        [Test]
        public void UserDetailModel_Title()
        {
            Assert.AreEqual(SystemUnderTest.Title, "user_detail_title");
        }

        [Test]
        public void UserDetailModel_OrganizationInput()
        {
            AssertCheckboxInputModel(SystemUnderTest.IsActiveInput, "IsActive", "is_active", false, true);
        }

        [Test]
        public void UserDetailModel_Parameter()
        {
            SystemUnderTest.UserUid.ShouldBe(UidOne);
            SystemUnderTest.Username.ShouldBe(StringOne);
            SystemUnderTest.FirstName.ShouldBe(StringTwo);
            SystemUnderTest.LastName.ShouldBe(StringThree);
            SystemUnderTest.Email.ShouldBe(EmailOne);
            SystemUnderTest.Description.ShouldBe(StringFour);
            SystemUnderTest.IsAdmin.ShouldBe(BooleanTrue);
            SystemUnderTest.LabelCount.ShouldBe(Zero);
            SystemUnderTest.LabelTranslationCount.ShouldBe(Zero);
            SystemUnderTest.LabelKey.ShouldBe(StringFive);
            SystemUnderTest.LanguageName.ShouldBe(StringSix);
            SystemUnderTest.LanguageIconUrl.ShouldBe(HttpUrl);
            SystemUnderTest.OrganizationUid.ShouldBe(UidOne);
            SystemUnderTest.OrganizationName.ShouldBe(StringSeven);
            SystemUnderTest.InvitedAt.ShouldBe(DateTimeOne);
            SystemUnderTest.InvitedByUserUid.ShouldBe(UidTwo);
            SystemUnderTest.InvitedByUserName.ShouldBe(StringEight);
            SystemUnderTest.IsActive.ShouldBe(BooleanTrue);
        }
    }
}
