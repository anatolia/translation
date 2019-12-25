using System.Collections.Generic;
using System.Globalization;
using System.Text;

using StandardUtils.Helpers;
using StandardUtils.Models.DataTransferObjects;

using Translation.Client.Web.Models.Base;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers.DataResultHelpers
{
    public static class DataResultHelper
    {
        public static DataResult GetUserLoginLogDataResult(List<UserLoginLogDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization", "user", "ip", "country", "city", "browser", "browser_version", "platform", "platform_version", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.UserName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Browser}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.BrowserVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Platform}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.PlatformVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetAdminListDataResult(List<UserDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("user_name", "is_active", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.FirstName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}");
                stringBuilder.Append($"{result.PrepareDegradeToUserButton("/Admin/DegradeToUser/")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetOrganizationListDataResult(List<OrganizationDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "user_count", "project_count", "label_count", "label_translation_count", "is_active", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.Uid}", item.Name, true)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.UserCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ProjectCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelTranslationCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/OrganizationChangeActivation/")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetUserListDataResult(List<UserDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "is_active", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");

                if (!item.IsAdmin)
                {
                    stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}");
                    stringBuilder.Append($"{result.PrepareUpgradeToAdminButton("/Admin/UserUpgradeToAdmin/")}{DataResult.SEPARATOR}");
                }
                else
                {
                    stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}{DataResult.SEPARATOR}");
                }

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetOrganizationUserListDataResult(List<UserDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("user_name", "email", "invited_at", "invitation_accepted_at", "last_logged_in_at", "is_active", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Email}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.InvitedAt?.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.InvitationAcceptedAt?.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LastLoggedInAt?.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetJournalListDataResult(List<JournalDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "integration_name", "message", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                if (item.UserUid.IsNotEmptyGuid())
                {
                    stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.UserUid}", item.UserName)}{DataResult.SEPARATOR}");
                }
                else
                {
                    stringBuilder.Append($"-{DataResult.SEPARATOR}");
                }

                if (item.IntegrationUid.IsNotEmptyGuid())
                {
                    stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                }
                else
                {
                    stringBuilder.Append($"-{DataResult.SEPARATOR}");
                }

                stringBuilder.Append($"{item.Message}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetTokenRequestLogListDataResult(List<TokenRequestLogDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "user_name", "ip", "country", "city", "http_method", "response_code", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.HttpMethod}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ResponseCode}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetOrganizationTokenRequestLogListDataResult(List<TokenRequestLogDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("integration", "integration_client", "ip", "country", "city", "http_method", "response_code", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IntegrationClientUid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.HttpMethod}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ResponseCode}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetSendEmailLogListDataResult(List<SendEmailLogDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "mail_uid", "send_to", "subject", "send_at", "is_opened");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.OrganizationUid}", item.OrganizationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.MailUid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.EmailTo}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Subject}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}");
                stringBuilder.Append($"{item.IsOpened}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetIntegrationListDataResult(List<IntegrationDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("integration_name", "description", "is_active", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Description}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetActiveTokensDataResult(List<TokenDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("integration_client_uid", "access_token", "ip", "created_at", "expires_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IntegrationClientUid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.AccessToken}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IP}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ExpiresAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetProjectListDataResult(List<ProjectDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("project_name", "url", "label_count", "is_active", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Project/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink(item.Url)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetLabelRevisionsDataResult(List<RevisionDto<LabelDto>> items)
        {
            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "label_name", "is_active", "created_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var revisionItem = items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Label/Restore/", "/Label/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetLanguageRevisionsDataResult(List<RevisionDto<LanguageDto>> items)
        {
            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "language_name", "2_char_code", "3_char_code", "icon", "created_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var revisionItem = items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Name}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode2}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode3}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareImage($"{item.IconPath}", item.OriginalName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Language/Restore/", "/Language/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetLanguageListDataResult(List<LanguageDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("language_name", "2_char_code", "3_char_code", "icon", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink("/Language/Detail/" + item.Uid, item.Name, true)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode2}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsoCode3}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareImage($"{item.IconPath}", item.OriginalName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink("/Language/Edit/" + item.Uid, "edit", true)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetIntegrationRevisionsDataResult(List<RevisionDto<IntegrationDto>> items)
        {
            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "integration_name", "is_active", "created_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var revisionItem = items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Integration/Restore/", "/Integration/Detail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetLabelTranslationListDataResult(List<LabelTranslationDto> items, bool isSuperAdmin)
        {
            var result = new DataResult();
            result.AddHeaders("language", "translation", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareImage($"{item.LanguageIconUrl}", item.LanguageName)} {item.LanguageName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Translation}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/LabelTranslationEdit/{item.Uid}", "edit", true)}");

                if (isSuperAdmin)
                {
                    stringBuilder.Append($"{result.PrepareLink($"/Label/LabelTranslationRevisions/{item.Uid}", "revisions_link", true)}");
                    stringBuilder.Append($"{result.PrepareDeleteButton($"/Label/LabelTranslationDelete")}{DataResult.SEPARATOR}");
                }
                else
                {
                    stringBuilder.Append($"{result.PrepareLink($"/Label/LabelTranslationRevisions/{item.Uid}", "revisions_link", true)}{DataResult.SEPARATOR}");
                }

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetLabelTranslationRevisionsDataResult(List<RevisionDto<LabelTranslationDto>> items)
        {
            var result = new DataResult();
            result.AddHeaders("revision", "revisioned_by", "revisioned_at", "label_translation", "created_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var revisionItem = items[i];
                var item = revisionItem.Item;
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.Revision}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedByName}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{revisionItem.RevisionedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid}", item.Translation)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareRestoreButton("restore", "/Label/RestoreLabelTranslation/", "/Label/LabelTranslationDetail")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }


            return result;
        }

        public static DataResult GetLabelTranslationRevisionsDataResult(List<LabelDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("label_key", "label_translation_count", "description", "is_active");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Label/Detail/{item.Uid}", item.Key)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.LabelTranslationCount}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Description}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetClientActiveTokensDataResult(List<TokenDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("access_token", "ip", "created_at", "expires_at", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.AccessToken}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IP}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.ExpiresAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetTranslationProviderListDataResult(List<TranslationProviderDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("provider_name", "is_active", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/TranslationProvider/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsActive}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareChangeAllActivationButton("/Admin/TranslationProviderChangeActivation/")}");
                stringBuilder.Append($"{result.PrepareLink($"/TranslationProvider/Edit/{item.Uid}", "Edit", true)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetUserJournalListDataResult(List<JournalDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("message", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Message}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }
    }
}