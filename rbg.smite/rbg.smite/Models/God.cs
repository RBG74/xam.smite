using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace rbg.smite.Models
{
    public class God
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        /*{
            get { return Name; }
            set { Name = value.Replace("\n", Environment.NewLine); }
        }*/

        [JsonProperty("Pantheon")]
        public string Pantheon { get; set; }

        [JsonProperty("Lore")]
        public string Lore { get; set; }

        [JsonProperty("Title")]
        public string Title { get; set; }

        [JsonProperty("Roles")]
        public string Roles { get; set; }

        [JsonProperty("godIcon_URL")]
        public string GodIcon_URL { get; set; }

        [JsonProperty("godCard_URL")]
        public string GodCard_URL { get; set; }

        public override string ToString()
        {
            return Name + " - " + Title;
        }
    }
}
