using System;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Translation.Common.Helpers
{
    public static class StringHelper
    {
        public static bool IsAlphabeticalOrUnderscore(this string text)
        {
            var isAlphabetical = new Regex(@"[a-z_]+");

            var isValidated = isAlphabetical.IsMatch(text);
            return isValidated;
        }

        /// <summary>
        /// Checks if a string contains number, upper case, lower case and more than 7 character 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsValidPassword(this string text)
        {
            if (string.IsNullOrWhiteSpace(text)
                || text.Length < 8)
            {
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+", RegexOptions.Compiled);
            var hasLowerChar = new Regex(@"[a-z]+", RegexOptions.Compiled);
            var hasUpperChar = new Regex(@"[A-Z]+", RegexOptions.Compiled);

            var isValidated = hasNumber.IsMatch(text)
                              && hasUpperChar.IsMatch(text)
                              && hasLowerChar.IsMatch(text);
            return isValidated;
        }

        public static bool IsNotValidPassword(this string text)
        {
            return !IsValidPassword(text);
        }

        public static bool IsEmail(this string text)
        {
            try
            {
                var mailAddress = new MailAddress(text);
                return mailAddress.Address == text;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsNotEmail(this string text)
        {
            return !IsEmail(text);
        }

        public static bool IsEmpty(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static bool IsNotEmpty(this string text)
        {
            return !IsEmpty(text);
        }

        public static bool IsUrl(this string text)
        {
            var result = Uri.TryCreate(text, UriKind.Absolute, out var uriResult)
                         && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        public static bool IsNotUrl(this string text)
        {
            return !IsUrl(text);
        }

        public static string TrimOrDefault(this string text)
        {
            return text == null ? string.Empty : text.Trim();
        }

        /// <summary>
        /// checks if .xls, .xlsx or .csv
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool IsBasicDataUploadExtension(this string text)
        {
            return text == ".csv"
                   || text == ".xls"
                   || text == ".xlsx";
        }

        public static bool IsNotBasicDataUploadExtension(this string text)
        {
            return !IsBasicDataUploadExtension(text);
        }

        public static string ToBase64(this byte[] text)
        {
            return Convert.ToBase64String(text);
        }

        public static byte[] FromBase64(this string text)
        {
            return Convert.FromBase64CharArray(text.ToCharArray(), 0, text.Length);
        }

        public static T ThrowIfNullOrEmpty<T>(this T obj, string message = "", bool condition = false)
        {
            return obj == null || condition
                ? throw new ArgumentNullException(message)
                : obj is string && (obj as string).IsEmpty() 
                    ? throw new ArgumentNullException(message) 
                    : obj;
        }

        public static long ThrowIfWrongId(this long id, string name = "")
        {
            return id < 1
                ? throw new ArgumentException(name.IsEmpty() ? id.ToString() : $"{name} => {id}")
                : id;
        }

        public static Guid ThrowIfWrongUid(this Guid guid, string name = "")
        {
            return guid.IsEmptyGuid()
                ? throw new ArgumentException(name.IsEmpty() ? guid.ToString() : $"{name} => {guid}")
                : guid;
        }
    }
}