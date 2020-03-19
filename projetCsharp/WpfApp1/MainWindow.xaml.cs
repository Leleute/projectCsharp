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
using System.Windows.Forms;
using System.Drawing;
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
        int mode = 0;
        int lieu = 0; 
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
            CoronavirusInformation.Visibility = Visibility.Collapsed;

            bValid.IsEnabled = false;
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
            }
            return JsonConvert.DeserializeObject<List<Coronavirus>>(result);
        }

        private void HandleCheck(object sender, RoutedEventArgs e)
        {
            if ((sender as System.Windows.Controls.CheckBox).Tag.ToString() == "coun")
            {
                cbRegion.IsChecked = false;
                lieu = 1;
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
            if ((sender as System.Windows.Controls.CheckBox).Tag.ToString() == "reg")
            {
                loadFromApi();
                cbCountry.IsChecked = false;
                lieu = 2;
                         
            }
            if(mode != 0) bValid.IsEnabled = true;
        }

        private void button_choice(object sender, EventArgs e)
        {
            if ((sender as System.Windows.Controls.Button).Tag.ToString() == "cont")
            {
                mode = 1;
                ButCont.Background = new SolidColorBrush(Colors.Green);
                ButMort.Background = new SolidColorBrush(Colors.White);
                ButGuer.Background = new SolidColorBrush(Colors.White);
                ButAct.Background = new SolidColorBrush(Colors.White);

            }
            if ((sender as System.Windows.Controls.Button).Tag.ToString() == "mort")
            {
                mode = 2;
                ButCont.Background = new SolidColorBrush(Colors.White);
                ButMort.Background = new SolidColorBrush(Colors.Green);
                ButGuer.Background = new SolidColorBrush(Colors.White);
                ButAct.Background = new SolidColorBrush(Colors.White);
            }
            if ((sender as System.Windows.Controls.Button).Tag.ToString() == "guer")
            {
                mode = 3;
                ButCont.Background = new SolidColorBrush(Colors.White);
                ButMort.Background = new SolidColorBrush(Colors.White);
                ButGuer.Background = new SolidColorBrush(Colors.Green);
                ButAct.Background = new SolidColorBrush(Colors.White);
            }
            if ((sender as System.Windows.Controls.Button).Tag.ToString() == "active")
            {
                mode = 4;
                ButCont.Background = new SolidColorBrush(Colors.White);
                ButMort.Background = new SolidColorBrush(Colors.White);
                ButGuer.Background = new SolidColorBrush(Colors.White);
                ButAct.Background = new SolidColorBrush(Colors.Green); ;
            }
            if (lieu != 0) bValid.IsEnabled = true;
        }

        private void ChangeDataBase()
        {
            if(recherche.Text != null)
            {
                for(int i = 0; i< Data.Count; i++)
                {
                    if(lieu == 1 && Data[i].countryRegion.Contains(recherche.Text) == false)
                    {
                        Data.RemoveAt(i);
                        i--;
                    }
                    if(lieu == 2 && Data[i].provinceState.Contains(recherche.Text) == false)
                    {
                        Data.RemoveAt(i);
                        i--;
                    }
                }
            }
        }


        private void button_handler(object sender, EventArgs e)
        {
            ChangeDataBase();
            if(lieu == 1)
            {
                country.Visibility = Visibility.Visible;
                province.Visibility = Visibility.Collapsed;
            }
            if(lieu == 2)
            {
                country.Visibility = Visibility.Collapsed;
                province.Visibility = Visibility.Visible;
            }
            CoronavirusInformation.Visibility = Visibility.Visible;
            CoronavirusInformation.Items.Clear();
            int i = 0;
            if (mode == 1)
            {

                 var test = from Coronavirus in Data
                           orderby Coronavirus.confirmed descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Visible;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }
            if (mode == 2)
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.deaths descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Visible;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }
            if (mode == 3)
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.recovered descending
                           select Coronavirus;
                foreach(Coronavirus value in test)
                {
                    i++;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Visible;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Collapsed;
                }
                i = 0;
            }
            if (mode == 4)
            {
                var test = from Coronavirus in Data
                           orderby Coronavirus.active descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    i++;
                    CoronavirusInformation.Items.Add(value);
                    confirmed.Visibility = Visibility.Collapsed;
                    recovered.Visibility = Visibility.Collapsed;
                    death.Visibility = Visibility.Collapsed;
                    active.Visibility = Visibility.Visible;
                }
            }
            mode = 0;
            lieu = 0;
            ButCont.Background = new SolidColorBrush(Colors.White);
            ButMort.Background = new SolidColorBrush(Colors.White);
            ButGuer.Background = new SolidColorBrush(Colors.White);
            ButAct.Background = new SolidColorBrush(Colors.White);
            cbCountry.IsChecked = false;
            cbRegion.IsChecked = false;
            bValid.IsEnabled = false;
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



