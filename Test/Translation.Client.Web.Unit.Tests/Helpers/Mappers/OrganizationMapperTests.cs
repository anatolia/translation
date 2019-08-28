using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Organization;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class OrganizationMapperTests
    {
        [Test]
        public void OrganizationMapper_MapOrganizationDetailModel()
        {
            // arrange
            var dto = GetOrganizationDto();

            // act
            var result = OrganizationMapper.MapOrganizationDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<OrganizationDetailModel>();
            result.OrganizationUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
            result.IsActive.ShouldBe(dto.IsActive);
        }

        [Test]
        public void OrganizationMapper_MapOrganizationEditModel()
        {
            // arrange
            var dto = GetOrganizationDto();

            // act
            var result = OrganizationMapper.MapOrganizationEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<OrganizationEditModel>();
            result.OrganizationUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
        }
    }
}