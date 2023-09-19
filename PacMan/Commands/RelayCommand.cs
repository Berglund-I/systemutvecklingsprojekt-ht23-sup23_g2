using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PacMan.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action<object> _excecute;
        private readonly Predicate<object> _canExecute;


        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action<object> execute, Predicate<object> predicate)
        {
            _excecute = execute;
            _canExecute = predicate;
        }

        public RelayCommand(Action<object> execute)
        {
            _excecute = execute;
            _canExecute = x => true;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _excecute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
