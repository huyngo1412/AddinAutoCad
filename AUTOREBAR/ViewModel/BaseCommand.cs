using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AUTOREBAR.ViewModel
{
    public class BaseCommand<T> : ICommand
    {
        private readonly Predicate<T> _canExecute;
        private readonly Action<T> _execute;
        public BaseCommand(Predicate<T> canExcute, Action<T> execute)
        {

            _canExecute = canExcute;
            _execute = execute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }
        public void Execute(object parameter)
        {

            _execute((T)parameter);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}
