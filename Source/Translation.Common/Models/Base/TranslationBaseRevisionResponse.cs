using StandardUtils.Enumerations;
using StandardUtils.Models.DataTransferObjects;
using StandardUtils.Models.Responses;

namespace Translation.Common.Models.Base
{
    public class TranslationBaseRevisionResponse<T> : BaseRevisionResponse<T>, ITranslationBaseResponse where T : BaseDto, new()
    {
        public void SetInvalidBecauseSlugMustBeUnique(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_slug_must_be_unique");
        }

        public void SetInvalidBecauseLabelKeyMustBeUnique(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_key_must_be_unique");
        }

        public void SetInvalidBecauseNotSuperAdmin(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_not_super_admin");
        }

        public void SetInvalidBecauseNotAdmin(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_not_admin");
        }

        public void SetInvalidBecauseAdmin(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_already_admin");
        }

        public void SetFailedBecauseRevisionNotFound(string entityName = "entity")
        {
            Status = ResponseStatus.Failed;
            ErrorMessages.Add(entityName.ToLowerInvariant() + "_revision_not_found");
        }
    }
}