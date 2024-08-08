using LogicalTier.DBObjects;
using System;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for the cournt user to see his User details change his password. Extends UserControlViewModelBase.
    internal class UserDetailsViewModel : UserControlViewModelBase
    {
        private User? _theUser;
        public User? TheUser
        {
            get
            {
                return _theUser;
            }
            set
            {
                _theUser = value;
                OnPropertyChanged();
            }
        }

        private string _password = ""; // separate property to allow password change
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        private bool _PasswordEnabled; // enabled the 'Password' field in the View
        public bool PasswordEnabled
        {
            get
            {
                return _PasswordEnabled;
            }
            set
            {
                _PasswordEnabled = value;
                OnPropertyChanged();

            }
        }


        public RelayCommand ChangePasswordCommand { get; }


        public UserDetailsViewModel() 
        {
            var mainWindowViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
            TheUser = mainWindowViewModel.TheCurrentUser?.User ?? throw new NullReferenceException();
            Password = TheUser.Password;

            ChangePasswordCommand = new(ChangePassword);
        }


        // Method to change the user password
        // First call will enable the 'Password' field so new password could be writen.
        // Second call will try to change the user's password with the writen password and print relevant message.
        public void ChangePassword (object? parameter)
        {
            try
            {
                if (!PasswordEnabled) // The user want to enter new password
                {
                    MessageBox.Show("Please write a new password in the \"Password\" field.\n\nRe-Click \"Change\" to confirm the change.", "Password Change", MessageBoxButton.OK, MessageBoxImage.Information);
                    PasswordEnabled = true;
                }
                else // The user want to confirm the change and update
                {
                    if (TheUser != null)
                    {
                        Password = Password.Trim();
                        string ChangeResult = TheUser.ChangePassword(Password);
                        if (string.IsNullOrEmpty(ChangeResult))
                        {
                            MessageBox.Show("Password changed Successfully.", "Password Change", MessageBoxButton.OK, MessageBoxImage.Information);
                            PasswordEnabled = false;
                        }
                        else 
                        {
                            MessageBox.Show(ChangeResult, "Password Change", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }
    }
}
