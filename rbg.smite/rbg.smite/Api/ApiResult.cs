using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace rbg.smite.Api
{
    internal class ApiResult
    {
        private string returnMessage;
        [JsonProperty("ret_msg")]
        public string ReturnMessage
        {
            get { return returnMessage; }
            set { returnMessage = value; }
        }

        private string sessionId;
        [JsonProperty("session_id")]
        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        private string timestamp;
        [JsonProperty("timestamp")]
        public string Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

    }
}
