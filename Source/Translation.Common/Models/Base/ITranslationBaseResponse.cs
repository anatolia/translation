namespace Translation.Common.Models.Base
{
    public interface ITranslationBaseResponse
    {
        void SetInvalidBecauseSlugMustBeUnique(string entityName = "entity");
        void SetInvalidBecauseLabelKeyMustBeUnique(string entityName = "entity");
        void SetInvalidBecauseNotSuperAdmin(string entityName = "entity");
        void SetInvalidBecauseNotAdmin(string entityName = "entity");
        void SetInvalidBecauseAdmin(string entityName = "entity");
        void SetFailedBecauseRevisionNotFound(string entityName = "entity");
    }
}