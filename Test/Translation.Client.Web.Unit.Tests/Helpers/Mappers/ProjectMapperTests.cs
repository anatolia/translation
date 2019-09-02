using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Project;
using static Translation.Common.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Common.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Client.Web.Unit.Tests.Helpers.Mappers
{
    [TestFixture]
    public class ProjectMapperTests
    {
        [Test]
        public void ProjectMapper_MapProjectCreateModel()
        {
            // arrange

            // act
            var result = ProjectMapper.MapProjectCreateModel(OrganizationOneUid);

            // assert
            result.ShouldBeAssignableTo<ProjectCreateModel>();
            result.OrganizationUid.ShouldBe(OrganizationOneUid);
        }

        [Test]
        public void ProjectMapper_MapProjectDetailModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = ProjectMapper.MapProjectDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<ProjectDetailModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.OrganizationName.ShouldBe(dto.OrganizationName);
            result.Name.ShouldBe(dto.Name);
            result.Slug.ShouldBe(dto.Slug);
            result.Description.ShouldBe(dto.Description);
            result.Url.ShouldBe(dto.Url);
            result.LabelCount.ShouldBe(dto.LabelCount);
            result.IsActive.ShouldBe(dto.IsActive);
        }

        [Test]
        public void ProjectMapper_MapProjectEditModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = ProjectMapper.MapProjectEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<ProjectEditModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.ProjectUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Slug.ShouldBe(dto.Slug);
            result.Url.ShouldBe(dto.Url);
            result.Description.ShouldBe(dto.Description);
        }

        [Test]
        public void ProjectMapper_MapProjectCloneModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = ProjectMapper.MapProjectCloneModel(dto);

            // assert
            result.ShouldBeAssignableTo<ProjectCloneModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.CloningProjectUid.ShouldBe(dto.Uid);
            result.Name.ShouldBe(dto.Name);
            result.Url.ShouldBe(dto.Url);
            result.Description.ShouldBe(dto.Description);
            result.LabelCount.ShouldBe(dto.LabelCount);
            result.LabelTranslationCount.ShouldBe(dto.LabelTranslationCount);
            result.IsSuperProject.ShouldBe(dto.IsSuperProject);
        }
    }
}