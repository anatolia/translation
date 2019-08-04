using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Server.Factories
{
    [TestFixture]
    public class OrganizationFactoryTests
    {
        public OrganizationFactory OrganizationFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            OrganizationFactory = new OrganizationFactory();
        }

        [Test]
        public void OrganizationFactory_CreateEntityFromRequest_SignUpRequest()
        {
            //arrange
            var organization = GetOrganization();
            var request = GetSignUpRequest(organization);
            var key = "example_string";
            var iv = "example_string";
           
            //act
            var result = OrganizationFactory.CreateEntityFromRequest(request,key,iv);

            //assert
            result.Name.ShouldBe(request.OrganizationName);
            result.ObfuscationKey.ShouldBe(key);
            result.ObfuscationIv.ShouldBe(iv);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void OrganizationFactory_CreateEntityFromRequest_OrganizationEditRequest_Organization()
        {
            //arrange
            var organization = GetOrganization();
            var request = GetOrganizationEditRequest(organization);

            //act
            var result = OrganizationFactory.CreateEntityFromRequest(request,organization);

            //assert
            result.UpdatedBy.ShouldBe(request.CurrentUserId);
            result.Name.ShouldBe(request.Name);
            result.Description.ShouldBe(request.Description);
        }

        [Test]
        public void OrganizationFactory_CreateDtoFromEntity_Organization()
        {
            // arrange
            var entity = GetOrganization();

            // act
            var result = OrganizationFactory.CreateDtoFromEntity(entity);

            // assert
            result.Uid.ShouldBe(entity.Uid);
            result.Name.ShouldBe(entity.Name);
            result.IsActive.ShouldBe(entity.IsActive);
            result.CreatedAt.ShouldBe(entity.CreatedAt);
            result.UpdatedAt.ShouldBe(entity.UpdatedAt);
            result.IsActive.ShouldBe(entity.IsActive);

            result.Description.ShouldBe(entity.Description);
            result.IsActive.ShouldBe(entity.IsActive);
            result.UserCount.ShouldBe(entity.UserCount);
        }

        [Test]
        public void OrganizationFactory_MapCurrentOrganization_Organization()
        {
            // arrange
            var platformOrganization = GetOrganization();
                
            // act
            var result = OrganizationFactory.MapCurrentOrganization(platformOrganization);

            // assert
            result.Id.ShouldBe(platformOrganization.Id);
            result.Uid.ShouldBe(platformOrganization.Uid);
            result.Name.ShouldBe(platformOrganization.Name);
        }

    }
}