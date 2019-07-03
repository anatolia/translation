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
            var project = GetParkNetProjectOne();
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
        public void ProjectFactory_CreateEntityFromRequest_ProjectCreateRequestAndOrganization()
        {
            // arrange
            var organization = GetCurrentParkNet();
            var project = GetParkNetProjectOne();
            var request = GetProjectCreateRequest(organization, project);

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, organization);

            // assert
            result.CreatedBy.ShouldBe(request.CurrentUserId);
            result.Name.ShouldBe(request.ProjectName);
            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();

            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);
        }
    }
}