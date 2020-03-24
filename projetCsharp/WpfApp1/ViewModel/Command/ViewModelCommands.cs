using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModel.Command
{
    public class LancerAppliCommand : ICommand
    {
        public ViewModelCoronavirus viewModel {get; set;}

        public LancerAppliCommand(ViewModelCoronavirus vm)
        {
            this.viewModel = vm;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
            //throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            this.viewModel.lancerAppli();
        }
    }
}
