using Newtonsoft.Json;
using rbg.smite.Api;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace rbg.smite.Api
{
    public static class Helper
    {
        private static string DEVID = AuthInfo.DEVID;
        private static string AUTHKEY = AuthInfo.AUTHKEY;

        private static string FORMAT = "json";
        private static string URLPREFIX = "http://api.smitegame.com/smiteapi.svc/";

        private static string LANGUAGECODE = "1";

        private static List<string> AvailableMethods = new List<string>{
            "createsession",
            "getgods"
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
            #warning ne pas créer une session à chaque appel de l'api
            var method = "createsession";
            var signature = CreateSignature(method, timestamp);
            var uri = URLPREFIX + method + FORMAT + "/" + DEVID + "/" + signature + "/" + timestamp;

            string json;
            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                json = wc.DownloadString(uri);
            }

            var result = JsonConvert.DeserializeObject<ApiResult>(json);

            return result.SessionId;
        }

        public static string GetGods()
        {
            string result = null;

            var method = "getgods";
            var timestamp = Utilities.GetUtcTimestamp();
            var signature = CreateSignature(method, timestamp);
            var session = CreateSession(timestamp);
            var uri = URLPREFIX + method + FORMAT + "/" + DEVID + "/" + signature + "/" + session + "/" + timestamp + "/" + LANGUAGECODE;

            using (var wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                result = wc.DownloadString(uri);//wc.UploadString(uri/*, myParameters*/);
            }
            return result;
        }

    }
}
