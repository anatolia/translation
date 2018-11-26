using Cheviri.Common.Enumerations;

using System.Collections.Generic;

namespace Cheviri.Common.Models.Base
{
    public abstract class BaseResponse<T> where T : BaseDto
    {
        public ResponseStatus ResponseStatus { get; set; }
        public List<string> ErrorMessages { get; set; }
        public T Result { get; set; }
        public List<T> ListResult { get; set; }

        protected BaseResponse()
        {
            ResponseStatus = ResponseStatus.Unknown;
            ErrorMessages = new List<string>();
            ListResult = new List<T>();
        }
    }
}