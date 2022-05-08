using System;
using System.Windows.Input;

namespace DesignDashboard.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object?> _execute;
        private Func<object,bool>? _canExecute;

        public RelayCommand(Action<object?> execute)
        {
            _execute = execute;
            _canExecute = null;
        }

        public RelayCommand(Action<object?> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// CanExecuteChanged delegates the event subscription to the CommandManager.RequerySuggested event.
        /// This ensure that the WPF commanding infrastructure asks all RelayCommand objects
        /// if they can excute whenever it asks the built-in commands
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || CanExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }
    }
}
