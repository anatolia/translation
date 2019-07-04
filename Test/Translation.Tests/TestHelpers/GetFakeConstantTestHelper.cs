using System;

using Translation.Common.Helpers;

namespace Translation.Tests.TestHelpers
{
    public class GetFakeConstantTestHelper
    {
        public const long CurrentUserId = 4198161609L;

        public const long OrganizationOneId = 8168161649L;
        public static Guid OrganizationOneUid => new Guid("d59f0cf5-b347-4c1e-883a-af2fd4333459");
        public const string OrganizationOneName = "Organization One";

        public const long OrganizationOneProjectOneId = 1167161649L;
        public static Guid OrganizationOneProjectOneUid => new Guid("7f062647-4c7d-4ba5-b380-55869c74a944");
        public const string OrganizationOneProjectOneName = "Organization One Project One";

        public const long OrganizationTwoId = 5168064649L;
        public static Guid OrganizationTwoUid => new Guid("bead3ad6-dc3c-44cd-9031-ad09438877f1");
        public const string OrganizationTwoName = "Organization Two";

        public const long OrganizationTwoProjectOneId = 2157161649L;
        public static Guid OrganizationTwoProjectOneUid => new Guid("c67d7fa4-5626-4777-a591-ded84dc76e50");
        public const string OrganizationTwoProjectOneName = "BlueSoft Project One";

        public static Guid EmptyUid => Guid.Empty;
        public static Guid UidOne => new Guid("3A2F7BA0-0D4C-4231-B9DF-A15460B60BD2");
        public static Guid UidTwo => new Guid("5bb6cdd7-b5d1-4cf0-98c9-9bc68fa940c3");

        public static DateTime DateTimeOne => new DateTime(2019, 01, 01, 09, 00, 00);
        public static DateTime DateTimeTwo => new DateTime(2019, 01, 02, 18, 00, 00);

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

        public const string IpOne = "87.204.85.116";

        public const string IsoCode2One = "TR";
        public const string IsoCode3One = "TUR";

        public const string CaseOne = "case_1";
        public const string CaseTwo = "case_2";
        public const string CaseThree = "case_3";
        public const string CaseFour = "case_4";
        public const string CaseFive = "case_5";
        public const string CaseSix = "case_6";
        public const string CaseSeven = "case_7";
        public const string CaseEight = "case_8";
        public const string CaseNine = "case_9";
        public const string CaseTen = "case_10";
        public const string CaseEleven = "case_11";
        public const string CaseTwelve = "case_12";
        public const string CaseThirteen = "case_13";

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