using Translation.Common.Contracts;
using Translation.Common.Models.Shared;

namespace Translation.Client.Web.Helpers.ActionFilters
{
    public interface IJournalingController
    {
        IJournalService JournalService { get; set; }
        CurrentUser CurrentUser { get; }
    }
}