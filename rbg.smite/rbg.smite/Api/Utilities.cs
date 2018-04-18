using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Net;
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

        public static string GetRequest(string uri)
        {
            try
            { 
                using (var wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                    return wc.DownloadString(uri);
                }
            }
            catch(Exception exception)
            {
                Crashes.TrackError(exception);
                return null;
            }
}

        public static string PutRequest(string uri, string data)
        {
            try
            {
                using (var wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    return wc.UploadString(uri, "PUT", data);
                }
            }
            catch(Exception exception)
            {
                Crashes.TrackError(exception);
                return null;
            }
        }
    }
}
