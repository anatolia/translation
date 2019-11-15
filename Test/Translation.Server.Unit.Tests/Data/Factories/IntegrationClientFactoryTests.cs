using NUnit.Framework;
using Shouldly;

using StandardUtils.Helpers;

using Translation.Data.Factories;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class IntegrationClientFactoryTests
    {
        public IntegrationClientFactory IntegrationClientFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            IntegrationClientFactory = new IntegrationClientFactory();
        }

        [Test]
        public void IntegrationClientFactory_CreateEntity_Integration()
        {
            // arrange
            var integration = GetIntegration();
            var isActive = integration.IsActive;

            // act
            var result = IntegrationClientFactory.CreateEntity(integration);

            // assert
            result.OrganizationUid.ShouldBe(integration.OrganizationUid);
            result.OrganizationUid.ShouldBe(integration.OrganizationUid);
            result.OrganizationName.ShouldBe(integration.OrganizationName);

            result.IntegrationId.ShouldBe(integration.Id);
            result.IntegrationUid.ShouldBe(integration.Uid);
            result.IntegrationName.ShouldBe(integration.Name);

            result.Description.ShouldBe(integration.Description);
            result.IsActive.ShouldBe(isActive);
            result.ClientId.IsNotEmptyGuid().ShouldBeTrue();
            result.ClientSecret.IsNotEmptyGuid().ShouldBeTrue();

        }

        [Test]
        public void IntegrationClientFactory_UpdateEntityForChangeActivation()
        {
            // arrange
            var integrationClient = GetIntegrationClient();
            var isActive = integrationClient.IsActive;

            // act
            var result = IntegrationClientFactory.UpdateEntityForChangeActivation(integrationClient);

            // assert
            result.OrganizationId.ShouldBe(integrationClient.OrganizationId);
            result.OrganizationUid.ShouldBe(integrationClient.OrganizationUid);
            result.OrganizationName.ShouldBe(integrationClient.OrganizationName);

            result.IntegrationUid.ShouldBe(integrationClient.Uid);
            result.IntegrationName.ShouldBe(integrationClient.Name);

            result.Uid.ShouldBe(integrationClient.Uid);
            result.Name.ShouldBe(integrationClient.Name);

            result.Description.ShouldBe(integrationClient.Description);
            result.IsActive.ShouldBe(!isActive);
        }

        [Test]
        public void IntegrationClientFactory_CreateDtoFromEntity_IntegrationClient()
        {
            // arrange
            var integrationClient = GetIntegrationClient();

            // act
            var result = IntegrationClientFactory.CreateDtoFromEntity(integrationClient);

            // assert
            result.OrganizationUid.ShouldBe(integrationClient.OrganizationUid);
            result.OrganizationName.ShouldBe(integrationClient.OrganizationName);

            result.IntegrationUid.ShouldBe((integrationClient.Uid));
            result.IntegrationName.ShouldBe(integrationClient.Name);

            result.Uid.ShouldBe(integrationClient.Uid);
            result.Name.ShouldBe(integrationClient.Name);

            result.IsActive.ShouldBe(integrationClient.IsActive);
            result.ClientId.ShouldBe(integrationClient.ClientId);
            result.ClientSecret.ShouldBe(integrationClient.ClientSecret);
        }

        [Test]
        public void IntegrationClientFactory_UpdateEntityForRefresh_IntegrationClient()
        {
            // arrange
            var integrationClient = GetIntegrationClient();

            // act
            var result = IntegrationClientFactory.UpdateEntityForRefresh(integrationClient);

            // assert
            result.ClientId.IsNotEmptyGuid().ShouldBeTrue();
            result.ClientSecret.IsNotEmptyGuid().ShouldBeTrue();
        }


    }
}