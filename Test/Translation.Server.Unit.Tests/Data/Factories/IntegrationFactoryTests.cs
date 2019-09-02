using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
{
    [TestFixture]
    public class IntegrationFactoryTests
    {
        public IntegrationFactory IntegrationFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            IntegrationFactory = new IntegrationFactory();
        }

        [Test]
        public void IntegrationFactory_CreateEntityFromRequest_IntegrationCreateRequest_Organization()
        {
            // arrange
            var organization = GetOrganizationOne();
            var integration = GetOrganizationOneIntegrationOne();
            var request = GetIntegrationCreateRequest(integration, organization);

            // act
            var result = IntegrationFactory.CreateEntityFromRequest(request, organization);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);


            result.Name.ShouldBe(request.Name);
            result.Description.ShouldBe(request.Description);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void IntegrationFactory_CreateEntityFromRequest_IntegrationCreateRequest_CurrentOrganization()
        {
            // arrange
            var currentOrganization = GetCurrentOrganizationOne();
            var integration = GetOrganizationOneIntegrationOne();
            var request = GetIntegrationCreateRequest( integration,currentOrganization);

            // act
            var result = IntegrationFactory.CreateEntityFromRequest(request, currentOrganization);

            // assert
            result.OrganizationId.ShouldBe(currentOrganization.Id);
            result.OrganizationUid.ShouldBe(currentOrganization.Uid);
            result.OrganizationName.ShouldBe(currentOrganization.Name);

            result.Name.ShouldBe(request.Name);
            result.Description.ShouldBe(request.Description);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void IntegrationFactory_CreateEntityFromRequest_integrationEditRequest()
        {
            // arrange
            var integration = GetOrganizationOneIntegrationOne();
            var request = GetIntegrationEditRequest(integration);

            // act
            var result = IntegrationFactory.CreateEntityFromRequest(request, integration);

            // assert
            result.OrganizationId.ShouldBe(integration.OrganizationId);
            result.OrganizationUid.ShouldBe(integration.OrganizationUid);
            result.OrganizationName.ShouldBe(integration.OrganizationName);

            result.Name.ShouldBe(request.Name);
            result.Description.ShouldBe(request.Description);
        }

        [Test]
        public void IntegrationFactory_UpdateEntityForChangeActivation()
        {
            // arrange
            var integration = GetOrganizationOneIntegrationOne();
            var isActive = integration.IsActive;

            // act
            var result = IntegrationFactory.UpdateEntityForChangeActivation(integration);

            // assert
            result.OrganizationId.ShouldBe(integration.OrganizationId);
            result.OrganizationUid.ShouldBe(integration.OrganizationUid);
            result.OrganizationName.ShouldBe(integration.OrganizationName);

            result.Id.ShouldBe(integration.Id);
            result.Uid.ShouldBe(integration.Uid);
            result.Name.ShouldBe(integration.Name);

            result.Description.ShouldBe(integration.Description);
            result.IsActive.ShouldBe(!isActive);
        }

        [Test]
        public void IntegrationFactory_CreateDtoFromEntity_integration()
        {
            // arrange
            var integration = GetOrganizationOneIntegrationOne();

            // act
            var result = IntegrationFactory.CreateDtoFromEntity(integration);

            // assert
            result.OrganizationUid.ShouldBe(integration.OrganizationUid);
            result.OrganizationName.ShouldBe(integration.OrganizationName);

            result.Uid.ShouldBe(integration.Uid);
            result.Name.ShouldBe(integration.Name);

            result.Description.ShouldBe(integration.Description);
            result.IsActive.ShouldBe(integration.IsActive);
            result.CreatedAt.ShouldNotBeNull();
        }

        [Test]
        public void IntegrationFactory_CreateDefault()
        {
            // arrange
            var organization = GetOrganizationOne();

            // act
            var result = IntegrationFactory.CreateDefault(organization);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.Name.ShouldBe("Default");

            result.IsActive.ShouldBeTrue();
        }

    }
}