using Microsoft.AppCenter.Crashes;
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
        internal static string DEVID = AuthInfo.DEVID;
        internal static string AUTHKEY = AuthInfo.AUTHKEY;

        internal static string FORMAT = "json";
        internal static string URLPREFIX = "http://api.smitegame.com/smiteapi.svc/";

        internal static string LANGUAGECODE = "1";

        private static List<string> AvailableMethods = new List<string>{
            "createsession",
            "getgods"
        };

        internal static string CreateSignature(string method, string timestamp)
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

        

        public static string GetGods()
        {
            string result = null;

            try
            {
                var method = "getgods";
                var timestamp = Utilities.GetUtcTimestamp();
                var signature = CreateSignature(method, timestamp);
                var session = SessionManager.GetSession(timestamp);
                var uri = URLPREFIX + method + FORMAT + "/" + DEVID + "/" + signature + "/" + session + "/" + timestamp + "/" + LANGUAGECODE;

                result = Utilities.GetRequest(uri);
            }
            catch(Exception exception)
            {
                Crashes.TrackError(exception);
            }

            return result;
        }

    }
}
