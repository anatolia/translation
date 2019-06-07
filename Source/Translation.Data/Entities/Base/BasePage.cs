using System.Collections.Generic;

using Translation.Common.Models.Shared;

namespace Translation.Data.Entities.Base
{
    public class BasePage<T> where T : BaseEntity
    {
        public PagingInfo PagingInfo { get; set; }

        public List<T> Items { get; set; }

        public BasePage(int skip, int take)
        {
            Items = new List<T>();
            PagingInfo = new PagingInfo { Skip = skip, Take = take };
        }
    }
}