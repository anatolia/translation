using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Translation.Common.Helpers
{
    public class CryptoHelper
    {
        public int GetRandomNumber()
        {
            var byteArray = new byte[4];
            using (var provider = new RNGCryptoServiceProvider())
            {
                provider.GetBytes(byteArray);
                return Math.Abs(BitConverter.ToInt32(byteArray, 0));
            }
        }

        public byte[] GetRandomData(int bits)
        {
            var result = new byte[bits / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(result);
            }

            return result;
        }

        public byte[] GetSalt()
        {
            return GetRandomData(128);
        }

        public byte[] GetKey()
        {
            return GetRandomData(256);
        }

        public byte[] GetIV()
        {
            return GetRandomData(128);
        }

        public string GetKeyAsString()
        {
            return GetKey().ToBase64();
        }

        public string GetIVAsString()
        {
            return GetIV().ToBase64();
        }

        public string GetSaltAsString()
        {
            return GetSalt().ToBase64();
        }

        public string ConvertToString(byte[] text)
        {
            return Convert.ToBase64String(text);
        }

        public byte[] ConvertToByteArray(string text)
        {
            return Convert.FromBase64CharArray(text.ToCharArray(), 0, text.Length);
        }

        public string Hash(string text, string salt)
        {
            return Convert.ToBase64String(
                KeyDerivation.Pbkdf2(
                    text,
                    salt.FromBase64(),
                    KeyDerivationPrf.HMACSHA512,
                    28657,
                    256 / 8));
        }

        public string Encrypt(string text, byte[] key, byte[] iv)
        {
            ValidateParameters(text, key, iv);
            var textInBytes = Encoding.UTF8.GetBytes(text).ToBase64().FromBase64();
            byte[] result;
            using (var aes = Aes.Create())
            {
                ValidateAesInstanceCreation(aes);

                aes.Key = key;
                aes.IV = iv;

                using (var encryptor = aes.CreateEncryptor(key, iv))
                using (var to = new MemoryStream())
                using (var writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                {
                    writer.Write(textInBytes, 0, textInBytes.Length);
                    writer.FlushFinalBlock();
                    result = to.ToArray();
                }

                aes.Clear();
            }

            return result.ToBase64();
        }

        public string Decrypt(string text, byte[] key, byte[] iv)
        {
            ValidateParameters(text, key, iv);

            var textInBytes = text.FromBase64();
            byte[] result;
            int decryptedByteCount;
            using (var aes = Aes.Create())
            {
                ValidateAesInstanceCreation(aes);

                aes.Key = key;
                aes.IV = iv;

                try
                {
                    using (var decryptor = aes.CreateDecryptor(key, iv))
                    using (var from = new MemoryStream(textInBytes))
                    using (var reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                    {
                        result = new byte[textInBytes.Length];
                        decryptedByteCount = reader.Read(result, 0, result.Length);
                    }
                }
                catch (Exception)
                {
                    return string.Empty;
                }
                finally
                {
                    aes.Clear();
                }
            }

            return Encoding.UTF8.GetString(result, 0, decryptedByteCount);
        }

        private static void ValidateParameters(string text, byte[] key, byte[] iv)
        {
            text.ThrowIfNullOrEmpty(nameof(text));
            key.ThrowIfNullOrEmpty(nameof(key));
            iv.ThrowIfNullOrEmpty(nameof(iv));
        }

        private static void ValidateAesInstanceCreation(Aes aes)
        {
            aes.ThrowIfNullOrEmpty("Crypto algorithm not created!");
        }
    }
}