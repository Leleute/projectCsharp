using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;

namespace WpfApp1
{

    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Coronavirus> Data;
        public MainWindow()
        {
            InitializeComponent();
            loadFromApi();
            country.Visibility = Visibility.Collapsed;
            province.Visibility = Visibility.Collapsed;
            confirmed.Visibility = Visibility.Collapsed;
            recovered.Visibility = Visibility.Collapsed;
            death.Visibility = Visibility.Collapsed;
            active.Visibility = Visibility.Collapsed;
            lastU.Visibility = Visibility.Collapsed;
            lat.Visibility = Visibility.Collapsed;
            lon.Visibility = Visibility.Collapsed;
        }

        private void loadFromApi()
        {
            if(Data != null) Data.Clear();
            string url = "https://covid19.mathdro.id/api/confirmed";
            Data = apiLoader(url);
            for (int i = 0; i < Data.Count; i++)
            {
                if (Data[i].provinceState == null) Data[i].provinceState = Data[i].countryRegion;
            }
        }

        private List<Coronavirus> apiLoader(string url)
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
            return JsonConvert.DeserializeObject<List<Coronavirus>>(result);
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            CoronavirusInformation.Items.Clear();
            if ((sender as CheckBox).Tag.ToString() == "coun")
            {
                cbRegion.IsChecked = false;                            
                country.Visibility = Visibility.Visible;
                province.Visibility = Visibility.Collapsed;
                loadFromApi();
                for (int i = 0; i < Data.Count; i++)
                {
                    for (int j = 0; j < Data.Count; j++)
                    {
                        if(Data[i].countryRegion == Data[j].countryRegion && Data[i].provinceState != Data[j].provinceState)
                        {

                            Data[i].confirmed += Data[j].confirmed;
                            Data[i].deaths += Data[j].deaths;
                            Data[i].active += Data[j].active;
                            Data[i].recovered += Data[j].recovered;
                            Data.RemoveAt(j);
                            j--;    
                        }
                    }
                }
            }

            if ((sender as CheckBox).Tag.ToString() == "reg")
            {
                loadFromApi();
                CoronavirusInformation.Items.Clear();
                cbCountry.IsChecked = false;
                country.Visibility = Visibility.Collapsed;
                province.Visibility = Visibility.Visible;
               
            }
        }

        private void button_handler(object sender, EventArgs e)
        {
            CoronavirusInformation.Items.Clear();
            int i = 0;
            if ((sender as Button).Tag.ToString() == "cont")
            {

                 var test = from Coronavirus in Data
                           orderby Coronavirus.confirmed descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    if (i >= 50) break;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Visible;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }

            if ((sender as Button).Tag.ToString() == "mort")
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.deaths descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    if (i >= 50) break;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Visible;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }
            if ((sender as Button).Tag.ToString() == "guer")
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.recovered descending
                           select Coronavirus;
                foreach(Coronavirus value in test)
                {
                    i++;
                    if (i >= 50) break;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Visible;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }
            if ((sender as Button).Tag.ToString() == "active")
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.active descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    if (i >= 50) break;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Visible;
                }
            }
        }
    }

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
        public string iso2 { get; set; }
        public string iso3 { get; set; }

        public Coronavirus()
            {
            }
       }

}



