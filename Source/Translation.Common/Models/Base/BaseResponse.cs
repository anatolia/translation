using System.Collections.Generic;

using Translation.Common.Enumerations;
using Translation.Common.Models.Shared;

namespace Translation.Common.Models.Base
{
    public class BaseResponse
    {
        public ResponseStatus Status { get; set; }
        public List<string> ErrorMessages { get; set; }
        public List<string> SuccessMessages { get; set; }
        public List<string> WarningMessages { get; set; }
        public List<string> InfoMessages { get; set; }

        protected BaseResponse()
        {
            Status = ResponseStatus.Unknown;

            ErrorMessages = new List<string>();
            SuccessMessages = new List<string>();
            WarningMessages = new List<string>();
            InfoMessages = new List<string>();
        }

        public void SetInvalid()
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(ResponseStatus.Invalid.Description);
        }

        public void SetInvalidBecauseNotFound(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName + "_not_found");
        }

        public void SetInvalidBecauseNotActive(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName + "_not_active");
        }

        public void SetInvalidBecauseHasChildren(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName + "_has_children");
        }

        public void SetInvalidBecauseRevisionNotFound(string entityName = "entity")
        {
            Status = ResponseStatus.Invalid;
            ErrorMessages.Add(entityName + "_revision_not_found");
        }

        public void SetFailed()
        {
            Status = ResponseStatus.Failed;
            ErrorMessages.Add(ResponseStatus.Failed.Description);
        }

        public void SetFailedBecauseNameMustBeUnique(string entityName = "entity")
        {
            Status = ResponseStatus.Failed;
            ErrorMessages.Add(entityName + "_name_must_be_unique");
        }
    }

    public abstract class BaseResponse<T> : BaseResponse where T : BaseDto, new()
    {
        public T Item { get; set; }
        public List<T> Items { get; set; }

        public PagingInfo PagingInfo { get; set; }

        protected BaseResponse()
        {
            Item = new T();
            Items = new List<T>();

            PagingInfo = new PagingInfo();
        }
    }
}