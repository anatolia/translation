using NUnit.Framework;
using Shouldly;

using StandardUtils.Helpers;
using Translation.Data.Factories;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class TokenFactoryTests
    {
        public TokenFactory TokenFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            TokenFactory = new TokenFactory();
        }

        [Test]
        public void TokenFactory_CreateEntity_Project_IntegrationClient()
        {
            // arrange
            var integrationClient = GetIntegrationClient();

            // act
            var result = TokenFactory.CreateEntity(integrationClient);

            // assert
            result.AccessToken.IsNotEmptyGuid().ShouldBeTrue();
            result.ExpiresAt.ShouldBe(result.CreatedAt.AddMinutes(30));
            result.IsActive.ShouldBeTrue();

            result.IntegrationClientUid.ShouldBe(integrationClient.Uid);
            result.IntegrationClientId.ShouldBe(integrationClient.Id);
            result.IntegrationClientName.ShouldBe(integrationClient.Name);

            result.IntegrationUid.ShouldBe(integrationClient.IntegrationUid);
            result.IntegrationId.ShouldBe(integrationClient.IntegrationId);
            result.IntegrationName.ShouldBe(integrationClient.IntegrationName);

            result.OrganizationUid.ShouldBe(integrationClient.OrganizationUid);
            result.OrganizationId.ShouldBe(integrationClient.OrganizationId);
            result.OrganizationName.ShouldBe(integrationClient.OrganizationName);
        }

        [Test]
        public void TokenFactory_CreateEntityFromRequest_LanguageCreateRequest()
        {
            //arrange
            var integrationClient = GetIntegrationClient();
            var request = GetTokenCreateRequest(integrationClient);

            //act
            var result = TokenFactory.CreateEntityFromRequest(request, integrationClient);

            //assert
            result.Ip.ShouldBe(request.IP.ToString());
        }

        [Test]
        public void TokenFactory_CreateDtoFromEntity_Language()
        {
            // arrange
            var entity = GetToken();
            // act
            var result = TokenFactory.CreateDtoFromEntity(entity);

            // assert
            result.Uid.ShouldBe(entity.Uid);
            result.IntegrationClientUid.ShouldBe(entity.IntegrationClientUid);
            result.AccessToken.ShouldBe(entity.AccessToken);
            result.CreatedAt.ShouldBe(entity.CreatedAt);
            result.ExpiresAt.ShouldBe(entity.ExpiresAt);
        }
    }
}