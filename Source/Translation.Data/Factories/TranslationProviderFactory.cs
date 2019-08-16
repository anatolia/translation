using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;

namespace Translation.Data.Factories
{
    public class TranslationProviderFactory
    {
        public TranslationProvider CreateEntity(string name, bool isActive, string value)
        {
            var entity = new TranslationProvider();
            entity.Name = name;
            entity.IsActive = isActive;
            entity.Value = value;

            return entity;
        }

        public TranslationProviderDto CreateDtoFromEntity(TranslationProvider entity)
        {
            var dto = new TranslationProviderDto();

            dto.Uid = entity.Uid;
            dto.CreatedAt = entity.CreatedAt;
            dto.UpdatedAt = entity.UpdatedAt;
            dto.Name = entity.Name;
            dto.IsActive = entity.IsActive;
            dto.Value = entity.Value;
            dto.Description = entity.Description;

            return dto;
        }

        public TranslationProvider CreateEntityFromRequest(TranslationProviderEditRequest request, TranslationProvider entity)
        {
            entity.UpdatedBy = request.CurrentUserId;
            entity.Value = request.Value;
            entity.Description = request.Description;

            return entity;
        }

        public CurrentTranslationProvider MapCurrentTranslationProvider(TranslationProvider platformTranslationProvider)
        {
            var currentTranslationProvider = new CurrentTranslationProvider();
            currentTranslationProvider.Id = platformTranslationProvider.Id;
            currentTranslationProvider.Uid = platformTranslationProvider.Uid;
            currentTranslationProvider.Name = platformTranslationProvider.Name;

            return currentTranslationProvider;
        }

    }
}