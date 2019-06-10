using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Language;
using Translation.Data.Entities.Parameter;

namespace Translation.Data.Factories
{
    public class LanguageFactory
    {
        public Language CreateEntity(string isoCode2Char, string isoCode3Char, string name, string originalName)
        {
            var entity = new Language();
            entity.IsoCode2Char = isoCode2Char;
            entity.IsoCode3Char = isoCode3Char;
            entity.IconUrl = $"/images/flags/{isoCode2Char}.png";
            entity.Name = name;
            entity.OriginalName = originalName;

            return entity;
        }

        public LanguageDto CreateDtoFromEntity(Language entity)
        {
            var dto = new LanguageDto();

            dto.Uid = entity.Uid;
            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;
            dto.OriginalName = entity.OriginalName;
            dto.Name = entity.Name;
            dto.IsoCode2 = entity.IsoCode2Char;
            dto.IsoCode3 = entity.IsoCode3Char;
            dto.IconPath = entity.IconUrl;
            dto.Description = entity.Description;

            return dto;
        }

        public Language CreateEntityFromRequest(LanguageCreateRequest request)
        {
            var entity = new Language();

            entity.CreatedBy = request.CurrentUserId;
            entity.Name = request.Name;
            entity.IsoCode2Char = request.IsoCode2;
            entity.IsoCode3Char = request.IsoCode3;
            entity.IconUrl = request.Icon;
            entity.Description = request.Description;

            return entity;
        }

        public Language CreateEntityFromRequest(LanguageEditRequest request, Language entity)
        {
            entity.Name = request.Name;
            entity.OriginalName = request.OriginalName;
            entity.Description = request.Description;
            entity.IsoCode2Char = request.IsoCode2;
            entity.IsoCode3Char = request.IsoCode3;
            entity.IconUrl = request.Icon;

            return entity;
        }
    }
}