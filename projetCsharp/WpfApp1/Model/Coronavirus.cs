using Newtonsoft.Json;

namespace WpfApp1.Model
{
    public class Coronavirus
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string provinceState { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string countryRegion { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float lastUpdate { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float lat { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public float lon { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int confirmed { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int recovered { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int deaths { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int active { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string admin2 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string iso2 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string iso3 { get; set; }
        public Coronavirus()
        {
        }
    }

}
