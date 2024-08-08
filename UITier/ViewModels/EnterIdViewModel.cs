using System.Linq;
using System.Windows;

namespace UITier.ViewModels
{
    // This ViewModel class is used for displaying a message box to input an ID.
    internal class EnterIdViewModel : WindowViewModelBase
    {
        private string _inputId = ""; // store the input string from the user.
        public string InputId
        {
            get 
            { 
                return _inputId; 
            }
            set
            {
                _inputId = value;
                OnPropertyChanged();
                EnterCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand EnterCommand { get; private set; } // The command means the user wants 'InputId' to be used.

        public EnterIdViewModel()
        {
            EnterCommand = new RelayCommand(CloseWindowWithTrueResult, (_ => !string.IsNullOrWhiteSpace(InputId)));
        }


        // Closes the dialog with a 'true' result value. This will indicate that the user want InputIdto be used.
        private static void CloseWindowWithTrueResult(object? parameter)
        {
            Window? window = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (window != null)
                window.DialogResult = true;
        }
    }
}
