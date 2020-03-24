using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApp1.Model;
using WpfApp1.ViewModel;

namespace WpfApp1
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            resetVisibility();
            CoronavirusInformation.Visibility = Visibility.Collapsed;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        public void button_handler(object sender, EventArgs e)
        {
            resetVisibility();
            CoronavirusInformation.Visibility = Visibility.Visible;
            if (ButCont.IsChecked == true)
            {
                confirmed.Visibility = Visibility.Visible;
            }
            if (ButMort.IsChecked == true)
            {
                death.Visibility = Visibility.Visible;
            }
            if (ButGuer.IsChecked == true)
            {
                recovered.Visibility = Visibility.Visible;
            }
            if (ButAct.IsChecked == true)
            {
                active.Visibility = Visibility.Visible;
            }
            if (rbCountry.IsChecked == true)
            {
                country.Visibility = Visibility.Visible;
            }
            if (rbRegion.IsChecked == true)
            {
                province.Visibility = Visibility.Visible;
            }
            
            if (ButCont.IsChecked == true)
            {
                List<Coronavirus> Data = CoronavirusViewModel.ChargeDataView("confirmed", int.Parse(nbMax.Text), recherche.Text, rbRegion.IsChecked ?? true);
                CoronavirusInformation.ItemsSource = Data;
            }
            else if (ButMort.IsChecked == true)
            {
                List<Coronavirus> Data = CoronavirusViewModel.ChargeDataView("death", int.Parse(nbMax.Text), recherche.Text, rbRegion.IsChecked ?? true);
                CoronavirusInformation.ItemsSource = Data;
            }
            else if (ButGuer.IsChecked == true)
            {
                List<Coronavirus> Data = CoronavirusViewModel.ChargeDataView("recovered", int.Parse(nbMax.Text), recherche.Text, rbRegion.IsChecked ?? true);
                CoronavirusInformation.ItemsSource = Data;
            }
            else if (ButAct.IsChecked == true)
            {
                List<Coronavirus> Data = CoronavirusViewModel.ChargeDataView("active", int.Parse(nbMax.Text), recherche.Text, rbRegion.IsChecked ?? true);
                CoronavirusInformation.ItemsSource = Data;
            }
            CoronavirusInformation.Items.Refresh();
            rbCountry.IsChecked = true;
            rbRegion.IsChecked = false;
            ButCont.IsChecked = false;
            ButMort.IsChecked = false;
            ButGuer.IsChecked = false;
            ButAct.IsChecked = false;
            bValid.IsEnabled = false;
        }

        public void resetVisibility()
        {
            country.Visibility = Visibility.Collapsed;
            province.Visibility = Visibility.Collapsed;
            confirmed.Visibility = Visibility.Collapsed;
            recovered.Visibility = Visibility.Collapsed;
            death.Visibility = Visibility.Collapsed;
            active.Visibility = Visibility.Collapsed;
            CoronavirusInformation.Visibility = Visibility.Collapsed;

        }

        public void verifButton(object sender, RoutedEventArgs e)
        {   if(String.IsNullOrWhiteSpace(nbMax.Text) == true)
            {
                nbMax.Text = "0";
            }
        
            if((ButCont.IsChecked == true || ButMort.IsChecked == true || ButGuer.IsChecked == true || ButAct.IsChecked == true ))
            {
                bValid.IsEnabled = true;
            }
        }
    }
}