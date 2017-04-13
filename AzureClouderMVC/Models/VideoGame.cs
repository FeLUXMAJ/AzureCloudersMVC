using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureClouderMVC.Models
{
    public class VideoGame
    {
        [JsonProperty(PropertyName = "id")]
        public long VideoGameId { get; set; }

        public string Name { get; set; }

        public string Slug { get; set; }

        public string Url { get; set; }

        public string Summary { get; set; }

        public string Storyline { get; set; }
    }
}
