using Newtonsoft.Json;

namespace WpfApp1.Model
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.ComponentModel;


    public class Coronavirus
    {
        public string provinceState { get; set; }
        public string countryRegion { get; set; }
        public float lastUpdate { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public int confirmed { get; set; }
        public int recovered { get; set; }
        public int deaths { get; set; }
        public int active { get; set; }
        public string admin2 { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string iso2 { get; set; }
        public string iso3 { get; set; }
        public Coronavirus()
            {
            }
    }

}
