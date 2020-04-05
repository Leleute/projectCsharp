using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        CoronavirusViewModel viewModel = new CoronavirusViewModel();
        public MainWindow()
        {
            InitializeComponent();
            base.DataContext = viewModel;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
