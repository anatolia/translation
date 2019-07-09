using System;

using Translation.Common.Models.Base;

namespace Translation.Common.Models.Requests.Label
{
    public sealed class LabelSearchListRequest : BaseAuthenticatedPagedRequest
    {
        public LabelSearchListRequest(long currentUserId, string searchTerm) : base(currentUserId)
        {
            PagingInfo.Take = 6;
            SearchTerm = searchTerm;
        }
    }
}