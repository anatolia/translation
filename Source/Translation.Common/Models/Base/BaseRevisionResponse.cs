using System.Collections.Generic;

using Translation.Common.Models.DataTransferObjects;

namespace Translation.Common.Models.Base
{
    public abstract class BaseRevisionResponse<T> : BaseResponse where T : BaseDto, new()
    {
        public List<RevisionDto<T>> Items { get; set; }

        protected BaseRevisionResponse()
        {
            Items = new List<RevisionDto<T>>();
        }
    }
}