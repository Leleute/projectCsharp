using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Http;
using System.IO;

using Newtonsoft.Json;

namespace testAPI
{

    class Program
    {
        public static void Main(string[] arg)
        {
            string url = "https://covid19.mathdro.id/api/confirmed";
            Coronavirus[] result = null;
            apiLoader prog = new apiLoader();
            result = prog.Page_Load(url);
            for (int i = 0; i < result.Length; i++)
            {
                Console.WriteLine(result[i].countryRegion);

            }
            System.Threading.Thread.Sleep(500000);
        }
    }
    class apiLoader
    {
        public Coronavirus[] Page_Load(string url)
        {
            string result = null;
            string urlApi = string.Format(url);
            WebRequest requestApi = WebRequest.Create(urlApi);
            requestApi.Method = "GET";
            HttpWebResponse responseOject = null; ;
            responseOject = (HttpWebResponse)requestApi.GetResponse();
            using (Stream stream = responseOject.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
                ;
            }
            return JsonConvert.DeserializeObject<Coronavirus[]>(result);

        }
    }

    class Coronavirus
    {
        public string provinceState;
        public string countryRegion;
        public float lastUpdate;
        public float lat;
        public float lon;
        public int confirmed;
        public int recovered;
        public int death;
        public int active;
        //public int admin2;
        //public int fips;
        //public int combinedKey;
        public string iso2;
        public string iso3;
    }

}
