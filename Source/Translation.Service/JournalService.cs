using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Models;
using StandardUtils.Enumerations;

using Translation.Common.Contracts;
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
            var currentUser = _cacheManager.GetCachedCurrentUser(request.CurrentUserId);
            var organization = _cacheManager.GetCachedOrganization(currentUser.OrganizationUid);
            if (organization == null)
            {
                response.SetFailedBecauseNotFound(nameof(Organization));
                return response;
            }

            Expression<Func<Journal, bool>> filter = x => x.OrganizationId == organization.Id;

            var entities = await _journalRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                               new List<OrderByInfo<Journal>>() { new OrderByInfo<Journal>(x => x.Id, request.PagingInfo.IsAscending) });

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
                response.SetFailedBecauseNotFound(nameof(User));
                return response;
            }

            Expression<Func<Journal, bool>> filter = x => x.UserId == user.Id;

            var entities = await _journalRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                               new List<OrderByInfo<Journal>>() { new OrderByInfo<Journal>(x => x.Id, request.PagingInfo.IsAscending) });
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