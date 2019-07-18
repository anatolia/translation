using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Models.Requests.Journal;
using Translation.Common.Models.Responses.Journal;
using Translation.Data.Entities.Main;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class JournalService : IJournalService
    {
        private readonly CacheManager _cacheManager;
        private readonly JournalFactory _journalFactory;
        private readonly IJournalRepository _journalRepository;

        public JournalService(CacheManager cacheManager, IJournalRepository journalRepository, JournalFactory journalFactory)
        {
            _cacheManager = cacheManager;
            _journalRepository = journalRepository;
            _journalFactory = journalFactory;
        }

        public JournalCreateResponse CreateJournal(JournalCreateRequest request)
        {
            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);

            var entity = _journalFactory.CreateEntityFromRequest(request, currentUser);
            _journalRepository.Insert(request.CurrentUserId, entity).Wait();

            var response = new JournalCreateResponse { Status = ResponseStatus.Success };
            return response;
        }

        public async Task<JournalReadListResponse> GetJournalsOfOrganization(OrganizationJournalReadListRequest request)
        {
            var response = new JournalReadListResponse();
            
            var organization = _cacheManager.GetCachedOrganization(request.OrganizationUid);
            if (organization == null)
            {
                response.SetInvalid();
                response.ErrorMessages.Add("organization_not_found");
                return response;
            }

            Expression<Func<Journal, bool>> filter = x => x.OrganizationId == organization.Id;
            
            var entities = await _journalRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _journalFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _journalRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<JournalReadListResponse> GetJournalsOfUser(UserJournalReadListRequest request)
        {
            var response = new JournalReadListResponse();
            
            var user = _cacheManager.GetCachedUser(request.UserUid);
            if (user == null)
            {
                response.SetInvalid();
                response.ErrorMessages.Add("user_not_found");
                return response;
            }

            Expression<Func<Journal, bool>> filter = x => x.UserId == user.Id;
            
            var entities = await _journalRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, x => x.Id, request.PagingInfo.IsAscending);
            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _journalFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _journalRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }
    }
}