using System;
using System.Collections.Generic;
using StandardUtils.Helpers;
using StandardUtils.Models.Shared;
using Translation.Common.Models.Requests.Label;
using Translation.Common.Models.Requests.Label.LabelTranslation;

namespace Translation.Common.Tests.TestHelpers
{
    public class FakeConstantTestHelper
    {
        public const long CurrentUserId = 4198161609L;

        public const long OrganizationOneId = 8168161649L;
        public static Guid OrganizationOneUid => new Guid("d59f0cf5-b347-4c1e-883a-af2fd4333459");
        public const string OrganizationOneName = "Organization One";

        public const long OrganizationOneProjectOneId = 1167161649L;
        public static Guid OrganizationOneProjectOneUid => new Guid("7f062647-4c7d-4ba5-b380-55869c74a944");
        public const string OrganizationOneProjectOneName = "Organization One Project One";
        public const string OrganizationOneProjectOneSlug = "organization_one_project_one";

        public const long OrganizationOneUserOneId = 2165161649L;
        public static Guid OrganizationOneUserOneUid => new Guid("5f235216-1da8-471c-ad50-7cb6c424f996");
        public const string OrganizationOneUserOneName = "Organization One User One";
        public const string OrganizationOneUserOneEmail = "organizationoneuserone@gmail.com";

        public const string OrganizationOneSuperAdminUserOneName = "Organization One Super Admin User One";
        public const string OrganizationOneSuperAdminUserOneEmail = "organizationonesuperadminuserone@gmail.com";

        public const long OrganizationOneIntegrationOneId = 1168161649L;
        public static Guid OrganizationOneIntegrationOneUid => new Guid("6a45c8ff-3814-4d2b-b71c-84c1ed1925e0");
        public const string OrganizationOneIntegrationOneName = "Organization One Integration One";

        public const long OrganizationOneIntegrationOneIntegrationClientOneId = 3168161601L;
        public static Guid OrganizationOneIntegrationOneIntegrationClientOneUid => new Guid("2068f8f7-b2d9-4c1f-83e9-267a59501d1e");
        public const string OrganizationOneIntegrationOneIntegrationClientOneName = "Organization One Integration One Integration Client One";

        public const long OrganizationOneProjectOneLabelOneId = 3164161549L;
        public static Guid OrganizationOneProjectOneLabelOneUid => new Guid("246a0211-c84f-4c92-b1df-7d606d1bdf02");
        public const string OrganizationOneProjectOneLabelOneName = "Organization One Project One Label One";
        public const string OrganizationOneProjectOneLabelOneKey = "organization_one_project_one_label_one";

        public const long OrganizationTwoProjectOneLabelOneId = 3164161549L;
        public static Guid OrganizationTwoProjectOneLabelOneUid => new Guid("3b278fca-4d8c-43a9-9060-510cc3278f0a");
        public const string OrganizationTwoProjectOneLabelOneName = "Organization Two Project One Label One";
        public const string OrganizationTwoProjectOneLabelOneKey = "organization_two_project_one_label_one";

        public const long OrganizationOneProjectOneLabelOneLabelTranslationOneId = 3164161549L;
        public static Guid OrganizationOneProjectOneLabelOneLabelTranslationOneUid => new Guid("26b4f65d-f4c9-40f2-a973-0529389d6f3e");
        public const string OrganizationOneProjectOneLabelOneLabelTranslationOneName = "Organization One Project One Label One LabelTranslation One";

        public const long OrganizationTwoId = 5168064649L;
        public static Guid OrganizationTwoUid => new Guid("bead3ad6-dc3c-44cd-9031-ad09438877f1");
        public const string OrganizationTwoName = "Organization Two";

        public const long OrganizationTwoProjectOneId = 2157161649L;
        public static Guid OrganizationTwoProjectOneUid => new Guid("c67d7fa4-5626-4777-a591-ded84dc76e50");
        public const string OrganizationTwoProjectOneName = "BlueSoft Project One";

        public const long OrganizationTwoUserOneId = 6065151649L;
        public static Guid OrganizationTwoUserOneUid => new Guid("6ca74999-22d7-4bcd-b3ee-dbd88a38598e");
        public const string OrganizationTwoUserOneName = "Organization Two User One";
        public const string OrganizationTwoUserOneEmail = "organizationtwouserone@gmail.com";

        public const long OrganizationTwoIntegrationOneId = 1168161649L;
        public static Guid OrganizationTwoIntegrationOneUid => new Guid("110dadde-a4bd-4f53-abcb-fb6a1c69a7b8");
        public const string OrganizationTwoIntegrationOneName = "Organization Two Integration One";

        public const long OrganizationTwoIntegrationOneIntegrationClientOneId = 3168161601L;
        public static Guid OrganizationTwoIntegrationOneIntegrationClientOneUid => new Guid("e4db895f-569c-4ab4-804b-553b4a4b7518");
        public const string OrganizationTwoIntegrationOneIntegrationClientOneName = "Organization Two Integration One Integration Client One";

        public static Guid EmptyUid => Guid.Empty;
        public static Guid UidOne => new Guid("3A2F7BA0-0D4C-4231-B9DF-A15460B60BD2");
        public static Guid UidTwo => new Guid("5bb6cdd7-b5d1-4cf0-98c9-9bc68fa940c3");
        public static Guid UidThree => new Guid("f5fb877e-6191-4ad0-9d0f-b6f28e182423");
        public static Guid UidFour => new Guid("8b1229b2-9c01-4d8e-802f-f0deab66e4b0");

