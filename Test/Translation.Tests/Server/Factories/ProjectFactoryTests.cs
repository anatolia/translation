using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Tests.TestHelpers.GetFakeRequestTestHelper;
using static Translation.Tests.TestHelpers.GetFakeEntityTestHelper;

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
            result.UpdatedBy.ShouldBe(request.CurrentUserId);
            result.Name.ShouldBe(request.ProjectName);
            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
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
            result.Name.ShouldBe(request.ProjectName);
            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);
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
            result.Name.ShouldBe(request.ProjectName);
            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);
        }


        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCreateRequest_Project()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCloneRequest(project);

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, project);

            // assert
            result.Name.ShouldBe(request.Name);
            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.LabelCount.ShouldBe(project.LabelCount);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);
        }

        [Test]
        public void ProjectFactory_CreateDtoFromEntity_Project()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();

            // act
            var result = ProjectFactory.CreateDtoFromEntity(project);

            // assert
            result.CreatedAt.ShouldBe(project.CreatedAt);
            result.UpdatedAt.ShouldBe(project.UpdatedAt);
            result.Name.ShouldBe(project.Name);
            result.Url.ShouldBe(project.Url);
            result.Description.ShouldBe(project.Description);
            result.IsActive.ShouldBe(project.IsActive);

            result.Uid.ShouldBe(project.Uid);

            result.OrganizationName.ShouldBe(project.OrganizationName);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
        }
    }
}