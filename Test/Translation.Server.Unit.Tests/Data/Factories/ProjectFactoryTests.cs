using NUnit.Framework;
using Shouldly;

using Translation.Data.Factories;
using static Translation.Common.Tests.TestHelpers.FakeRequestTestHelper;
using static Translation.Server.Unit.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Server.Unit.Tests.Data.Factories
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
            var language = GetLanguageOne();

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, project, language);

            // assert
            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);
            
            result.Uid.ShouldBe(request.ProjectUid);
            result.Name.ShouldBe(request.ProjectName);

            result.Url.ShouldBe(request.Url);
            result.Description.ShouldBe(request.Description);

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageIconUrl.ShouldBe(language.IconUrl);
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCreateRequest_CurrentOrganization()
        {
            // arrange
            var organization = GetCurrentOrganizationOne();
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCreateRequest(organization, project);
            var language = GetLanguageOne();

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, organization, language);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.Name.ShouldBe(request.ProjectName);

            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.IsActive.ShouldBeTrue();

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageIconUrl.ShouldBe(language.IconUrl);
        }

        [Test]
        public void ProjectFactory_CreateEntityFromRequest_ProjectCloneRequest_Project()
        {
            // arrange
            var project = GetOrganizationOneProjectOne();
            var request = GetProjectCloneRequest(project);
            var language = GetLanguageOne();

            // act
            var result = ProjectFactory.CreateEntityFromRequest(request, project, language);

            // assert
            result.OrganizationId.ShouldBe(project.OrganizationId);
            result.OrganizationUid.ShouldBe(project.OrganizationUid);
            result.OrganizationName.ShouldBe(project.OrganizationName);

            result.Name.ShouldBe(request.Name);
            result.Slug.ShouldBe(request.Slug);

            result.Description.ShouldBe(request.Description);
            result.Url.ShouldBe(request.Url);
            result.LabelCount.ShouldBe(request.LabelCount);
            result.LabelTranslationCount.ShouldBe(request.LabelTranslationCount);
            result.IsSuperProject.ShouldBe(request.IsSuperProject);

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageIconUrl.ShouldBe(language.IconUrl);
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
            var language = GetLanguageOne();

            // act
            var result = ProjectFactory.CreateDefault(organization, language);

            // assert
            result.OrganizationId.ShouldBe(organization.Id);
            result.OrganizationUid.ShouldBe(organization.Uid);
            result.OrganizationName.ShouldBe(organization.Name);

            result.LanguageId.ShouldBe(language.Id);
            result.LanguageUid.ShouldBe(language.Uid);
            result.LanguageName.ShouldBe(language.Name);
            result.LanguageIconUrl.ShouldBe(language.IconUrl);

            result.Name.ShouldBe("Default");

            result.IsActive.ShouldBeTrue();
        }
    }
}