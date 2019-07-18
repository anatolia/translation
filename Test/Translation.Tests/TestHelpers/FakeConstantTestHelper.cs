using System;

using Translation.Common.Helpers;

namespace Translation.Tests.TestHelpers
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
        public static Guid OrganizationOneProjectOneLabelOneUid => new Guid("0aae2e10-2a3d-4ee6-adc5-658d83c50c8b");
        public const string OrganizationOneProjectOneLabelOneName = "Organization One Project One Label One";
        public const string OrganizationOneProjectOneLabelOneKey = "organization_one_project_one_label_one";

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

        public const string OrganizationTwoSuperAdminUserOneName = "Organization Two Super Admin User One";
        public const string OrganizationTwoSuperAdminUserOneEmail = "organizationtwosuperadminuserone@gmail.com";

        public const long OrganizationTwoIntegrationOneId = 1168161649L;
        public static Guid OrganizationTwoIntegrationOneUid => new Guid("110dadde-a4bd-4f53-abcb-fb6a1c69a7b8");
        public const string OrganizationTwoIntegrationOneName = "Organization Two Integration One";

        public const long OrganizationTwoIntegrationOneIntegrationClientOneId = 3168161601L;
        public static Guid OrganizationTwoIntegrationOneIntegrationClientOneUid => new Guid("e4db895f-569c-4ab4-804b-553b4a4b7518");
        public const string OrganizationTwoIntegrationOneIntegrationClientOneName = "Organization Two Integration One Integration Client One";

        public static Guid EmptyUid => Guid.Empty;
        public static Guid UidOne => new Guid("3A2F7BA0-0D4C-4231-B9DF-A15460B60BD2");
        public static Guid UidTwo => new Guid("5bb6cdd7-b5d1-4cf0-98c9-9bc68fa940c3");

        public static DateTime DateTimeOne => new DateTime(2019, 01, 01, 09, 00, 00);
        public static DateTime DateTimeTwo => new DateTime(2019, 01, 02, 18, 00, 00);
        public static DateTime DateTimeOneDayBefore => DateTime.Now.AddDays(-1);
        public static DateTime DateTimeOneWeekBefore => DateTime.Now.AddDays(-7);

        public const string StringEmpty = "";
        public const string StringOne = "String One";
        public const string StringTwo = "String Two";

        public const string StringSixtyFourOne = "bXk=";
        public const string StringSixtyFourTwo = "bXkx";

        public const int MinusOne = -1;
        public const int MinusTwo = -2;
        public const int Zero = 0;
        public const int One = 1;
        public const int Two = 2;
        public const int Three = 3;
        public const int Four = 4;
        public const int Five = 5;
        public const int Six = 6;
        public const int Seven = 7;
        public const int Eight = 8;
        public const int Nine = 9;
        public const int Ten = 10;
        
        public const double MinusDoubleOne = -1.04;
        public const double MinusDoubleTwo = -2.04;
        public const double MinusDoubleThree = -3.04;

        public const double DoubleOne = 1.04;
        public const double DoubleTwo = 2.04;
        public const double DoubleThree = 3.04;

        public const long LongOne = 2147483649L;
        public const long LongTwo = 9107483649L;

        public const bool BooleanTrue = true;
        public const bool BooleanFalse = false;

        public const string DateFormatOne = "2019-01-16";
        public const string DateFormatTwo = "2019-01-17";
        public const string DateFormatThree = "2019-01-18";

        public const string InvalidPassword = "invalid-password";
        public const string PasswordOne = "Test+-2018*";
        public const string PasswordTwo = "Test+-2018*+";
        public const string PasswordThree = "Test+-2018**";

        public const string UidStringOne = "ee4c5b8a-3498-4a7d-a9c8-74e86075853c";
        public const string UidStringTwo = "b64c5b8a-3498-4a7d-a9c8-74e86075853c";
        public const string UidStringThree = "1f6f9edc-4da4-444f-82dd-e089c9ebd68d";

        public const string InvalidEmail = "invalid-email";
        public const string EmailOne = "test@test.com";
        public const string EmailTwo = "test_1@test_1.com";
        public const string EmailThree = "test_2@test_2.com";

        public const string HttpUrl = "http://turkiye.gov.tr";
        public const string HttpWwwUrl = "http://www.turkiye.gov.tr";
        public const string ShortHttpUrl = "http://aka.ms";
        public const string HttpsUrl = "https://turkiye.gov.tr";
        public const string HttpsWwwUrl = "https://www.turkiye.gov.tr";
        public const string ShortHttpsUrl = "https://aka.ms";

        public const string IpOne = "85.201.85.116";
        public const string IpTwo = "87.204.85.116";

        public const string IsoCode2One = "TR";
        public const string IsoCode3One = "TUR";

        public const string CaseOne = "Case One";
        public const string CaseTwo = "Case Two";
        public const string CaseThree = "Case Three";
        public const string CaseFour = "Case Four";
        public const string CaseFive = "Case Five";
        public const string CaseSix = "Case Six";
        public const string CaseSeven = "Case Seven";
        public const string CaseEight = "Case Eight";
        public const string CaseNine = "Case Nine";
        public const string CaseTen = "Case Ten";
        public const string CaseEleven = "Case Eleven";
        public const string CaseTwelve = "Case Twelve";
        public const string CaseThirteen = "Case Thirten";

        public static string GetNewEmail()
        {
            return Guid.NewGuid().ToUidString() + "@email.com";
        }

        public static Guid GetStringAsGuid(string uid)
        {
            if (uid.IsNotUid())
            {
                return Guid.Empty;
            }
            return new Guid(uid);
        }

        public static string GetUidAsString(Guid uid)
        {
            return uid.ToString("D").ToLowerInvariant();
        }
    }
}