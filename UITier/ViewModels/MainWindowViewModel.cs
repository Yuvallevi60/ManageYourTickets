using LogicalTier;
using LogicalTier.DBObjects;
using LogicalTier.UserState;
using System;
using System.Windows;
using UITier.Views;

namespace UITier.ViewModels
{
    // ViewModel class for the main window of the application.
    internal class MainWindowViewModel : WindowViewModelBase
    {
        private bool _isLogged; // 'true' if there is a user logged-in
        public bool IsLogged
        {
            get
            {
                return _isLogged;
            }
            set
            {
                _isLogged = value;
                OnPropertyChanged();
                LogOutCommand.RaiseCanExecuteChanged();
                NavBarButtonCommand.RaiseCanExecuteChanged();
                OpenWindowCommand.RaiseCanExecuteChanged();
            }
        }

        private CurrentUser? _theCurrentUser;
        public CurrentUser? TheCurrentUser
        {
            get
            {
                return _theCurrentUser;
            }
            set
            {
                _theCurrentUser = value;
                OnPropertyChanged();
            }
        }

        private string _idNumber = ""; // input from the user to log-in
        public string IdNumber
        {
            get 
            { 
                return _idNumber; 
            }
            set
            {
                _idNumber = value;
                OnPropertyChanged();
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password = ""; // input from the user to log-in
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                LogInCommand.RaiseCanExecuteChanged();
            }
        }

        private bool[] _openExpanders; // the 'IsExpanded' value of the navigition menu's Expenders in MainPanelView
        public bool[] OpenExpanders
        {
            get 
            { 
                return _openExpanders; 
            }
            set 
            { 
                _openExpanders = value; 
                OnPropertyChanged(); 
            }
        }

        private UserControlViewModelBase _currentPageVM; // the view model that shown in MainPanelView
        public UserControlViewModelBase CurrentPageVM
        {
            get
            {
                return _currentPageVM;
            }
            set
            {
                _currentPageVM = value;
                OnPropertyChanged();
            }
        }


        // Commands for user interactions.
        public RelayCommand UserActivityCommand { get; }
        public RelayCommand LogInCommand { get; }
        public RelayCommand LogOutCommand { get; }
        public RelayCommand NavBarButtonCommand { get; }
        public RelayCommand OpenWindowCommand { get; }

        public MainWindowViewModel()
        {
            _currentPageVM = new HomePageViewModel();
            _openExpanders = new bool[5];

            UserActivityCommand = new RelayCommand(UserActivity, (_ => (TheCurrentUser != null)));
            LogInCommand = new RelayCommand(LogIn, (_ => (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(IdNumber) && !IsLogged)));
            LogOutCommand = new RelayCommand(LogOut, _ => IsLogged);
            NavBarButtonCommand = new RelayCommand(NavBarButton, _=> IsLogged);
            OpenWindowCommand = new RelayCommand(OpenWindow, _=> IsLogged);

            try
            {   
                SetUpMethods.SetUp(); // Perform setup methods for the application
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while running the setup methods.\nIt is recommended to exit the software and restart it.", "Set-Up Error", MessageBoxButton.OK, MessageBoxImage.Error);
                ShowErrorPage(ex);
            }

        }

        // The method is called when the user performs any activity in the UI level of the application.
        private void UserActivity(object? parameter)
        {
            TheCurrentUser?.HandleActivity();
        }


