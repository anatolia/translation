using System;
using Microsoft.AspNetCore.Http;
using Translation.Client.Web.Models;
using Translation.Client.Web.Models.Admin;
using Translation.Client.Web.Models.Data;
using Translation.Client.Web.Models.InputModels;
using Translation.Client.Web.Models.Integration;
using Translation.Client.Web.Models.Label;
using Translation.Client.Web.Models.LabelTranslation;
using Translation.Client.Web.Models.Language;
using Translation.Client.Web.Models.Organization;
using Translation.Client.Web.Models.Project;
using Translation.Client.Web.Models.Token;
using Translation.Client.Web.Models.User;
using Translation.Common.Models.Shared;
using static Translation.Tests.TestHelpers.FakeConstantTestHelper;
using static Translation.Tests.TestHelpers.FakeEntityTestHelper;

namespace Translation.Tests.TestHelpers
{
    public class FakeModelTestHelper
    {
        public static ProjectCreateModel GetOrganizationOneProjectOneCreateModel()
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = OrganizationOneUid;
            model.Name = OrganizationOneProjectOneName;
            model.Url = HttpsUrl;
            model.Slug = OrganizationOneProjectOneSlug;
            model.LanguageUid = UidOne;

            return model;
        }

        public static ChangePasswordModel GetChangePasswordModel()
        {
            var model = new ChangePasswordModel();

            return model;
        }

        public static InviteAcceptModel GetInviteAcceptModel()
        {
            var model = new InviteAcceptModel();

            return model;
        }

        public static InviteAcceptModel GetInviteAcceptModel(Guid token, string email, string firstName,
                                                             string lastName, string password, string reEnterPassword,
                                                             Guid languageUid, string languageName)
        {
            var model = new InviteAcceptModel();
            model.Token = token;
            model.Email = email;
            model.FirstName = firstName;
            model.LastName = lastName;
            model.Password = password;
            model.ReEnterPassword = reEnterPassword;
            model.LanguageUid = languageUid;
            model.LanguageName = languageName;

            return model;
        }

        public static DemandPasswordResetModel GetDemandPasswordResetModel()
        {
            var model = new DemandPasswordResetModel();

            return model;
        }

        public static DemandPasswordResetModel GetDemandPasswordResetModel(string email)
        {
            var model = new DemandPasswordResetModel();
            model.Email = email;

            return model;
        }

