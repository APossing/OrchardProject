using System;
using System.Windows.Input;

namespace OrchardProject.ViewModels
{
    public delegate void CallBack();

    public class GeneralCommand : ICommand
    {
        public GeneralCommand(CallBack call)
        {
            _CBack = call;
        }

        private CallBack _CBack;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _CBack.Invoke();
        }
    }
}