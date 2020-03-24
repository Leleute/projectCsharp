using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    class CoronavirusViewModel
    {
        public static List<Coronavirus> apiLoader()
        {
            string url = "https://covid19.mathdro.id/api/confirmed";
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
            }
            List<Coronavirus> Data = JsonConvert.DeserializeObject<List<Coronavirus>>(result);
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].provinceState == null) Data[i].provinceState = Data[i].countryRegion;
            }
            return Data;
        }

        public static List<Coronavirus> ChargeDataView(string choixtri, int nbMax, string stringContenu, bool region)
        {
            int i = 0;
            List<Coronavirus> Data = new List<Coronavirus>();
            List<Coronavirus> DataFromApi = apiLoader();
            if (region == false)
            {
                for (int j = 0; j < DataFromApi.Count; j++)
                {
                    for (int k = 0; k < DataFromApi.Count; k++)
                    {
                        if (DataFromApi[j].countryRegion == DataFromApi[k].countryRegion && DataFromApi[j].provinceState != DataFromApi[k].provinceState)
                        {
                            DataFromApi[j].confirmed += DataFromApi[k].confirmed;
                            DataFromApi[j].deaths += DataFromApi[k].deaths;
                            DataFromApi[j].active += DataFromApi[k].active;
                            DataFromApi[j].recovered += DataFromApi[k].recovered;
                            DataFromApi.RemoveAt(k);
                            k--;
                        }
                    }
                }            
            }
            if (stringContenu != null)
            {
                for (int j = 0; j < DataFromApi.Count; j++)
                {
                    if( (DataFromApi[j].provinceState.Contains(stringContenu) == false && region == true) || (DataFromApi[j].countryRegion.Contains(stringContenu) == false && region == false))
                    {
                        Console.WriteLine(DataFromApi[j].countryRegion.Contains(stringContenu) + " " + region);
                        DataFromApi.RemoveAt(j);
                        j--;
                    }                   
                }
            }
            if(choixtri == "confirmed")
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.confirmed descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    Data.Add(value);
                    if (++i >= nbMax) break;
                }

                return Data;
            }
            else if (choixtri == "death")
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.deaths descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    Data.Add(value);
                    if (++i >= nbMax) break;
                }

                return Data;
            }
            else if (choixtri == "recovered")
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.recovered descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    
                    Data.Add(value);
                    if (++i >= nbMax) break;

                }
                return Data;
            }
            else
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.active descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {

                    Data.Add(value);
                    if (++i >= nbMax) break;

                }
                return Data;
            }            
        }
    }
}