        public static ChangePasswordModel GetChangePasswordModel(Guid userUid, string oldPassword, string newPassword,
                                                                 string reEnterNewPassword)
        {
            var model = new ChangePasswordModel();
            model.UserUid = userUid;
            model.OldPassword = oldPassword;
            model.NewPassword = newPassword;
            model.ReEnterNewPassword = reEnterNewPassword;

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

        public static AdminInviteModel GetAdminInviteModel()
        {
            var model = new AdminInviteModel();
            model.OrganizationUid = UidOne;
            model.Email = EmailOne;
            model.FirstName = StringOne;
            model.LastName = StringTwo;

            return model;
        }

        public static DataAddLabelModel GetMDataAddLabelModel()
        {
            var model = new DataAddLabelModel();
            model.Token = UidOne;
            model.ProjectUid = UidTwo;
            model.LabelKey = StringOne;
            model.LanguageIsoCode2s = IsoCode2One;

            return model;
        }

        public static AdminInviteModel GetAdminInviteModel(Guid organizationUid, string email,
                                                           string firstName, string lastName)
        {
            var model = new AdminInviteModel();
            model.OrganizationUid = organizationUid;
            model.Email = email;
            model.FirstName = firstName;
            model.LastName = lastName;

            return model;
        }

        public static AdminAcceptInviteModel GetAdminAcceptInviteModel(Guid organizationUid, Guid token, string email,
                                                                       string firstName, string lastName, string password,
                                                                       string reEnterPassword)
        {
            var model = new AdminAcceptInviteModel();
            model.OrganizationUid = organizationUid;
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
            model.LanguageUid = UidOne;

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
            model.Language = UidOne;
            model.LanguageName = StringOne;

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
            model.LanguageName = StringTwo;
            model.LanguageIconUrl = HttpsUrl;

            return model;
        }

        public static LanguageCreateModel GetLanguageOneCreateModel()
        {
            var model = new LanguageCreateModel();
            model.Name = "Language One";
            model.OriginalName = "Language One Original Name";
            model.IsoCode2 = IsoCode2One;
            model.IsoCode3 = IsoCode3One;
            model.Icon = GetIcon();

            return model;
        }

        public static LanguageEditModel GetLanguageOneEditModel()
        {
            var model = new LanguageEditModel();
            model.Name = "Language One";
            model.LanguageUid = UidOne;
            model.OriginalName = "Language One Original Name";

            model.IsoCode2 = IsoCode2One;
            model.IsoCode3 = IsoCode3One;
            model.Icon = GetIcon();

            return model;
        }

        public static JournalListModel GetJournalListModel()
        {
            var model = new JournalListModel();

            return model;
        }

        public static AllUserListModel GetAllUserListModel()
        {
            var model = new AllUserListModel();

            return model;
        }

        public static HomeModel GetHomeModel()
        {
            var model = new HomeModel();

            return model;
        }

        public static AccessDeniedModel GetAccessDeniedModel()
        {
            var model = new AccessDeniedModel();

            return model;
        }

        public static InviteAcceptDoneModel GetInviteAcceptDoneModel()
        {
            var model = new InviteAcceptDoneModel();

            return model;
        }

        public static ChangePasswordDoneModel GetChangePasswordDoneModel()
        {
            var model = new ChangePasswordDoneModel();

            return model;
        }

        public static DemandPasswordResetDoneModel GetDemandPasswordResetDoneModel()
        {
            var model = new DemandPasswordResetDoneModel();

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

        public static OrganizationUserLoginLogListModel GetOrganizationUserLoginLogListModel()
        {
            var model = new OrganizationUserLoginLogListModel();

            return model;
        }

        public static CreateBulkLabelDoneModel GetCreateBulkLabelDoneModel()
        {
            var model = new CreateBulkLabelDoneModel();

            return model;
        }

        public static LanguageCreateModel GetLanguageCreateModel()
        {
            var model = new LanguageCreateModel();

            return model;
        }

        public static LanguageCreateModel GetLanguageCreateModel(string name, string originalName, string isoCode2,
                                                                 string isoCode3, IFormFile icon)
        {
            var model = new LanguageCreateModel();
            model.Name = name;
            model.OriginalName = originalName;
            model.IsoCode2 = isoCode2;
            model.IsoCode3 = isoCode3;
            model.Icon = icon;

            return model;
        }

        public static LanguageEditModel GetLanguageEditModel()
        {
            var model = new LanguageEditModel();

            return model;
        }

        public static LanguageCreateModel GetLanguageEditModel(string name, string originalName, string isoCode2,
            string isoCode3, IFormFile icon)
        {
            var model = new LanguageCreateModel();
            model.Name = name;
            model.OriginalName = originalName;
            model.IsoCode2 = isoCode2;
            model.IsoCode3 = isoCode3;
            model.Icon = icon;

            return model;
        }

        public static LanguageListModel GetLanguageListModel()
        {
            var model = new LanguageListModel();

            return model;
        }

        public static LanguageRevisionReadListModel GetLanguageRevisionReadListModel()
        {
            var model = new LanguageRevisionReadListModel();

            return model;
        }

        public static LanguageDetailModel GetLanguageDetailModel()
        {
            var model = new LanguageDetailModel();

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
            model.LanguageUid = UidOne;
            model.LanguageName = StringThree;
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

        public static OrganizationDetailModel GetOrganizationDetailModel()
        {
            var model = new OrganizationDetailModel();
            model.OrganizationUid = UidOne;
            model.Name = StringOne;
            model.Description = StringOne;

            return model;
        }

        public static OrganizationEditModel GetOrganizationEditModel()
        {
            var model = new OrganizationEditModel();
            model.OrganizationUid = UidOne;
            model.Name = StringOne;
            model.Description = StringOne;

            return model;
        }

        public static OrganizationJournalListModel GetOrganizationJournalListModel()
        {
            var model = new OrganizationJournalListModel();
            model.OrganizationUid = UidOne;

            return model;
        }

        public static OrganizationListModel GetOrganizationListModel()
        {
            var model = new OrganizationListModel();

            return model;
        }

        public static OrganizationPendingTranslationReadListModel GetOrganizationPendingTranslationReadListModel()
        {
            var model = new OrganizationPendingTranslationReadListModel();
            model.OrganizationName = StringOne;
            model.OrganizationUid = UidOne;

            return model;
        }

        public static OrganizationRevisionReadListModel GetOrganizationRevisionReadListModel()
        {
            var model = new OrganizationRevisionReadListModel();
            model.OrganizationName = StringOne;
            model.OrganizationUid = UidOne;

            return model;
        }

        public static OrganizationTokenRequestLogListModel GetOrganizationTokenRequestLogListModel()
        {
            var model = new OrganizationTokenRequestLogListModel();
            model.OrganizationUid = UidOne;

            return model;
        }

        public static OrganizationEditModel GetOrganizationEditModel(Guid organizationUid, string name)
        {
            var model = new OrganizationEditModel();
            model.OrganizationUid = organizationUid;
            model.Name = name;

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
            model.LanguageUid = UidOne;
            model.LanguageName = StringThree;

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

        public static ActiveTokensModel GetActiveTokensModel()
        {
            var model = new ActiveTokensModel();

            return model;
        }

        public static ActiveTokensDataModel GetActiveTokensDataModel()
        {
            var model = new ActiveTokensDataModel();

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
            model.LanguageUid = UidStringOne;

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

        public static CheckboxInputModel GetCheckboxInputModel(string name, string labelKey, bool isRequired,
                                                                bool isReadOnly, bool value)
        {
            var model = new CheckboxInputModel(name, labelKey, isRequired, isReadOnly, value);
            return model;
        }

        public static InputModel GetInputModel(string name, string labelKey, bool isRequired, string value)
        {
            var model = new InputModel(name, labelKey, isRequired, value);
            return model;
        }

        public static InputModel GetInputModel(string name, string labelKey, bool isRequired)
        {
            var model = new InputModel(name, labelKey, isRequired);
            return model;
        }

        public static InputModel GetInputModel(string name, string labelKey)
        {
            var model = new InputModel(name, labelKey);
            return model;
        }

        public static DateInputModel GetDateInputModel(string name, string labelKey, bool isRequired)
        {
            var model = new DateInputModel(name, labelKey, isRequired);
            model.Value = DateTimeOne;

            return model;
        }

        public static FileInputModel GetFileInputModel(string name, string labelKey, bool isRequired, bool isMultiple)
        {
            var model = new FileInputModel(name, labelKey, isRequired, isMultiple);

            return model;
        }

        public static SelectInputModel GetSelectInputModel(string name, string labelKey, string dataUrl, bool required, string addNewUrl)
        {
            var model = new SelectInputModel(name, labelKey, dataUrl, required, addNewUrl);

            return model;
        }

        public static NumberInputModel GetNumberInputModel(string name, string labelKey, bool isRequired, int value)
        {
            var model = new NumberInputModel(name, labelKey, isRequired, value);

            return model;
        }

        public static PasswordInputModel GetPasswordInputModel(string name, string labelKey, bool isRequired, string value)
        {
            var model = new PasswordInputModel(name, labelKey, isRequired, value);

            return model;
        }

        public static TextareaInputModel GetTextareaInputModel(string name, string labelKey, bool isRequired, string value)
        {
            var model = new TextareaInputModel(name, labelKey, isRequired, value);

            return model;
        }

        public static UrlInputModel GetUrlInputModel(string name, string labelKey, bool isRequired, string value)
        {
            var model = new UrlInputModel(name, labelKey, isRequired, value);

            return model;
        }

        public static ShortInputModel GetShortInputModel(string name, string labelKey, bool isRequired, string value)
        {
            var model = new ShortInputModel(name, labelKey, isRequired, value);

            return model;
        }

        public static ReadOnlyInputModel GetReadOnlyInputModel(string labelKey, string value)
        {
            var model = new ReadOnlyInputModel(labelKey, value);

            return model;
        }

        public static HiddenInputModel GetHiddenInputModel(string name, string value)
        {
            var model = new HiddenInputModel(name, value);

            return model;
        }

        public static LongInputModel GetLongInputModel(string name, string labelKey, bool isRequired)
        {
            var model = new LongInputModel(name, labelKey, isRequired);

            return model;
        }

        public static EmailInputModel GetEmailInputModel(string name, string labelKey, bool isRequired)
        {
            var model = new EmailInputModel(name, labelKey, isRequired);

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
            model.Project = OrganizationOneProjectOneUid;
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
            model.Project = cloningLabelUid;
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

        public static CreateBulkLabelModel GetCreateBulkLabelModel(int csvContentLength)
        {
            var model = new CreateBulkLabelModel();

            model.OrganizationUid = UidOne;
            model.ProjectUid = UidTwo;
            model.ProjectName = StringOne;
            model.BulkLabelData = GenerateCsvContent(csvContentLength);

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

        public static LabelUploadFromCSVModel GetLabelUploadFromCSVModel(int csvFileLength)
        {
            var model = new LabelUploadFromCSVModel();

            model.ProjectName = StringOne;
            model.OrganizationUid = UidOne;
            model.ProjectUid = UidOne;
            model.CSVFile = GetCsvFile(csvFileLength);

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

        public static LabelTranslationCreateModel GetLabelTranslationCreateModel(Guid organizationUid, Guid projectUid, string projectName,
                                                                                 Guid labelUid, string labelKey, Guid languageUid,
                                                                                 string LabelTranslation)
        {
            var model = new LabelTranslationCreateModel();

            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.ProjectName = projectName;
            model.LabelUid = labelUid;
            model.LabelKey = labelKey;
            model.LabelUid = labelUid;
            model.LabelTranslation = LabelTranslation;
            model.LanguageUid = languageUid;

            return model;
        }

        public static LabelTranslationDetailModel GetLabelTranslationDetailModel()
        {
            var model = new LabelTranslationDetailModel();

            model.OrganizationUid = UidOne;
            model.LabelTranslationUid = UidTwo;
            model.LabelKey = StringOne;
            model.LanguageName = StringTwo;
            model.LanguageIconUrl = HttpsUrl;
            model.Translation = StringThree;

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

        public static LabelTranslationEditModel GetLabelTranslationEditModel(Guid organizationUid, Guid labelTranslationUid, string labelKey,
                                                                             string languageName, string languageIconUrl, string translation)
        {
            var model = new LabelTranslationEditModel();

            model.OrganizationUid = organizationUid;
            model.LabelTranslationUid = labelTranslationUid;
            model.LabelKey = labelKey;
            model.LanguageName = languageName;
            model.LanguageIconUrl = languageIconUrl;
            model.Translation = translation;

            return model;
        }

        public static LabelTranslationListModel GetLabelTranslationListModel()
        {
            var model = new LabelTranslationListModel();

            return model;
        }

        public static LabelTranslationRevisionReadListModel GetLabelTranslationRevisionReadListModel()
        {
            var model = new LabelTranslationRevisionReadListModel();

            return model;
        }

        public static TranslationUploadFromCSVDoneModel GetTranslationUploadFromCSVDoneModel()
        {
            var model = new TranslationUploadFromCSVDoneModel();

            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel GetUploadLabelTranslationFromCSVFileModel(int csvFileLength)
        {
            var model = new UploadLabelTranslationFromCSVFileModel();

            model.OrganizationUid = UidOne;
            model.LabelUid = UidOne;
            model.LabelKey = StringOne;
            model.CSVFile = GetCsvFile(csvFileLength);

            return model;
        }

        public static UploadLabelTranslationFromCSVFileModel GetUploadLabelTranslationFromCSVFileModel(Guid organizationUid, Guid labelUid, string labelKey,
                                                                                                       IFormFile csvFile)
        {
            var model = new UploadLabelTranslationFromCSVFileModel();

            model.OrganizationUid = organizationUid;
            model.LabelUid = labelUid;
            model.LabelKey = labelKey;
            model.CSVFile = csvFile;

            return model;
        }

        public static ProjectCloneModel GetProjectCloneModel(Guid organizationUid, Guid cloningProjectUid, string name,
                                                             string slug, string url, Guid languageUid,
                                                             string description = StringOne)
        {
            var model = new ProjectCloneModel();
            model.OrganizationUid = organizationUid;
            model.CloningProjectUid = cloningProjectUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.Language = languageUid;
            model.Description = description;

            return model;
        }

        public static ProjectCreateModel GetProjectCreateModel(Guid organizationUid, string name, string slug,
                                                               string url, Guid languageUid, string description = StringOne)
        {
            var model = new ProjectCreateModel();
            model.OrganizationUid = organizationUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.LanguageUid = languageUid;
            model.Description = description;

            return model;
        }

        public static ProjectEditModel GetProjectEditModel(Guid organizationUid, Guid projectUid, string name,
                                                           string slug, string url, Guid languageUid,
                                                           string description = StringOne)
        {
            var model = new ProjectEditModel();
            model.OrganizationUid = organizationUid;
            model.ProjectUid = projectUid;
            model.Name = name;
            model.Slug = slug;
            model.Url = url;
            model.LanguageUid = languageUid;
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