using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace rbg.smite.Api.Models
{
    public class MyJsonSession
    {
        [JsonProperty("session_id")]
        public string session_id;
        [JsonProperty("timestamp")]
        public string timestamp;
    }
}
