using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.Server.Factories
{
    [TestFixture]
    public class ProjectFactoryTests
    {
        public ProjectFactory ProjectFactory { get; set; }

        [SetUp]
        public void run_before_every_test()
        {
            ProjectFactory = new ProjectFactory();
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectEditRequest()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectEditRequest(project);
            
            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, project);

            // assert
            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);
            
            result.Uid.ShouldBe(request.ProjectUid);
            result.Name.ShouldBe(request.ProjectName);

            result.Url.ShouldBe(request.Url);
            result.Description.ShouldBe(request.Description);
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCreateRequest_Organization()
        {
            // arrange
            var organization = GetOrganizationOne();
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCreateRequest(organization, project);

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, organization);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.Name.ShouldBe(request.ProjectName);

            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCreateRequest_CurrentOrganization()
        {
            // arrange
            var organization = GetCurrentOrganizationOne();
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCreateRequest(organization, project);

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, organization);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.Name.ShouldBe(request.ProjectName);

            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCloneRequest_Project()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCloneRequest(project);

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, project);

            // assert
            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.Uid.ShouldBe(request.CloningProjectUid);
            result.Name.ShouldBe(request.Name);

            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            // todo: result.LabelCount.ShouldBe(request.LabelCount);
        }

        [Test]
        public void ProjectFactory_CreateDtoFromEntity_Project()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();

            // act
            var result = ProjectFactory.CreateDtoFromEntity(project);

            // assert
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.Uid.ShouldBe(project.Uid);
            result.Name.ShouldBe(project.Name);

            result.Url.ShouldBe(project.Url);
            result.Description.ShouldBe(project.Description);
            result.IsActive.ShouldBe(project.IsActive);
        }

        [Test]
        public void ProjectFactory_UpdateEntityForChangeActivation()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();
            var isActive = project.IsActive;

            // act
            var result = ProjectFactory.UpdateEntityForChangeActivation(project);

            // assert
            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.Id.ShouldBe(project.Id);
            result.Uid.ShouldBe(project.Uid);
            result.Name.ShouldBe(project.Name);

            result.Url.ShouldBe(project.Url);
            result.Description.ShouldBe(project.Description);
            result.IsActive.ShouldBe(!isActive);
        }

        [Test]
        public void ProjectFactory_CreateDefault()
        {
            // arrange
            var organization = GetOrganizationOne();

            // act
            var result = ProjectFactory.CreateDefault(organization);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.Name.ShouldBe("Default");

            result.IsActive.ShouldBeTrue();
        }
    }
}