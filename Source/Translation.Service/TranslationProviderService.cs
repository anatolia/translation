using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;
using StandardRepository.Models;
using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Responses.TranslationProvider;
using Translation.Common.Models.Shared;
using Translation.Data.Entities.Domain;
using Translation.Data.Factories;
using Translation.Data.Repositories;
using Translation.Data.Repositories.Contracts;
using Translation.Data.UnitOfWorks.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class TranslationProviderService : ITranslationProviderService
    {
        private readonly CacheManager _cacheManager;
        private readonly ITranslationProviderRepository _translationProviderRepository;
        private readonly TranslationProviderFactory _translationProviderFactory;

        public TranslationProviderService(CacheManager cacheManager, ITranslationProviderRepository translationProviderRepository, TranslationProviderFactory translationProviderFactory)
        {
            _cacheManager = cacheManager;
            _translationProviderRepository = translationProviderRepository;
            _translationProviderFactory = translationProviderFactory;
        }

        public async Task<TranslationProviderReadResponse> GetTranslationProvider(TranslationProviderReadRequest request)
        {
            var response = new TranslationProviderReadResponse();

            var provider = await _translationProviderRepository.Select(x => x.Uid == request.TranslationProviderUid);
            if (provider.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(TranslationProvider));
                return response;
            }

            response.Item = _translationProviderFactory.CreateDtoFromEntity(provider);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<TranslationProviderReadListResponse> GetTranslationProviders(TranslationProviderReadListRequest request)
        {
            var response = new TranslationProviderReadListResponse();

            Expression<Func<TranslationProvider, bool>> filter = null;
            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.SearchTerm);
            }

            List<TranslationProvider> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _translationProviderRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, false,
                                                                            new List<OrderByInfo<TranslationProvider>>() { new OrderByInfo<TranslationProvider>(x => x.Uid, request.PagingInfo.IsAscending) });
            }
            else
            {
                entities = await _translationProviderRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, false,
                                                                           new List<OrderByInfo<TranslationProvider>>() { new OrderByInfo<TranslationProvider>(x => x.Id, request.PagingInfo.IsAscending) });
            }

            if (entities != null)
            {
                for (var i = 0; i < entities.Count; i++)
                {
                    var entity = entities[i];
                    var dto = _translationProviderFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _translationProviderRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<TranslationProviderEditResponse> EditTranslationProvider(TranslationProviderEditRequest request)
        {
            var response = new TranslationProviderEditResponse();

            var translationProvider = await _translationProviderRepository.Select(x => x.Uid == request.TranslationProviderUid);
            if (translationProvider.IsNotExist())
            {
                response.SetFailedBecauseNotFound(nameof(TranslationProvider));
                return response;
            }

            if (translationProvider.Value == request.Value)
            {
                response.Item = _translationProviderFactory.CreateDtoFromEntity(translationProvider);
                response.Status = ResponseStatus.Success;
                return response;
            }

            if (await _translationProviderRepository.Any(x => x.Value == request.Value
                                                         && x.Uid != request.TranslationProviderUid))
            {
                response.SetFailed();
                response.ErrorMessages.Add("translation_provider_already_exist");
                return response;
            }

            var updatedEntity = _translationProviderFactory.CreateEntityFromRequest(request, translationProvider);
            var result = await _translationProviderRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _translationProviderFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public ActiveTranslationProvider GetActiveTranslationProvider(ActiveTranslationProviderRequest request)
        {
            var activeTranslationProvider = _cacheManager.GetCachedActiveTranslationProvider(request.IsActive);

            return activeTranslationProvider;
        }

    }
}