        public static Guid[] GuidArrayZero = { };
        public static Guid[] GuidArrayOne = { UidOne };
        public static string GuidStringTwo = UidOne + "," + UidTwo;

        public static DateTime DateTimeOne => new DateTime(2019, 01, 01, 09, 00, 00);
        public static DateTime DateTimeTwo => new DateTime(2019, 01, 02, 18, 00, 00);
        public static DateTime DateTimeOneMinuteBefore => DateTime.UtcNow.AddMinutes(-1);
        public static DateTime DateTimeFiveMinutesBefore => DateTime.UtcNow.AddMinutes(-5);
        public static DateTime DateTimeTwoHoursBefore => DateTime.UtcNow.AddHours(-2);
        public static DateTime DateTimeOneDayBefore => DateTime.UtcNow.AddDays(-1);
        public static DateTime DateTimeTwoDaysBefore => DateTime.UtcNow.AddDays(-2);
        public static DateTime DateTimeOneWeekBefore => DateTime.UtcNow.AddDays(-7);

        public const string EmptySlug = "";
        public const string SlugOne = "slug_one";

        public const string EmptyString = "";
        public const string StringOne = "String One";
        public const string StringTwo = "String Two";
        public const string StringThree = "String Three";
        public const string StringFour = "String Four";
        public const string StringFive = "String Five";
        public const string StringSix = "String Six";
        public const string StringSeven = "String Seven";
        public const string StringEight = "String Eight";

        public const string LanguageOne = "Language One";
        public const string LanguageTwo = "Language Two";
        public const string LanguageOneOriginalName = "Language One Original Name";
        public const string LanguageTwoOriginalName = "Language Two Original Name";
            
        public const string StringSixtyFourOne = "bXk=";
        public const string StringSixtyFourTwo = "bXkx";

        public static string PasswordHashOne => new CryptoHelper().Hash(PasswordOne, StringSixtyFourOne);

        public const int Zero = 0;
        public const int One = 1;
        public const int Two = 2;
        public const int Three = 3;
        public const int Four = 4;
        public const int Nine = 9;
        public const int Ten = 10;
        public const int OneHundred = 100;

        public const long LongZero = 0L;
        public const long LongOne = 2147483649L;
        public const long LongTwo = 9107483649L;

        public const bool BooleanTrue = true;
        public const bool BooleanFalse = false;

        public const string InvalidPassword = "invalid-password";
        public const string PasswordOne = "PasswordOne+-2019*";
        public const string PasswordTwo = "PasswordTwo+-2019*+";
        public const string PasswordThree = "PasswordThree+-2019**";

        public const string EmptyUidString = "00000000-0000-0000-0000-000000000000";
        public const string UidStringOne = "ee4c5b8a-3498-4a7d-a9c8-74e86075853c";
        public const string UidStringTwo = "b64c5b8a-3498-4a7d-a9c8-74e86075853c";

        public const string InvalidEmail = "invalid-email";
        public const string EmailOne = "test@test.com";

        public const string InvalidUrl = "invalid_url";
        public const string HttpUrl = "http://turkiye.gov.tr";
        public const string HttpsUrl = "https://turkiye.gov.tr";
       

        public const string IpOne = "85.201.85.116";
        public const string IpTwo = "87.204.85.116";

        public const string IsoCode2One = "tr";
        public const string IsoCode3One = "tur";
        public const string IsoCode2Two = "en";
        public const string IsoCode3Two = "eng";

        public static string[] IsoCode2ArrayZero = { };
        public static string[] IsoCode2ArrayOne = { IsoCode2One };
        public static string[] IsoCode2ArrayTwo = { IsoCode2One, IsoCode2Two };


        public const string CaseOne = "Case One";
        public const string CaseTwo = "Case Two";
        public const string CaseThree = "Case Three";
        public const string CaseFour = "Case Four";

        public const string GoogleName = "google";
        public const string YandexName = "yandex";
        public const string GoogleKey = "{ type project_id private_key_id private_key client_email client_id auth_uri token_uri auth_provider_x509_cert_url client_x509_cert_url }";
        public const string YandexKey = "trns";


        public static LabelListInfo GetLabelListInfo()
        {
            var labelListInfo = new LabelListInfo();
            labelListInfo.LabelKey = StringOne;
            labelListInfo.LanguageIsoCode2 = IsoCode2One;
            labelListInfo.Translation = StringOne;

            return labelListInfo;
        }

        public static List<LabelListInfo> GetLabelListInfoList()
        {

            var labelListInfoList = new List<LabelListInfo>();
            labelListInfoList.Add(GetLabelListInfo());

            return labelListInfoList;
        }

        public static List<TranslationListInfo> GetTranslationListInfoList()
        {

            var labelListInfoList = new List<TranslationListInfo>();
            labelListInfoList.Add(GetTranslationListInfo());

            return labelListInfoList;
        }

        public static TranslationListInfo GetTranslationListInfo()
        {
            var translationListInfo = new TranslationListInfo();
            translationListInfo.LanguageIsoCode2 = IsoCode2One;
            translationListInfo.Translation = StringOne;

            return translationListInfo;
        }

        public static ClientLogInfo GetClientLogInfo()
        {
            var clientLogInfo = new ClientLogInfo();
            clientLogInfo.Country = StringOne;
            clientLogInfo.City = StringOne;
            clientLogInfo.Ip = IpOne;

            return clientLogInfo;
        }
    }
}