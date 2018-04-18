using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rbg.smite.Models
{
    public class God
    {
        
        private string name;
        [JsonProperty("Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string pantheon;
        [JsonProperty("Pantheon")]
        public string Pantheon
        {
            get { return pantheon; }
            set { pantheon = value; }
        }

        private string lore;
        [JsonProperty("Lore")]
        public string Lore
        {
            get { return lore; }
            set { lore = value.Replace("\\n", Environment.NewLine); }
        }

        private string title;
        [JsonProperty("Title")]
        public string Title
        {
            get { return title; }
            set { title = value.First().ToString().ToUpper() + value.Substring(1); }
        }

        private string roles;
        [JsonProperty("Roles")]
        public string Roles
        {
            get { return roles; }
            set { roles = value; }
        }
        
        private string godIcon_URL;
        [JsonProperty("godIcon_URL")]
        public string GodIcon_URL
        {
            get { return godIcon_URL; }
            set { godIcon_URL = value; }
        }

        private string godCard_URL;
        [JsonProperty("godCard_URL")]
        public string GodCard_URL
        {
            get { return godCard_URL; }
            set { godCard_URL = value; }
        }

        public override string ToString()
        {
            return Name + " - " + Title;
        }
    }
}
