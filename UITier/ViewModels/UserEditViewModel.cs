using LogicalTier.DBObjects;
using System;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for editing an existing User objcet from the database. Extends EditViewModelBase<T>.
    internal class UserEditViewModel : EditViewModelBase<User>
    {
        // properties for the data of the to-be edited object

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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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



        public UserEditViewModel(IDBObject? selectedObject) : base(selectedObject)
        {
        }


        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        // If SelectedObject is not User instance then the properties will initialize to default values.
        protected override void PropertiesInitialize()
        {
            if (SelectedObject is User selectedUser) 
            {
                FirstName = selectedUser.FirstName;
                LastName = selectedUser.LastName;
                City = selectedUser.City;
                StreetName = selectedUser.StreetName;
                StreetNumber = selectedUser.StreetNumber.ToString();
                PhoneNumber = selectedUser.PhoneNumber;
                Password = selectedUser.Password;
                UsersPermission = selectedUser.Permissions["Users"];
                HallsPermission = selectedUser.Permissions["Halls"];
                EventsPermission = selectedUser.Permissions["Events"];
                OccurrencesPermission = selectedUser.Permissions["Occurrences"];
                OrdersPermission = selectedUser.Permissions["Orders"];
                RevenuesPermission = selectedUser.Permissions["Revenues"];
            }
            else
            {
                FirstName = LastName = City = StreetName = StreetNumber = PhoneNumber = Password = "";
                UsersPermission = HallsPermission = EventsPermission = OccurrencesPermission = OrdersPermission = RevenuesPermission = false;
            }
            ErrorMessage = "";
        }


        // Edit an existing User objcet from the database with new data that filed by the user.
        // If not all the data is valid, the editing will not be preformed and string with the erros will be stored in 'ErrorMessage'.
        protected override void Edit(object? parameter)
        {
            if (SelectedObject is User selectedUser)
            {
                FirstName = FirstName.Trim();
                LastName = LastName.Trim();
                City = City.Trim();
                StreetName = StreetName.Trim();
                StreetNumber = StreetNumber.Trim();
                PhoneNumber = PhoneNumber.Trim();
                Password = Password.Trim();

                try
                {
                    ErrorMessage = selectedUser.Edit(FirstName, LastName, City, StreetName, StreetNumber, PhoneNumber, Password,
                                                      UsersPermission, HallsPermission, EventsPermission, OccurrencesPermission, OrdersPermission, RevenuesPermission);
                    if (ErrorMessage == "")
                        MessageBox.Show($"User #{selectedUser.Id} edited successfully", "User edited", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if (SelectedObject == null || (string.IsNullOrWhiteSpace(FirstName)) || (string.IsNullOrWhiteSpace(LastName)) || (string.IsNullOrWhiteSpace(City)) || (string.IsNullOrWhiteSpace(StreetNumber)) ||
                (string.IsNullOrWhiteSpace(StreetName)) || (string.IsNullOrWhiteSpace(PhoneNumber)) || (string.IsNullOrWhiteSpace(Password)))
                return false;
            else
                return true;
        }
    }
}
