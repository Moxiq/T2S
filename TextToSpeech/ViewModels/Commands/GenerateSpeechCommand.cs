using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TextToSpeech.ViewModels.Commands
{
    public class GenerateSpeechCommand : ICommand
    {
        T2PViewModel _viewModel;

        public event EventHandler CanExecuteChanged;

        public GenerateSpeechCommand(T2PViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.GenerateSpeech();
        }
    }
}
