using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BoatManager.Commands
{
    class RelayCommand : ICommand   
    {
        public event EventHandler CanExecuteChanged;
        private Action doWork;

        public RelayCommand(Action work)
        {
            doWork = work;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            doWork();
        }

    }
}
