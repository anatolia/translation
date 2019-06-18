using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Translation.Client.Web.Models.Base;
using Translation.Common.Models.DataTransferObjects;

namespace Translation.Client.Web.Helpers
{
    public class DataResultHelper
    {
        public static DataResult GetUserLoginLogDataResult(List<UserLoginLogDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("user", "ip", "country", "city", "browser", "browser_version", "platform", "platform_version", "created_at");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.Uid}", item.UserName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Ip}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Country}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.City}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Browser}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.BrowserVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Platform}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.PlatformVersion}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

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
                stringBuilder.Append($"{result.PrepareButton("degrade_to_user", "handleDegradeToUser(this, \"/Admin/DegradeToUser/\")", "btn-secondary", "are_you_sure_you_want_to_degrade_to_user_title", "are_you_sure_you_want_to_degrade_to_user_content")}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetOrganizationListDataResult(List<OrganizationDto> items)
        {
            var result = new DataResult();
            result.AddHeaders("organization_name", "user_count", "is_active", "");

            for (var i = 0; i < items.Count; i++)
            {
                var item = items[i];
                var stringBuilder = new StringBuilder();
                stringBuilder.Append($"{item.Uid}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Organization/Detail/{item.Uid}", item.Name)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.UserCount}{DataResult.SEPARATOR}");
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
                stringBuilder.Append($"{result.PrepareChangeActivationButton("/Admin/ChangeActivation/")}");
                stringBuilder.Append($"{result.PrepareButton("upgrade_to_admin", "handleUpgradeToAdmin(this, \"/Admin/UserUpgradeToAdmin/\")", "btn-secondary", "are_you_sure_you_want_to_upgrade_to_admin_title", "are_you_sure_you_want_to_upgrade_to_admin_content")}{DataResult.SEPARATOR}");

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
                stringBuilder.Append($"{result.PrepareLink($"/User/Detail/{item.UserUid}", item.UserName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{result.PrepareLink($"/Integration/Detail/{item.IntegrationUid}", item.IntegrationName)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.Message}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetTokenRequestLogListData(List<TokenRequestLogDto> items)
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
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }

        public static DataResult GetSendEmailLogListData(List<SendEmailLogDto> items)
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
                stringBuilder.Append($"{item.CreatedAt.ToString("yyyy-MM-dd HH:mm", CultureInfo.CurrentUICulture)}{DataResult.SEPARATOR}");
                stringBuilder.Append($"{item.IsOpened}{DataResult.SEPARATOR}");

                result.Data.Add(stringBuilder.ToString());
            }

            return result;
        }
    }
}