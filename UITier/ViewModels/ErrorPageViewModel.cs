using System;

namespace UITier.ViewModels
{
    // A class for displaying Exception details on error page when they are thrown during the program.
    internal class ErrorPageViewModel : UserControlViewModelBase
    {
        private string _errorMessage;
        public string ErrorMessage 
        { 
            get 
            { 
                return _errorMessage; 
            } 
            set 
            {  
                _errorMessage = value;
                OnPropertyChanged();
            } 
        }

        private string _exceptionType;
        public string ExceptionType
        {
            get 
            { 
                return _exceptionType; 
            }

            set
            {
                _exceptionType = value;
                OnPropertyChanged();
            }
        }

        private string _stackTrace;
        public string StackTrace
        {
            get 
            { 
                return _stackTrace; 
            }
            set
            {
                _stackTrace = value;
                OnPropertyChanged();
            }
        }

        // builder that gets Exception "ex" for which its details will be displayed in the error page.
        public ErrorPageViewModel(Exception ex)
        {
            _errorMessage = ex.Message;
            _exceptionType = ex.GetType().Name;
            _stackTrace = ex.StackTrace ?? "StackTrace is null";
        }
    }
}
