using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using WpfApp1.Model;
using WpfApp1.ViewModel.Commands;

namespace WpfApp1.ViewModel
{
    class CoronavirusViewModel : INotifyPropertyChanged
    {
        public ValidationChoix validChoix { get; set; }

        private Visibility _visibilityGrid;
        public Visibility VisibilityGrid
        {
            get
            {
                return _visibilityGrid;
            }
            set
            {
                _visibilityGrid = value;
                RaisePropertyChanged("VisibilityGrid");
            }
        }

        private Visibility _visibilityCountry;
        public Visibility VisibilityCountry
        {
            get
            {
                return _visibilityCountry;
            }
            set
            {
                _visibilityCountry = value;
                RaisePropertyChanged("VisibilityCountry");
            }
        }

        private Visibility _visibilityRegion;
        public Visibility VisibilityRegion
        {
            get
            {
                return _visibilityRegion;
            }
            set
            {
                _visibilityRegion = value;
                RaisePropertyChanged("VisibilityRegion");
            }
        }

        private Visibility _visibilityConf;
        public Visibility VisibilityConf
        {
            get
            {
                return _visibilityConf;
            }
            set
            {
                _visibilityConf = value;
                RaisePropertyChanged("VisibilityConf");
            }
        }

        private Visibility _visibilityDeath;
        public Visibility VisibilityDeath
        {
            get
            {
                return _visibilityDeath;
            }
            set
            {
                _visibilityDeath = value;
                RaisePropertyChanged("VisibilityDeath");
            }
        }

        private Visibility _visibilityRecov;
        public Visibility VisibilityRecov
        {
            get
            {
                return _visibilityRecov;
            }
            set
            {
                _visibilityRecov = value;
                RaisePropertyChanged("VisibilityRecov");
            }
        }

        private Visibility _visibilityActive;
        public Visibility VisibilityActive
        {
            get
            {
                return _visibilityActive;
            }
            set
            {
                _visibilityActive = value;
                RaisePropertyChanged("VisibilityActive");
            }
        }

        private bool _cbCont;
        public bool CbCont
        {
            get
            {
                return _cbCont;
            }
            set
            {
                if (CbCont != value)
                {
                    _cbCont = value;
                    RaisePropertyChanged("CbCont");
                }
            }
        }

        private bool _cbMort;
        public bool CbMort
        {
            get
            {
                return _cbMort;
            }
            set
            {
                if (CbMort != value)
                {
                    _cbMort = value;
                    RaisePropertyChanged("CbMort");
                }
            }
        }

        private bool _cbGuer;
        public bool CbGuer
        {
            get
            {
                return _cbGuer;
            }
            set
            {
                if (CbGuer != value)
                {
                    _cbGuer = value;
                    RaisePropertyChanged("CbGuer");
                }
            }
        }

        private bool _cbActive;
        public bool CbActive
        {
            get
            {
                return _cbActive;
            }
            set
            {
                if (CbActive != value)
                {
                    _cbActive = value;
                    RaisePropertyChanged("CbActive");
                }
            }
        }

        private bool _rbregion;
        public bool Rbregion
        {
            get
            {
                return _rbregion;
            }
            set
            {
                if (Rbregion != value)
                {
                    _rbregion = value;
                    RaisePropertyChanged("Rbregion");
                }
            }
        }

        private string _rechercheSpe;
        public String RechercheSpe
        {
            get
            {
                return _rechercheSpe;
            }
            set
            {
                if (_rechercheSpe != value)
                {
                    _rechercheSpe = value;
                    RaisePropertyChanged("RechercheSpe");
                }
            }
        }

        private string _nbMax;
        public String NbMax
        {
            get
            {
                return _nbMax;
            }
            set
            {
                if (_nbMax != value)
                {
                    if (value == "" || value == null) value = "0";
                    _nbMax = value;
                    RaisePropertyChanged("NbMax");
                }
            }
        }

        public ValidationChoix ValidationChoix { get; set; }

        private List<Coronavirus> _coronavirusData;
        public List<Coronavirus> CoronavirusData
        {
            get
            {
                return _coronavirusData;
            }
            set
            {
                if (_coronavirusData != value)
                {
                    _coronavirusData = value;
                    RaisePropertyChanged("CoronavirusData");
                }
            }

        }

