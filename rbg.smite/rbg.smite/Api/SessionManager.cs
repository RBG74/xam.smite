using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using rbg.smite.Api.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace rbg.smite.Api
{
    public static class SessionManager
    {
        private static string MYJSONURL = "https://api.myjson.com/bins/twg8b";
        public static string GetSession(string timestamp)
        {
            string sessionId = null;

            try
            {
                bool needNewSession = true;
                var sessionObj = GetSessionFromMyJson();
                try
                {
                    var sessionCreateDate = DateTime.ParseExact(sessionObj.timestamp, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
                    needNewSession = sessionCreateDate.AddMinutes(15) < DateTime.UtcNow;
                }
                catch (FormatException)
                {
                    needNewSession = true;
                }

                if (needNewSession)
                {
                    sessionId = CreateSession(timestamp);
                    Analytics.TrackEvent("New session created");
                }
                else
                {
                    sessionId = sessionObj.session_id;
                    Analytics.TrackEvent("Existing session used");
                }
            }
            catch(Exception exception)
            {
                Crashes.TrackError(exception);
            }

            return sessionId;
        }

        private static MyJsonSession GetSessionFromMyJson()
        {
            var json = Utilities.GetRequest(MYJSONURL);
            var obj = JsonConvert.DeserializeObject<MyJsonSession>(json);

            return obj;
        }

        private static string CreateSession(string timestamp)
        {
            var method = "createsession";
            var signature = Helper.CreateSignature(method, timestamp);
            var uri = Helper.URLPREFIX + method + Helper.FORMAT + "/" + Helper.DEVID + "/" + signature + "/" + timestamp;

            var json = Utilities.GetRequest(uri);

            var result = JsonConvert.DeserializeObject<ApiResult>(json);

            var data = JsonConvert.SerializeObject(
                new MyJsonSession { session_id = result.SessionId, timestamp = timestamp }
                );
            Utilities.PutRequest(MYJSONURL, data);
            
            return result.SessionId;
        }
    }
}