        // Attempts to log in user based on the provided data,
        // If 'IdNumber' is the 'Id' of un-deleted User and 'Password' is its 'Password' property.
        private void LogIn(object? parameter)
        {
            try
            {
                IdNumber = IdNumber.Trim();
                Password = Password.Trim();

                if (!int.TryParse(IdNumber, out int id))
                    MessageBox.Show("User ID must be a number.", "Log In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    User? user = DatabaseMethods.GetObject<User>(id);
                    if (user == null)
                        MessageBox.Show("There is no user with the given ID.", "Log In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (user.Password != Password)
                        MessageBox.Show("The given password is incorrect.", "Log In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else if (user.IsDeleted)
                        MessageBox.Show("The user is deleted and cannot be logged-in.", "Log In Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    else
                    {
                        TheCurrentUser = LogicalTier.CurrentUser.GetInstance(user);
                        if (TheCurrentUser != null)
                        {
                            // event handler when user State changed to 'LoggedOutState', performs inactivity logout.
                            TheCurrentUser.PropertyChanged += (sender, e) =>
                            {
                                if (e.PropertyName == nameof(LogicalTier.CurrentUser.State) && TheCurrentUser?.State is LoggedOutState)
                                    Application.Current.Dispatcher.InvokeAsync(InactivityLogOut);
                            };

                            IsLogged = true;
                            IdNumber = "";
                            Password = "";
                            CurrentPageVM = new HomePageViewModel();
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Logs the user out of the application. 
        private void LogOut(object? parameter)
        {
            foreach (Window window in Application.Current.Windows) // close any secondery window
            {
                if (window != Application.Current.MainWindow)
                    window.Close();
            }
            OpenExpanders = new bool[5]; // closes all the Navigation menu Expenders by setting the IsExpanded property to 'false'
            TheCurrentUser = null;
            IsLogged = false;
            CurrentPageVM = new HomePageViewModel();
        }


        // Handles logout that occur due to user inactivity. displaying a message and calling the LogOut method.
        private void InactivityLogOut()
        {
            // Block all open windows for user interaction until the user closed the MessageBox.
            // If not blocked, the user (in some cases) could ignore the MessageBox and continue working.
            foreach (Window window in Application.Current.Windows)
            {
                window.IsEnabled = false;
            }
            MessageBox.Show("Due to inactivity you were logged out from the system", "Logged Out", MessageBoxButton.OK, MessageBoxImage.Information);
            Application.Current.MainWindow.IsEnabled = true;
            LogOut(null); 
        }


        // Changes CurrentPageVM to new ViewModel acording to the button in the navigation bar that been clicked.
        private void NavBarButton(object? buttonContent) 
        {
            try
            {
                if (buttonContent != null)
                {
                    if (((string)buttonContent).Equals("User Details")) // Prevents the admin user from entering the User Details page
                    {
                        if (TheCurrentUser?.User.Id == LogicalTier.Constants.ADMIN_ID)
                            MessageBox.Show("The current user is Admain User.\nThere is no user details to show.", "No User Details", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        else
                            CurrentPageVM = new UserDetailsViewModel();
                    }
                    else
                    {
                        CurrentPageVM = (string)buttonContent switch
                        {
                            "Home" => new HomePageViewModel(),
                            "Revenues" => new RevenuesReportViewModel(),
                            "User Add" => new UserAddViewModel(),
                            "User List" => new UserListViewModel(),
                            "User Edit" => new UserEditViewModel(null),
                            "Hall Add" => new HallAddViewModel(),
                            "Hall List" => new HallListViewModel(),
                            "Hall Edit" => new HallEditViewModel(null),
                            "Event Add" => new EventAddViewModel(),
                            "Event List" => new EventListViewModel(),
                            "Event Edit" => new EventEditViewModel(null),
                            "Occurrence Add" => new OccurrenceAddViewModel(),
                            "Occurrence List" => new OccurrenceListViewModel(),
                            "Occurrence Edit" => new OccurrenceEditViewModel(null),
                            "Order Add" => new OrderAddViewModel(),
                            "Order List" => new OrderListViewModel(),
                            "Order Edit" => new OrderEditViewModel(null),
                            _ => throw new ArgumentException("Invalid buttonContent value: " + (string)buttonContent)
                        };
                    }
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Open Another window acording to the button in the navigation bar that been clicked.
        private void OpenWindow(object? buttonContent)
        {
            try
            {
                if (buttonContent != null)
                {
                    if (((string)buttonContent).Equals("Event Demand"))
                    {
                        // create the View and ViewModel for setting the Demand of the Event.
                        SetEventDemandViewModel viewModel = new();
                        SetEventDemandView view = new()
                        {
                            DataContext = viewModel
                        };
                        view.ShowDialog();
                    }
                    else if (((string)buttonContent).Equals("Hall Approval"))
                    {
                        // create the View and ViewModel for setting the Hall Approval of the Occurrences.
                        ApproveOccurrencesViewModel viewModel = new();
                        ApproveOccurrencesView view = new()
                        {
                            DataContext = viewModel
                        };
                        view.ShowDialog();
                    }
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Displays an error page with information about the given exception.
        public void ShowErrorPage(Exception ex)
        {
            CurrentPageVM = new ErrorPageViewModel(ex);
        }
    }
}