        public CoronavirusViewModel()
        {
            this.ValidationChoix = new ValidationChoix(this);
            NbMax = "50";
            Rbregion = true;
            VisibilityConf = Visibility.Collapsed;
            CoronavirusData = new List<Coronavirus>();
        }

        public void VerifTextNotEmpty()
        {
            if (NbMax == "" || NbMax == null)
            {
                NbMax = "0";
            }
        }

        public List<Coronavirus> apiLoader()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string url = "https://covid19.mathdro.id/api/confirmed";
            string result = null;
            string urlApi = string.Format(url);
            WebRequest requestApi = WebRequest.Create(urlApi);
            requestApi.Method = "GET";
            HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
            wr.KeepAlive = false;
            wr.Timeout = System.Threading.Timeout.Infinite;
            wr.ProtocolVersion = HttpVersion.Version10;
            wr.AllowWriteStreamBuffering = false;
            HttpWebResponse responseOject = null;
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

        public void checkVisibility()
        {
            if (CbCont)
            {
                VisibilityConf = Visibility.Visible;
            }
            else
            {
                VisibilityConf = Visibility.Collapsed;
            }
            if (CbMort)
            {
                VisibilityDeath = Visibility.Visible;
            }
            else
            {
                VisibilityDeath = Visibility.Collapsed;
            }
            if (CbGuer)
            {
                VisibilityRecov = Visibility.Visible;
            }
            else
            {
                VisibilityRecov = Visibility.Collapsed;
            }
            if (CbActive)
            {
                VisibilityActive = Visibility.Visible;
            }
            else
            {
                VisibilityActive = Visibility.Collapsed;
            }
            if (Rbregion)
            {
                VisibilityRegion = Visibility.Visible;
                VisibilityCountry = Visibility.Collapsed;
            }
            else
            {
                VisibilityRegion = Visibility.Collapsed;
                VisibilityCountry = Visibility.Visible;
            }
        }

        public void UseButton()
        {
            int i = 0;
            List<Coronavirus> Data = new List<Coronavirus>();
            List<Coronavirus> DataFromApi = apiLoader();
            checkVisibility();
            if (Rbregion == false)
            {
                for (int j = 0; j < DataFromApi.Count; j++)
                {
                    for (int k = 0; k < DataFromApi.Count; k++)
                    {
                        if (DataFromApi[j].countryRegion == DataFromApi[k].countryRegion)
                        {
                            if (DataFromApi[j].admin2 != DataFromApi[k].admin2 && DataFromApi[j].provinceState == DataFromApi[k].provinceState)
                            {
                                DataFromApi.RemoveAt(k);
                                k--;
                            }

                            else if (DataFromApi[j].provinceState != DataFromApi[k].provinceState)
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
            }

            if (RechercheSpe != null)
            {
                for (int j = 0; j < DataFromApi.Count; j++)
                {
                    if ((DataFromApi[j].provinceState.Contains(RechercheSpe) == false && Rbregion == true) || (DataFromApi[j].countryRegion.Contains(RechercheSpe) == false && Rbregion == false))
                    {
                        DataFromApi.RemoveAt(j);
                        j--;
                    }
                }
            }

            if (CbCont == true)
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.confirmed descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    if (NbMax == "0") break;
                    Data.Add(value);
                    if (++i >= int.Parse(NbMax)) break;

                }

            }
            else if (CbMort == true)
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.deaths descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    if (NbMax == "0") break;
                    Data.Add(value);
                    if (++i >= int.Parse(NbMax)) break;
                }
            }
            else if (CbGuer == true)
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.recovered descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    if (NbMax == "0") break;
                    Data.Add(value);
                    if (++i >= int.Parse(NbMax)) break;
                }
            }
            else
            {
                var test = from Coronavirus in DataFromApi
                           orderby Coronavirus.active descending
                           select Coronavirus;
                foreach (Coronavirus value in test)
                {
                    if (NbMax == "0") break;
                    Data.Add(value);
                    if (++i >= int.Parse(NbMax)) break;

                }
            }
            CoronavirusData = Data;

        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
