using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace rbg.smite.Api
{
    internal class Utilities
    {
        public static string GetMD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string GetUtcTimestamp()
        {
            return DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        }
    }
}
