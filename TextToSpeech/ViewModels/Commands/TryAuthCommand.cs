using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextToSpeech.ViewModels.Commands
{
    public class TryAuthCommand : ICommand
    {
        T2PViewModel _viewModel;

        public TryAuthCommand(T2PViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.TryAuthenticate();
        }
    }
}
