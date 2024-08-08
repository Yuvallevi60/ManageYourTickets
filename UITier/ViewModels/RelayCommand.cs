using System;
using System.Windows.Input;

namespace UITier.ViewModels
{
    // The RelayCommand class implements the ICommand interface, providing a way to define commands for user interface interactions.
    internal class RelayCommand : ICommand
    {
        private Action<object?> _execute; // The action to be executed when the command is triggered.

        private Func<object?, bool>? _canExecute; // The function to determine if the command can be executed.

        public event EventHandler? CanExecuteChanged; // Event that is raised when the command's executability changes.



        // Constructor
        public RelayCommand(Action<object?> execute, Func<object?, bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }



        // Calls the _canExecute function to determine if the command can be executed.
        public bool CanExecute(object? parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true; // If no _canExecute function is provided, the command is always executable.              
        }


        // Executes the stored action when the command is triggered.
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }


        // Notifies subscribers that the command's executability may have changed.
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
