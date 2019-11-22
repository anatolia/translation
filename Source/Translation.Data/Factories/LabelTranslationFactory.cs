using System;

using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Label.LabelTranslation;
using Translation.Data.Entities.Domain;
using Translation.Data.Entities.Parameter;

namespace Translation.Data.Factories
{
    public class LabelTranslationFactory
    {
        public LabelTranslation CreateEntity(string translation, Label label, Language language)
        {
            var entity = new LabelTranslation();
            entity.TranslationText = translation;
            entity.IsActive = true;

            entity.OrganizationId = label.OrganizationId;
            entity.OrganizationUid = label.OrganizationUid;
            entity.OrganizationName = label.OrganizationName;

            entity.ProjectId = label.ProjectId;
            entity.ProjectUid = label.ProjectUid;
            entity.ProjectName = label.ProjectName;

            entity.LabelId = label.Id;
            entity.LabelUid = label.Uid;
            entity.LabelName = label.LabelKey;

            entity.LanguageId = language.Id;
            entity.LanguageUid = language.Uid;
            entity.LanguageName = language.Name;

            return entity;
        }

        public LabelTranslation CreateEntityFromRequest(LabelTranslationEditRequest request, LabelTranslation entity)
        {
            entity.TranslationText = request.NewTranslation;

            return entity;
        }

        public LabelTranslationDto CreateDtoFromEntity(LabelTranslation entity, Language language)
        {
            var dto = CreateDtoFromEntity(entity);

            dto.LanguageIconUrl = language.IconUrl;
            dto.LanguageIsoCode2 = language.IsoCode2Char;

            return dto;
        }

        public LabelTranslationDto CreateDtoFromEntity(LabelTranslation entity)
        {
            var dto = new LabelTranslationDto();
            dto.Uid = entity.Uid;
            dto.Translation = entity.TranslationText;
            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;
            entity.IsActive = entity.IsActive;

            dto.OrganizationUid = entity.OrganizationUid;
            dto.OrganizationName = entity.OrganizationName;

            dto.ProjectUid = entity.ProjectUid;
            dto.ProjectName = entity.ProjectName;

            dto.LabelUid = entity.LabelUid;
            dto.LabelKey = entity.LabelName;

            dto.LanguageUid = entity.LanguageUid;
            dto.LanguageName = entity.LanguageName;

            return dto;
        }
    }
}