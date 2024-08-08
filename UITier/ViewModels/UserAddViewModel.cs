using LogicalTier.DBObjects;
using System;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for adding a new User objcet to the database. Extends AddViewModelBase<T>.
    internal class UserAddViewModel : AddViewModelBase<User>
    {
        // properties for the input data from the user

        private string _firstName = "";
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _lastName = "";
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _city = "";
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _streetName = "";
        public string StreetName
        {
            get
            {
                return _streetName;
            }
            set
            {
                _streetName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _streetNumber = "";
        public string StreetNumber
        {
            get
            {
                return _streetNumber;
            }
            set
            {
                _streetNumber = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _phoneNumber = "";
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _password = "";
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
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _usersPermission;
        public bool UsersPermission
        {
            get
            {
                return _usersPermission;
            }
            set
            {
                _usersPermission = value;
                OnPropertyChanged();
            }
        }

        private bool _hallsPermission;
        public bool HallsPermission
        {
            get
            {
                return _hallsPermission;
            }
            set
            {
                _hallsPermission = value;
                OnPropertyChanged();
            }
        }

        private bool _eventsPermission;
        public bool EventsPermission
        {
            get
            {
                return _eventsPermission;
            }
            set
            {
                _eventsPermission = value;
                OnPropertyChanged();
            }
        }

        private bool _occurrencesPermission;
        public bool OccurrencesPermission
        {
            get
            {
                return _occurrencesPermission;
            }
            set
            {
                _occurrencesPermission = value;
                OnPropertyChanged();
            }
        }

        private bool _ordersPermission;
        public bool OrdersPermission
        {
            get
            {
                return _ordersPermission;
            }
            set
            {
                _ordersPermission = value;
                OnPropertyChanged();
            }
        }

        private bool _revenuesPermission;
        public bool RevenuesPermission
        {
            get
            {
                return _revenuesPermission;
            }
            set
            {
                _revenuesPermission = value;
                OnPropertyChanged();
            }
        }




        // Add new User objcet to the database if all the data is valid,
        // otherwise it will store in 'ErrorMessage'  the errors in the data
        protected override void Add (object? parameter)
        {
            IdNumber = IdNumber.Trim();
            FirstName = FirstName.Trim();
            LastName = LastName.Trim();
            City = City.Trim();
            StreetName = StreetName.Trim();
            StreetNumber = StreetNumber.Trim();
            PhoneNumber = PhoneNumber.Trim();
            Password = Password.Trim();

            try
            {
                ErrorMessage = User.Create(IdNumber, FirstName, LastName, City, StreetName, StreetNumber, PhoneNumber, Password,
                                                        UsersPermission, HallsPermission, EventsPermission, OccurrencesPermission, OrdersPermission, RevenuesPermission);
                if (ErrorMessage == "")
                {
                    MessageBox.Show($"User #{IdNumber} created and added to the database", "User Created", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear(parameter);
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((string.IsNullOrWhiteSpace(IdNumber)) || (string.IsNullOrWhiteSpace(FirstName)) || (string.IsNullOrWhiteSpace(LastName)) || (string.IsNullOrWhiteSpace(City)) || (string.IsNullOrWhiteSpace(StreetNumber)) ||
                (string.IsNullOrWhiteSpace(StreetName)) || (string.IsNullOrWhiteSpace(PhoneNumber)) || (string.IsNullOrWhiteSpace(Password)))
                return false;
            else
                return true;
        }


        // Clear all input fields.
        protected override void Clear(object? parameter)
        {
            IdNumber = FirstName = LastName = City = StreetName = StreetNumber = PhoneNumber = Password = ErrorMessage = "";
            UsersPermission = HallsPermission = EventsPermission = OccurrencesPermission = OrdersPermission = RevenuesPermission = false;
        }
    }
}
