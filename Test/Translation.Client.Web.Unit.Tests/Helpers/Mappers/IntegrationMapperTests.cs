using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Integration;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class IntegrationMapperTests
    {
        [Test]
        public void IntegrationMapper_MapIntegrationCreateModel()
        {
            // arrange

            // act 
            var result = IntegrationMapper.MapIntegrationCreateModel(UidOne);

            // assert
            result.ShouldBeAssignableTo<IntegrationCreateModel>();
            result.OrganizationUid.ShouldBe(UidOne);
        }

        [Test]
        public void IntegrationMapper_MapIntegrationDetailModel()
        {
            // arrange
            var dto = GetIntegrationDto();

            // act
            var result = IntegrationMapper.MapIntegrationDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<IntegrationDetailModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.OrganizationName.ShouldBe(dto.OrganizationName);
            result.IntegrationUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
        }

        [Test]
        public void IntegrationMapper_MapIntegrationActiveTokensModel()
        {
            // arrange
            var dto = GetIntegrationDto();

            // act
            var result = IntegrationMapper.MapIntegrationActiveTokensModel(dto);

            // assert
            result.ShouldBeAssignableTo<IntegrationActiveTokensModel>();
            result.IntegrationUid.ShouldBe(dto.Uid);
            result.IntegrationName.ShouldBe(dto.Name);
        }

        [Test]
        public void IntegrationMapper_MapIntegrationClientActiveTokensModel()
        {
            // arrange
            var dto = GetIntegrationClientDto();

            // act
            var result = IntegrationMapper.MapIntegrationClientActiveTokensModel(dto);

            // assert
            result.ShouldBeAssignableTo<IntegrationClientActiveTokensModel>();
            result.IntegrationUid.ShouldBe(dto.IntegrationUid);
            result.IntegrationName.ShouldBe(dto.IntegrationName);
            result.ClientUid.ShouldBe(dto.Uid);
        }

        [Test]
        public void IntegrationMapper_MapIntegrationClientTokenRequestLogsModel()
        {
            // arrange

            // act
            var result = IntegrationMapper.MapIntegrationClientTokenRequestLogsModel(UidOne);

            // assert
            result.ShouldBeAssignableTo<IntegrationClientTokenRequestLogsModel>();
            result.IntegrationClientUid.ShouldBe(UidOne);
        }

        [Test]
        public void IntegrationMapper_MapIntegrationEditModel()
        {
            // arrange
            var dto = GetIntegrationDto();

            // act
            var result = IntegrationMapper.MapIntegrationEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<IntegrationEditModel>();
            result.IntegrationUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Description.ShouldBe(dto.Description);
        }
    }
}