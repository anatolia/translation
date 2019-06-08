namespace Translation.Client.Web.Helpers
{
    public static class ConstantHelper
    {
        public const string ORGANIZATION_NAME = "Anatolia";
        public const string APP_NAME = "Translation";

        public const string KEY_DB_HOST = "DbHost";
        public const string KEY_DB_NAME = "DbName";
        public const string KEY_DB_USER = "DbUser";
        public const string KEY_DB_PASS = "DbPass";
        public const string KEY_DB_PORT = "DbPort";

        public const string KEY_SUPER_ADMIN_EMAIL = "SuperAdminEmail";
        public const string KEY_SUPER_ADMIN_FIRST_NAME = "SuperAdminFirstName";
        public const string KEY_SUPER_ADMIN_LAST_NAME = "SuperAdminLastName";
        public const string KEY_SUPER_ADMIN_PASS = "SuperAdminPass";

        public const string KEY_POSTMARK_KEY = "PostmarkKey";
        public const string KEY_POSTMARK_EMAIL = "PostmarkEmail";

        /// <summary>
        /// 3 letter country code header
        /// supplied from NGINX GEOIP2 module
        /// </summary>
        public const string HEADER_X_COUNTRY = "X-COUNTRY";
        public const string HEADER_X_CITY = "X-CITY";
    }
}