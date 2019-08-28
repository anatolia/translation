using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Admin;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class AdminMapperTests
    {
        [Test]
        public void AdminMapper_MapAdminInviteModel()
        {
            // arrange

            // act
            var result = AdminMapper.MapAdminInviteModel(UidOne);

            // assert
            result.ShouldBeAssignableTo<AdminInviteModel>();
            result.OrganizationUid.ShouldBe(UidOne);
        }

        [Test]
        public void AdminMapper_MapAdminAcceptInviteModel()
        {
            // arrange
            var dto = GetUserDto();

            // act
            var result = AdminMapper.MapAdminAcceptInviteModel(dto, UidOne, EmailOne);

            // assert
            result.ShouldBeAssignableTo<AdminAcceptInviteModel>();
            result.FirstName.ShouldBe(dto.FirstName);
            result.LastName.ShouldBe(dto.LastName);
            result.Token.ShouldBe(UidOne);
            result.Email.ShouldBe(EmailOne);
        }
    }
}