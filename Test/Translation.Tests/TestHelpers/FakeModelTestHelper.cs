
﻿using System;
 using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using Moq;
 using Translation.Client.Web.Models.Admin;
 using Translation.Client.Web.Models.Integration;
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Client.Web.Models.Language;
using Translation.Client.Web.Models.Organization;
using Translation.Client.Web.Models.Project;
using Translation.Client.Web.Models.User;
using Translation.Common.Models.Shared;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeModelTestHelper
    {
        public static PagingInfo GetFakePagingInfo()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = One;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }

        public static PagingInfo GetFakePagingInfoForSelectMany()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = One;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }

        public static PagingInfo GetFakePagingInfoForSelectAfter()
        {
            var pagingInfo = new PagingInfo();
            pagingInfo.Skip = Zero;
            pagingInfo.Take = Two;
            pagingInfo.LastUid = UidOne;
            pagingInfo.IsAscending = BooleanTrue;
            pagingInfo.TotalItemCount = Ten;
            pagingInfo.Type = PagingInfo.PAGE_NUMBERS;

            return pagingInfo;
        }

        public static ProjectCreateModel GetOrganizationOneProjectOneCreateModel()
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.Url = HttpsUrl;
            model.Slug = OrganizationOneProjectOneSlug;

            return model;
        }

        public static AdminAcceptInviteModel GetAdminAcceptInviteModel()
        {
            var model = new AdminAcceptInviteModel();
            model.OrganizationUid = UidOne;
            model.Token = UidTwo;
            model.Email = EmailOne;
            model.FirstName = StringOne;
            model.LastName = StringTwo;
            model.Password = PasswordOne;
            model.ReEnterPassword = PasswordOne;

            return model;
        }

        public static AdminAcceptInviteModel GetAdminAcceptInviteModel(Guid organizaitonUid, Guid token, string email,
                                                                       string firstName, string lastName, string password,
                                                                       string reEnterPassword)
        {
            var model = new AdminAcceptInviteModel();
            model.OrganizationUid = organizaitonUid;
            model.Token = token;
            model.Email = email;
            model.FirstName = firstName;
            model.LastName = lastName;
            model.Password = password;
            model.ReEnterPassword = reEnterPassword;

            return model;
        }

        public static ProjectEditModel GetOrganizationOneProjectOneEditModel()
        {
            var model = new ProjectEditModel();
            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.Url = HttpsUrl;
            model.Slug = OrganizationOneProjectOneSlug;

            return model;
        }

        public static ProjectCloneModel GetOrganizationOneProjectOneCloneModel()
        {
            var model = new ProjectCloneModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.CloningProjectUid = OrganizationOneProjectOneUid;
            model.LabelCount = One;
            model.LabelTranslationCount = Two;
            model.Url = HttpsUrl;
            model.Slug = StringOne;

            return model;
        }

        public static ProjectDetailModel GetOrganizationOneProjectOneDetailModel()
        {
            var model = new ProjectDetailModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.LabelCount = One;
            model.Url = HttpsUrl;
            model.Slug = StringOne;

            return model;
        }

        public static LanguageCreateModel GetLanguageOneCreateModel()
        {
            var model = new LanguageCreateModel();
            model.Name = "Language One";
            model.OriginalName = "Language One Original Name";
            model.IsoCode2 = IsoCode2One;
            model.IsoCode3 = IsoCode3One;
            model.Icon = GetLanguageOneCreateIcon();

           return model;
        }

        public static LanguageEditModel GetLanguageOneEditModel()
        {
            var model = new LanguageEditModel();
            model.Name = "Language One";
            model.OriginalName = "Language One Original Name";
            model.IsoCode2 = IsoCode2One;
            model.IsoCode3 = IsoCode3One;
            model.Icon = GetLanguageOneCreateIcon();

            return model;
        }

        public static JournalListModel GetJournalListModel()
        {
            var model = new JournalListModel();

            return model;
        }

        public static SendEmailLogListModel GetSendEmailLogListModel()
        {
            var model = new SendEmailLogListModel();

            return model;
        }

        public static TokenRequestLogListModel GetTokenRequestLogListModel()
        {
            var model = new TokenRequestLogListModel();

            return model;
        }

        public static UserLoginLogListModel GetUserLoginLogListModel()
        {
            var model = new UserLoginLogListModel();

            return model;
        }

        public static CreateBulkLabelDoneModel GetCreateBulkLabelDoneModel()
        {
            var model = new CreateBulkLabelDoneModel();

            return model;
        }

        public static SignUpModel GetOrganizationOneUserOneSignUpModel()
        {
            var model = new SignUpModel();
            model.OrganizationName = OrganizationOneName;
            model.Email = OrganizationOneUserOneEmail;
            model.FirstName = StringOne;
            model.LastName = StringTwo;
            model.Password = PasswordOne;
            model.IsTermsAccepted = BooleanTrue;

            return model;
        }

        public static LogOnModel GetOrganizationOneUserOneLogOnModel()
        {
            var model = new LogOnModel();
            model.Email = OrganizationOneUserOneEmail;
            model.Password = PasswordOne;

            return model;
        }

        public static DemandPasswordResetModel GetOrganizationOneUserOneDemandPasswordResetModel()
        {
            var model = new DemandPasswordResetModel();
            model.Email = OrganizationOneUserOneEmail;

            return model;
        }

        public static ResetPasswordModel GetOrganizationOneUserOneResetPasswordModel()
        {
            var model = new ResetPasswordModel();
            model.Token = UidOne;
            model.Email = OrganizationOneUserOneEmail;
            model.Password = PasswordOne;
            model.ReEnterPassword = PasswordOne;

            return model;
        }

        public static ChangePasswordModel GetOrganizationOneUserOneChangePasswordModel()
        {
            var model = new ChangePasswordModel();
            model.NewPassword = PasswordTwo;
            model.ReEnterNewPassword = PasswordTwo;
            model.OldPassword = PasswordOne;
            model.UserUid = OrganizationOneUserOneUid;

            return model;
        }

        public static InviteModel GetOrganizationOneUserOneInviteModel()
        {
            var model = new InviteModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Email = OrganizationOneUserOneEmail;
            model.FirstName = StringOne;
            model.LastName = StringTwo;

            return model;
        }

        public static InviteAcceptModel GetOrganizationOneUserOneInviteAcceptModel()
        {
            var model = new InviteAcceptModel();
            model.Token = UidOne;
            model.Email = OrganizationOneUserOneEmail;
            model.FirstName = StringOne;
            model.LastName = StringTwo;
            model.Password = PasswordTwo;
            model.ReEnterPassword = PasswordTwo;

            return model;
        }

        public static UserEditModel GetOrganizationOneUserOneUserEditModel()
        {
            var model = new UserEditModel();
            model.UserUid = OrganizationOneUserOneUid;
            model.FirstName = StringOne;
            model.LastName = StringTwo;
            model.LanguageUid = UidOne;

            return model;
        }

        public static OrganizationEditModel GetOrganizationOneOrganizationEditModel()
        {
            var model = new OrganizationEditModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneName;

            return model;
        }

        public static IntegrationCreateModel GetIntegrationCreateModel()
        {
            var model = new IntegrationCreateModel();
            model.OrganizationUid = UidOne;
            model.Name = StringOne;
            model.Description = StringOne;
            return model;
        }

        public static IntegrationActiveTokensModel GetIntegrationActiveTokensModel()
        {
            var model = new IntegrationActiveTokensModel();
            model.IntegrationUid = UidOne;
            model.IntegrationName = StringOne;

            return model;
        }

        public static AdminAcceptInviteDoneModel GetAdminAcceptInviteDoneModel()
        {
            var model = new AdminAcceptInviteDoneModel();

            return model;
        }

        public static AdminDashboardBaseModel GetAdminDashboardBaseModel()
        {
            var model = new AdminDashboardBaseModel();

            return model;
        }

        public static AdminInviteDoneModel GetAdminInviteDoneModel()
        {
            var model = new AdminInviteDoneModel();

            return model;
        }

        public static AdminListBaseModel GetAdminListBaseModel()
        {
            var model = new AdminListBaseModel();

            return model;
        }

        public static IntegrationActiveTokensModel GetIntegrationActiveTokensModel(Guid integrationUid, string integrationName)
        {
            var model = new IntegrationActiveTokensModel();
            model.IntegrationUid = integrationUid;
            model.IntegrationName = integrationName;

            return model;
        }

        public static IntegrationClientActiveTokensModel GetIntegrationClientActiveTokensModel(Guid integrationUid, string integrationName)
        {
            var model = new IntegrationClientActiveTokensModel();
            model.IntegrationUid = integrationUid;
            model.IntegrationName = integrationName;

            return model;
        }

        public static IntegrationClientTokenRequestLogsModel GetIntegrationClientTokenRequestLogsModel(Guid integrationClientUid)
        {
            var model = new IntegrationClientTokenRequestLogsModel();
            model.IntegrationClientUid = integrationClientUid;

            return model;
        }

        public static IntegrationRevisionReadListModel GetIntegrationRevisionReadListModel(Guid integrationUid, string integrationName)
        {
            var model = new IntegrationRevisionReadListModel();
            model.IntegrationUid = integrationUid;
            model.IntegrationName = integrationName;

            return model;
        }

        public static IntegrationCreateModel GetIntegrationCreateModel(Guid organizationUid, string name, string description = StringOne)
        {
            var model = new IntegrationCreateModel();
            model.OrganizationUid = organizationUid;
            model.Name = name;
            model.Description = description;
            return model;
        }

        public static IntegrationEditModel GetIntegrationEditModel()
        {
            var model = new IntegrationEditModel();
            model.IntegrationUid = UidOne;
            model.Name = StringOne;
            model.Description = StringOne;

            return model;
        }

        public static IntegrationEditModel GetIntegrationEditModel(Guid integrationUid, string name, string description = StringOne)
        {
            var model = new IntegrationEditModel();
            model.IntegrationUid = integrationUid;
            model.Name = name;
            model.Description = description;

            return model;
        }

        public static IntegrationDetailModel GetIntegrationDetailModel()
        {
            var model = new IntegrationDetailModel();
            model.OrganizationUid = UidOne;
            model.OrganizationName = StringOne;
            model.IntegrationUid = UidTwo;
            model.Name = StringTwo;
            model.Description = StringThree;

            return model;
        }

        public static IntegrationDetailModel GetIntegrationDetailModel(Guid organizationUid, string organizationName, Guid integrationUid,
                                                                       string name, string description = StringOne)
        {
            var model = new IntegrationDetailModel();
            model.OrganizationUid = organizationUid;
            model.OrganizationName = organizationName;
            model.IntegrationUid = integrationUid;
            model.Name = name;
            model.Description = description;

            return model;
        }

        public static LabelCreateModel GetLabelCreateModel()
        {
            var model = new LabelCreateModel();
            model.OrganizationUid = UidOne;
            model.ProjectUid = UidOne;
            model.Key = StringOne;
            model.ProjectName = StringOne;
            model.Description = StringOne;


            return model;
        }

        public static LabelRevisionReadListModel GetLabelRevisionReadListModel()
        {
            var model = new LabelRevisionReadListModel();

            return model;
        }

        public static LabelDetailModel GetLabelDetailModel()
        {
            var model = new LabelDetailModel();
            model.OrganizationUid = UidOne;
            model.ProjectUid = UidOne;
            model.Key = StringOne;
            model.ProjectName = StringOne;
            model.Description = StringOne;

            return model;
        }

        public static LabelSearchListModel GetLabelSearchListModel()
        {
            var model = new LabelSearchListModel();

            return model;
        }

        public static LabelUploadFromCSVDoneModel GetLabelUploadFromCSVDoneModel()
        {
            var model = new LabelUploadFromCSVDoneModel();

            return model;
        }

        public static LabelDetailModel GetLabelDetailModel(Guid organizationUid, string organizationName, Guid projectUid,
                                                           string projectName, string key)
        {
            var model = new LabelDetailModel();
            model.OrganizationUid = organizationUid;
            model.OrganizationName = organizationName;
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;
            model.Key = key;
            model.Description = StringOne;

            return model;
        }

        public static LabelCreateModel GetLabelCreateModel(Guid organizationUid, Guid projectUid, string projectName,
                                                           string key)
        {
            var model = new LabelCreateModel();

            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;
            model.Description = StringOne;
            model.Key = key;

            return model;
        }

        public static LabelEditModel GetLabelEditModel()
        {
            var model = new LabelEditModel();
            model.OrganizationUid = UidOne;
            model.LabelUid = UidOne;
            model.Key = StringOne;
            model.Description = StringOne;


            return model;
        }

        public static LabelEditModel GetLabelEditModel(Guid organizationUid, Guid labelUid, string key)
        {
            var model = new LabelEditModel();
            model.OrganizationUid = organizationUid;
            model.LabelUid = labelUid;
            model.Key = key;
            model.Description = StringOne;


            return model;
        }

        public static LabelCloneModel GetLabelCloneModel()
        {
            var model = new LabelCloneModel();

            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.Description = StringOne;
            model.CloningLabelKey = StringOne;
            model.CloningLabelDescription = StringOne;
            model.CloningLabelUid = UidOne;
            model.Key = StringOne;

            return model;
        }

        public static LabelCloneModel GetLabelCloneModel(Guid organizationUid, Guid cloningLabelUid, string cloningLabelKey,
                                                         string cloningLabelDescription, Guid projectUid, string key)
        {
            var model = new LabelCloneModel();

            model.OrganizationUid = organizationUid;
            model.ProjectUid = cloningLabelUid;
            model.Description = StringOne;
            model.CloningLabelKey = cloningLabelKey;
            model.CloningLabelDescription = StringOne;
            model.CloningLabelUid = cloningLabelUid;
            model.Key = key;

            return model;
        }

        public static CreateBulkLabelModel GetCreateBulkLabelModel()
        {
            var model = new CreateBulkLabelModel();

            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.ProjectName = StringOne;

            return model;
        }

        public static CreateBulkLabelModel GetCreateBulkLabelModel(Guid organizationUid, Guid projectUid, string projectName,
                                                                   string bulkLabelData)
        {
            var model = new CreateBulkLabelModel();

            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;
            model.BulkLabelData = bulkLabelData;

            return model;
        }

        public static CreateBulkLabelModel GetCreateBulkLabelModelThreeValue()
        {
            var model = new CreateBulkLabelModel();

            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.ProjectName = StringOne;
            model.BulkLabelData = "label_key,language_char_code_2,translation\n" +
                                  "label,tr,Türkçesi";

            return model;
        }

        public static CreateBulkLabelModel GetCreateBulkLabelModelNotThreeValue()
        {
            var model = new CreateBulkLabelModel();

            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.ProjectName = StringOne;
            model.BulkLabelData = "label_key,language_char_code_2,translation\n" +
                                  "label,tr";

            return model;
        }

        public static LabelUploadFromCSVModel GetLabelUploadFromCSVModel()
        {
            var model = new LabelUploadFromCSVModel();

            model.ProjectName = StringOne;
            model.OrganizationUid = UidOne;
            model.ProjectUid = UidOne;
            return model;
        }

        public static LabelUploadFromCSVModel GetLabelUploadFromCSVModel(Guid organizationUid, Guid projectUid, string projectName,
                                                                         IFormFile csvFile)
        {
            var model = new LabelUploadFromCSVModel();

            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;
            model.CSVFile = csvFile;
            return model;
        }

        public static LabelUploadFromCSVModel GetLabelUploadFromCSVModelThreeValue()
        {
            var model = GetLabelUploadFromCSVModel();
            model.CSVFile = GetUploadLabelCsvTemplateFileThreeValue();

            return model;
        }

        public static LabelUploadFromCSVModel GetLabelUploadFromCSVModelNotThreeValue()
        {
            var model = GetLabelUploadFromCSVModel();
            model.CSVFile = GetUploadLabelCsvTemplateFileNotThreeValue();
            return model;
        }

        public static LabelTranslationCreateModel GetLabelTranslationCreateModel()
        {
            var model = new LabelTranslationCreateModel();

            model.OrganizationUid = OrganizationOneUid;
            model.ProjectUid = OrganizationOneProjectOneUid;
            model.ProjectName = StringOne;
            model.LabelUid = UidOne;
            model.LabelKey = StringOne;
            model.LabelUid = UidOne;
            model.LabelTranslation = StringTwo;
            model.LanguageUid = UidOne;

            return model;
        }

        public static LabelTranslationEditModel GetLabelTranslationEditModel()
        {
            var model = new LabelTranslationEditModel();

            model.OrganizationUid = OrganizationOneUid;
            model.LabelTranslationUid = UidOne;
            model.Translation = UidStringOne;

            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel GetUploadLabelTranslationFromCSVFileModel()
        {
            var model = new UploadLabelTranslationFromCSVFileModel();

            model.OrganizationUid = UidOne;
            model.LabelUid = UidOne;
            model.LabelKey = StringOne;

            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel GetUploadLabelTranslationFromCSVFileModelTwoLength()
        {
            var model = GetUploadLabelTranslationFromCSVFileModel();
            model.CSVFile = GetUploadLabelCsvTemplateFileTwoLength();

            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel GetUploadLabelTranslationFromCSVFileModelNotTwoLength()
        {
            var model = GetUploadLabelTranslationFromCSVFileModel();
            model.CSVFile = GetUploadLabelCsvTemplateFileNotTwoLength();

            return model;
        }

        public static ProjectCloneModel GetProjectCloneModel(Guid organizationUid, Guid cloningProjectUid, string name,
                                                             string slug, string url, string description = StringOne)
        {
            var model = new ProjectCloneModel();
            model.OrganizationUid = organizationUid;
            model.CloningProjectUid = cloningProjectUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.Description = description;

            return model;
        }

        public static ProjectCreateModel GetProjectCreateModel(Guid organizationUid, string name, string slug,
                                                               string url, string description = StringOne)
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = organizationUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.Description = description;

            return model;
        }

        public static ProjectDetailModel GetProjectDetailModel(Guid organizationUid, string organizationName, Guid projectUid,
                                                               string name, string slug, string url, 
                                                               int labelCount, string description = StringOne)
        {
            var model = new ProjectDetailModel();
            model.OrganizationUid = organizationUid;
            model.OrganizationName = organizationName;
            model.ProjectUid = projectUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.Description = description;
            model.LabelCount = labelCount;
            model.IsActive = true;

            return model;
        }

        public static ProjectEditModel GetProjectEditModel(Guid organizationUid, Guid projectUid, string name,
                                                           string slug, string url, string description = StringOne)
        {
            var model = new ProjectEditModel();
            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.Description = description;

            return model;
        }

        public static ProjectPendingTranslationReadListModel GetProjectPendingTranslationReadListModel(Guid projectUid, string projectName)
        {
            var model = new ProjectPendingTranslationReadListModel();
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;

            return model;
        }

        public static ProjectRevisionReadListModel GetProjectRevisionReadListModel(Guid projectUid, string projectName)
        {
            var model = new ProjectRevisionReadListModel();
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;

            return model;
        }

        public static ClientLogInfo GetClientLogInfo()
        {
            var model = new ClientLogInfo();
            model.UserAgent = StringOne;
            model.Browser = StringOne;
            model.BrowserVersion = StringOne;
            model.City = StringOne;
            model.Country = StringOne;
            model.Ip = StringOne;
            model.Platform = StringOne;
            model.PlatformVersion = StringOne;

            return model;
        }

    }
}