using System.Threading.Tasks;

using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Responses.Journal;

namespace Translation.Common.Contracts
{
    public interface IJournalService
    {
        JournalCreateResponse CreateJournal(JournalCreateRequest request);
        Task<JournalReadListResponse> GetJournalsOfOrganization(OrganizationJournalReadListRequest request);
        Task<JournalReadListResponse> GetJournalsOfUser(UserJournalReadListRequest request);
    }
}