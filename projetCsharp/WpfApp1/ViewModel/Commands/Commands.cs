using System;
using System.Windows.Input;

namespace WpfApp1.ViewModel.Commands
{
    class ValidationChoix : ICommand
    {
        public CoronavirusViewModel ViewModel { get; set; }

        public ValidationChoix(CoronavirusViewModel tVM)
        {
            ViewModel = tVM;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (ViewModel.CbCont == true || ViewModel.CbMort == true || ViewModel.CbGuer == true || ViewModel.CbActive == true) return true;
            else return false;
        }

        public void Execute(object parameter)
        {
            ViewModel.UseButton();
        }
    }
}
