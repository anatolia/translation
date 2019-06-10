using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using StandardRepository.Helpers;

using Translation.Common.Contracts;
using Translation.Common.Enumerations;
using Translation.Common.Helpers;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Responses.Language;
using Translation.Data.Entities.Parameter;
using Translation.Data.Factories;
using Translation.Data.Repositories.Contracts;
using Translation.Service.Managers;

namespace Translation.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly CacheManager _cacheManager;
        private readonly ILanguageRepository _languageRepository;
        private readonly LanguageFactory _languageFactory;

        public LanguageService(CacheManager cacheManager, 
                               ILanguageRepository languageRepository, LanguageFactory languageFactory)
        {
            _cacheManager = cacheManager;
            _languageRepository = languageRepository;
            _languageFactory = languageFactory;
        }

        public async Task<LanguageReadResponse> GetLanguage(LanguageReadRequest request)
        {
            var response = new LanguageReadResponse();

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            response.Item = _languageFactory.CreateDtoFromEntity(language);
            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LanguageReadListResponse> GetLanguages(LanguageReadListRequest request)
        {
            var response = new LanguageReadListResponse();

            Expression<Func<Language, object>> orderByColumn = x => x.Id;

            Expression<Func<Language, bool>> filter = null;
            if (request.SearchTerm.IsNotEmpty())
            {
                filter = x => x.Name.Contains(request.SearchTerm) 
                              || x.IsoCode2Char.Contains(request.SearchTerm)
                              || x.IsoCode3Char.Contains(request.SearchTerm)
                              || x.OriginalName.Contains(request.SearchTerm);
            }

            List<Language> entities;
            if (request.PagingInfo.Skip < 1)
            {
                entities = await _languageRepository.SelectAfter(filter, request.PagingInfo.LastUid, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }
            else
            {
                entities = await _languageRepository.SelectMany(filter, request.PagingInfo.Skip, request.PagingInfo.Take, orderByColumn, request.PagingInfo.IsAscending);
            }

            if (entities != null)
            {
                foreach (var entity in entities)
                {
                    var dto = _languageFactory.CreateDtoFromEntity(entity);
                    response.Items.Add(dto);
                }
            }

            response.PagingInfo.Skip = request.PagingInfo.Skip;
            response.PagingInfo.Take = request.PagingInfo.Take;
            response.PagingInfo.LastUid = request.PagingInfo.LastUid;
            response.PagingInfo.IsAscending = request.PagingInfo.IsAscending;
            response.PagingInfo.TotalItemCount = await _languageRepository.Count(filter);

            response.Status = ResponseStatus.Success;
            return response;
        }

        public async Task<LanguageCreateResponse> CreateLanguage(LanguageCreateRequest request)
        {
            var response = new LanguageCreateResponse();

            var currentUser = _cacheManager.GetCachedUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var trimName = request.Name.Trim();
            var result = await _languageRepository.Any(x => x.Name == trimName
                                                            || x.IsoCode2Char == request.IsoCode2
                                                            || x.IsoCode3Char == request.IsoCode3);
            if (result)
            {
                response.ErrorMessages.Add("language_name_must_be_unique");
                response.Status = ResponseStatus.Invalid;
                return response;
            }

            var entity = _languageFactory.CreateEntityFromRequest(request);
            var id = await _languageRepository.Insert(request.CurrentUserId, entity);
            if (id > 0)
            {
                response.Item = _languageFactory.CreateDtoFromEntity(entity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LanguageEditResponse> EditLanguage(LanguageEditRequest request)
        {
            var response = new LanguageEditResponse();

            var currentUser = _cacheManager.GetCachedUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var trimName = request.Name.Trim();
            if (await _languageRepository.Any(x => (x.Name == trimName
                                                         || x.IsoCode2Char == request.IsoCode2
                                                         || x.IsoCode3Char == request.IsoCode3) && x.Uid != request.LanguageUid))
            {

            }

            var updatedEntity = _languageFactory.CreateEntityFromRequest(request, language);
            var result = await _languageRepository.Update(request.CurrentUserId, updatedEntity);
            if (result)
            {
                response.Item = _languageFactory.CreateDtoFromEntity(updatedEntity);
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }

        public async Task<LanguageDeleteResponse> DeleteLanguage(LanguageDeleteRequest request)
        {
            var response = new LanguageDeleteResponse();

            var currentUser = _cacheManager.GetCachedUser(request.CurrentUserId);
            if (!currentUser.IsSuperAdmin)
            {
                response.SetInvalid();
                return response;
            }

            var language = await _languageRepository.Select(x => x.Uid == request.LanguageUid);
            if (language.IsNotExist())
            {
                response.SetInvalidBecauseEntityNotFound();
                return response;
            }

            var result = await _languageRepository.Delete(request.CurrentUserId, language.Id);
            if (result)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.SetFailed();
            return response;
        }
    }
}