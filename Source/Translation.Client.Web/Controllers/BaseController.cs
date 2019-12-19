using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using StandardUtils.Models.Requests;
using StandardUtils.Models.Shared;

using Translation.Client.Web.Helpers;
using Translation.Client.Web.Helpers.ActionFilters;
using Translation.Common.Contracts;
using Translation.Common.Models.DataTransferObjects;
using Translation.Common.Models.Requests.Language;
using Translation.Common.Models.Requests.TranslationProvider;
using Translation.Common.Models.Requests.User;
using Translation.Common.Models.Shared;

namespace Translation.Client.Web.Controllers
{
    public class BaseController : Controller, IJournalingController
    {
        public IOrganizationService OrganizationService { get; set; }
        public IJournalService JournalService { get; set; }
        public ILanguageService LanguageService { get; set; }
        public ITranslationProviderService TranslationProviderService { get; set; }
        
        public BaseController(IOrganizationService organizationService,
                              IJournalService journalService,
                              ILanguageService languageService,
                              ITranslationProviderService translationProviderService)
        {
            OrganizationService = organizationService;
            JournalService = journalService;
            TranslationProviderService = translationProviderService;
            LanguageService = languageService;
        }

        public static string RedirectToAccessDeniedPath = "/Home/AccessDenied";
        public static string RedirectToHomePath = "/";

        private List<LanguageDto> _languages;
        public List<LanguageDto> Languages
        {
            get
            {
                if (_languages == null)
                {
                    var request = new LanguageReadListRequest();
                    _languages = LanguageService.GetLanguages(request).Result.Items;
                }
                return _languages;
            }
        }

        private CurrentUser _currentUser;
        public CurrentUser CurrentUser
        {
            get
            {
                if (_currentUser == null
                    && User.Identity.IsAuthenticated)
                {
                    var email = User.Claims.First(x => x.Type == ClaimTypes.Email).Value;
                    var currentUserRequest = new CurrentUserRequest(email);
                    _currentUser = OrganizationService.GetCurrentUser(currentUserRequest);
                }

                return _currentUser;
            }
        }

        private ActiveTranslationProvider _activeTranslationProvider;
        public ActiveTranslationProvider ActiveTranslationProvider
        {
            get
            {
                if (_activeTranslationProvider == null)
                {
                    var activeTranslationProviderRequest = new ActiveTranslationProviderRequest(); ;
                    _activeTranslationProvider = TranslationProviderService.GetActiveTranslationProvider(activeTranslationProviderRequest);
                }

                return _activeTranslationProvider;
            }
        }

        public RedirectResult RedirectToHome()
        {
            return Redirect(RedirectToHomePath);
        }

        public RedirectResult RedirectToAccessDenied()
        {
            return Redirect(RedirectToAccessDeniedPath);
        }

        public ClientLogInfo GetClientInfoLog()
        {
            var log = new ClientLogInfo();
            log.Ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            if (Request.Headers.ContainsKey(HeaderNames.UserAgent))
            {
                log.UserAgent = Request.Headers[HeaderNames.UserAgent].ToString();
                log.Platform = "";
                log.PlatformVersion = "";
                log.Browser = "";
                log.BrowserVersion = "";
            }

            if (Request.Headers.ContainsKey(ConstantHelper.HEADER_X_COUNTRY))
            {
                log.Country = Request.Headers[ConstantHelper.HEADER_X_COUNTRY].ToString();
            }

            if (Request.Headers.ContainsKey(ConstantHelper.HEADER_X_CITY))
            {
                log.City = Request.Headers[ConstantHelper.HEADER_X_CITY].ToString();
            }

            return log;
        }

        protected string GetRequestBodyString()
        {
            string bodyStr;
            using (var reader = new StreamReader(Request.Body, Encoding.UTF8, true, 1024, true))
            {
                bodyStr = reader.ReadToEnd();
            }

            return bodyStr;
        }

        protected void SetPaging(int skip, int take, BasePagedRequest request)
        {
            request.PagingInfo.Skip = skip;
            request.PagingInfo.Take = take;
        }

        protected void SetPaging(int skip, int take, BaseAuthenticatedPagedRequest request)
        {
            request.PagingInfo.Skip = skip;
            request.PagingInfo.Take = take;
        }

        public string GetDateTimeAsString(DateTime dateTime, string format = "yyyy/MM/dd HH:mm:ss")
        {
            return dateTime.ToString(format);
        }

        public string GetDateTimeAsString(DateTime? dateTime, string format = "yyyy/MM/dd HH:mm:ss")
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value.ToString(format);
            }

            return "-";
        }
    }
}