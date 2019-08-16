using NUnit.Framework;
using Shouldly;

using Translation.Client.Web.Helpers.Mappers;
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Common.Models.DataTransferObjects;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeDtoTestHelper;

namespace Translation.Tests.Client.Mappers
{
    [TestFixture]
    public class LabelMapperTests
    {
        [Test]
        public void LabelMapper_MapLabelCreateModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = LabelMapper.MapLabelCreateModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelCreateModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.ProjectUid.ShouldBe(dto.Uid);
            result.ProjectName.ShouldBe(dto.Name);
        }

        [Test]
        public void LabelMapper_MapLabelDetailModel()
        {
            // arrange
            var dto = GetLabelDto();

            // act
            var result = LabelMapper.MapLabelDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelDetailModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.OrganizationName.ShouldBe(dto.OrganizationName);
            result.ProjectUid.ShouldBe(dto.ProjectUid);
            result.ProjectName.ShouldBe(dto.ProjectName);
            result.LabelUid.ShouldBe(dto.Uid);
            result.Key.ShouldBe(dto.Key);
            result.Description.ShouldBe(dto.Description);
            result.IsActive.ShouldBe(dto.IsActive);
        }

        [Test]
        public void LabelMapper_MapLabelEditModel()
        {
            // arrange
            var dto = GetLabelDto();

            // act
            var result = LabelMapper.MapLabelEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelEditModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.LabelUid.ShouldBe(dto.Uid);
            result.Key.ShouldBe(dto.Key);
            result.Description.ShouldBe(dto.Description);
        }

        [Test]
        public void LabelMapper_MapLabelCloneModel()
        {
            // arrange
            var dto = GetLabelDto();

            // act
            var result = LabelMapper.MapLabelCloneModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelCloneModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.CloningLabelUid.ShouldBe(dto.Uid);
            result.CloningLabelKey.ShouldBe(dto.Key);
            result.CloningLabelDescription.ShouldBe(dto.Description);
            result.CloningLabelTranslationCount.ShouldBe(dto.LabelTranslationCount);
        }

        [Test]
        public void LabelMapper_MapLabelUploadFromCSVModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = LabelMapper.MapLabelUploadFromCSVModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelUploadFromCSVModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.ProjectUid.ShouldBe(dto.Uid);
            result.ProjectName.ShouldBe(dto.Name);
        }

        [Test]
        public void LabelMapper_MapCreateBulkLabelModel()
        {
            // arrange
            var dto = GetProjectDto();

            // act
            var result = LabelMapper.MapCreateBulkLabelModel(dto);

            // assert
            result.ShouldBeAssignableTo<CreateBulkLabelModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.ProjectUid.ShouldBe(dto.Uid);
            result.ProjectName.ShouldBe(dto.Name);
        }

        [Test]
        public void LabelMapper_MapUploadLabelTranslationFromCSVFileModel()
        {
            // arrange
            var dto = GetLabelDto();

            // act
            var result = LabelMapper.MapUploadLabelTranslationFromCSVFileModel(dto);

            // assert
            result.ShouldBeAssignableTo<UploadLabelTranslationFromCSVFileModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.LabelUid.ShouldBe(dto.Uid);
            result.LabelKey.ShouldBe(dto.Key);
        }

        [Test]
        public void LabelMapper_MapLabelTranslationCreateModel()
        {
            // arrange
            var labelDto = GetLabelDto();
            var projectDto = GetProjectDto();

            // act
            var result = LabelMapper.MapLabelTranslationCreateModel(labelDto, projectDto);

            // assert
            result.ShouldBeAssignableTo<LabelTranslationCreateModel>();
            result.OrganizationUid.ShouldBe(labelDto.OrganizationUid);
            result.ProjectUid.ShouldBe(projectDto.Uid);
            result.ProjectName.ShouldBe(projectDto.Name);
            result.ProjectLanguageUid.ShouldBe(projectDto.LanguageUid);
            result.LabelUid.ShouldBe(labelDto.Uid);
            result.LabelKey.ShouldBe(labelDto.Key);
        }

        [Test]
        public void LabelMapper_MapLabelTranslationEditModel()
        {
            // arrange
            var dto = GetLabelTranslationDto();

            // act
            var result = LabelMapper.MapLabelTranslationEditModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelTranslationEditModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.LabelTranslationUid.ShouldBe(dto.Uid);
            result.Translation.ShouldBe(dto.Translation);
            result.LabelKey.ShouldBe(dto.LabelKey);
            result.LanguageName.ShouldBe(dto.LanguageName);
            result.LanguageIconUrl.ShouldBe(dto.LanguageIconUrl);
        }

        [Test]
        public void LabelMapper_MapLabelTranslationDetailModel()
        {
            // arrange
            var dto = GetLabelTranslationDto();

            // act
            var result = LabelMapper.MapLabelTranslationDetailModel(dto);

            // assert
            result.ShouldBeAssignableTo<LabelTranslationDetailModel>();
            result.OrganizationUid.ShouldBe(dto.OrganizationUid);
            result.LabelTranslationUid.ShouldBe(dto.Uid);
            result.Translation.ShouldBe(dto.Translation);
            result.LabelKey.ShouldBe(dto.LabelKey);
            result.LanguageName.ShouldBe(dto.LanguageName);
            result.LanguageIconUrl.ShouldBe(dto.LanguageIconUrl);
        }
    }
}