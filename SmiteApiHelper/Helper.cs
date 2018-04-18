using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace SmiteApiHelper
{
    public static class Helper
    {
        private static string DEVID = "1340";
        private static string AUTHKEY = "F8EC16EAA851401DA626E4B90DA30086";

        private static string FORMAT = "json";
        private static string URLPREFIX = "http://api.smitegame.com/smiteapi.svc/";

        private static string LANGUAGECODE = "1";

        private static List<string> AvailableMethods = new List<string>{
            "createsession",
            ""
        };

        private static string CreateSignature(string method, string timestamp)
        {
            if (AvailableMethods.Contains(method))
            {
                string signature = DEVID + method + AUTHKEY + timestamp;

                return Utilities.GetMD5Hash(signature);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Invalid method name");
            }
        }

        private static string CreateSession(string timestamp)
        {
            string result = null;
            
            var method = "createsession";
            var signature = CreateSignature(method, timestamp);
            var uri = URLPREFIX + method + FORMAT + "/" + DEVID + "/" + signature + "/" + timestamp;
            //string myParameters = "param1=value1&param2=value2&param3=value3";

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                result = wc.DownloadString(uri);//wc.UploadString(uri/*, myParameters*/);
            }

            return result;
        }

        public static string GetGods()
        {
            string result = null;

            var method = "getgods";
            var timestamp = Utilities.GetUtcTimestamp();
            var signature = CreateSignature(method, timestamp);
            var session = CreateSession(timestamp);
            var uri = URLPREFIX + method + "/" + DEVID + "/" + signature + "/" + session + "/" + timestamp + "/" + LANGUAGECODE;

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                result = wc.DownloadString(uri);//wc.UploadString(uri/*, myParameters*/);
            }
            return result;
        }

    }
}
