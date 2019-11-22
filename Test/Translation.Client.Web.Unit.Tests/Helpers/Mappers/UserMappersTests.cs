using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.User;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class UserMapperTests
    {
        public UserMapper UserMapper { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            UserMapper = new UserMapper();
        }

        [Test]
        public void UserMapper_MapUserDetailModel()
        {
            // arrange
            var dto = GetUserDto();

            // act
            var result = UserMapper.MapUserDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<UserDetailModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.OrganizationName.ShouldBe(dto.OrganizationName);
            result.UserUid.ShouldBe(dto.Uid);
            result.Username.ShouldBe(dto.Name);
            result.FirstName.ShouldBe(dto.FirstName);
            result.LastName.ShouldBe(dto.LastName);
            result.Email.ShouldBe(dto.Email);
            result.Description.ShouldBe(dto.Description);
            result.IsActive.ShouldBe(dto.IsActive);
            result.InvitedAt.ShouldBe(dto.InvitedAt);
            result.InvitedByUserUid.ShouldBe(dto.InvitedByUserUid);
            result.InvitedByUserName.ShouldBe(dto.InvitedByUserName);
            result.LabelCount.ShouldBe(dto.LabelCount);
            result.LabelTranslationCount.ShouldBe(dto.LabelTranslationCount);
        }
    }
}