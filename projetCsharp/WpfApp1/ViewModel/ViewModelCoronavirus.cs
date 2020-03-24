using System;
using System.Collections.Generic;
using WpfApp1.Model;
using System.Windows;
using System.Windows.Input;
using System.Windows.Data;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using WpfApp1.ViewModel.Command;

namespace WpfApp1.ViewModel
{
    public class ViewModelCoronavirus //: INotifyPropertyChanged
    {
        public LancerAppliCommand lancerAppliCommand { get; set; }
        public string contientString { get; set; }
        private bool _isDataGridVisible;
        public bool isDataGridVisible
        {
            get { return _isDataGridVisible; }
            set
            {
                _isDataGridVisible = value;
                OnPropertyChanged(nameof(isDataGridVisible));
            }
        }

        public bool isCountryVisible { get; set; }
        public bool isProvinceVisible { get; set; }
        public bool isConfVisible { get; set; }
        public bool isDeathVisible { get; set; }
        public bool isRecoveredVisible { get; set; }
        public bool isActiveVisible{ get; set; }
        public int lieu { get; set; }
        public bool contCheck { get; set; }
        public bool mortCheck { get; set; }
        public bool guerCheck { get; set; }
        public bool activeCheck { get; set; }
        public bool bValid { get; set; }
        public bool cbregion { get; set; }
        public bool cbcountry { get; set; }
        public List<Coronavirus> Data { get; set; }
        public List<Coronavirus> DataFromAPI { get; set; }
        public string rechercheString = "";
        int i;
        private int _nbMax;       
        public int nbMax
        {
            get { return _nbMax; }
            set
            {
                _nbMax = value;
                OnPropertyChanged(nameof(nbMax));
            }
        }
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelCoronavirus()
        {
            lancerAppliCommand = new LancerAppliCommand(this);
            isCountryVisible = false;
            isProvinceVisible = false;
            isConfVisible = false;
            isDeathVisible = false;
            isRecoveredVisible = false;
            isActiveVisible = false;
            isDataGridVisible = false;
            contCheck = false;
            mortCheck = false;
            guerCheck = false;
            activeCheck = false;
            bValid = false;
            cbregion = true;
            cbcountry = false;
            bValid = false;
            nbMax = 50;
            lieu = 0;
            contientString = "";
            Data = new List<Coronavirus>();
            loadFromApi();
            i = 0;
        }

        private void loadFromApi()
        {
            if (DataFromAPI != null) Data.Clear();
            string url = "https://covid19.mathdro.id/api/confirmed";
            DataFromAPI = apiLoader(url);
            for (int i = 0; i < DataFromAPI.Count; i++)
            {
                if (DataFromAPI[i].provinceState == null) DataFromAPI[i].provinceState = DataFromAPI[i].countryRegion;
            }
            int j = 0;
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
                cbregion = false;
                lieu = 1;
                loadFromApi();
                for (int i = 0; i < Data.Count; i++)
                {
                    for (int j = 0; j < Data.Count; j++)
                    {
                        if (Data[i].countryRegion == Data[j].countryRegion && Data[i].provinceState != Data[j].provinceState)
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
                cbcountry = false;
                lieu = 2;

            }
            CheckButton();
        }

        private void ChangeDataBase()
        {
            if (rechercheString != null)
            {
                for (int i = 0; i < Data.Count; i++)
                {
                    if (lieu == 1 && Data[i].countryRegion.Contains(rechercheString) == false)
                    {
                        Data.RemoveAt(i);
                        i--;
                    }
                    if (lieu == 2 && Data[i].provinceState.Contains(rechercheString) == false)
                    {
                        Data.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void CheckButton()
        {
            if (lieu != 0 && ((contCheck || mortCheck || guerCheck || activeCheck) == true) && nbMax != 0)
            {
                bValid = true;
            }
            else
            {
                bValid = false;
            }
        }

        public void lancerAppli()
        {
            Console.WriteLine(nbMax);
            isDataGridVisible = true;
            if(cbcountry == true) isCountryVisible = true;
            if(cbregion == true) isProvinceVisible = true;           
            if(contCheck == true) isConfVisible = true;
            if(mortCheck == true) isDeathVisible = true;
            if(guerCheck == true) isRecoveredVisible = true;
            if(activeCheck == true) isActiveVisible = true;
        }
    }

    class BoolToVisibleOrCollapsed : IValueConverter
    {
        #region Constructors
        /// <summary>
        /// The default constructor
        /// </summary>
        public BoolToVisibleOrCollapsed() { }
        #endregion

        #region IValueConverter Members
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
                return Visibility.Visible;
            else
                return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Visibility visibility = (Visibility)value;

            if (visibility == Visibility.Visible)
                return true;
            else
                return false;
        }
        #endregion
    }

    public class BindingProxy : Freezable
    {
        public static readonly DependencyProperty DataProperty =
           DependencyProperty.Register("Data", typeof(object),
              typeof(BindingProxy));
        public object Data
        {
            get { return GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }
        #region Overrides of Freezable

        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }
        #endregion       
    }

    

}
