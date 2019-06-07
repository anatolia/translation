using Translation.Common.Models.Shared;

using System.Collections.Generic;

namespace Translation.Common.Models.Base
{
    public class BasePageDto<T> where T : BaseDto
    {
        public PagingInfo PagingInfo { get; set; }
        public List<T> Items { get; set; }

        public BasePageDto(int skip, int take)
        {
            Items = new List<T>();
            PagingInfo = new PagingInfo { Skip = skip, Take = take };
        }
    }
}