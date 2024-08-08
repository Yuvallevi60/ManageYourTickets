using LogicalTier.DBObjects;
using System;
using System.Windows;

namespace UITier.ViewModels
{
    // AddViewModelBase is an abstract base class that extends UserControlViewModelBase.
    // It is intended for use with view models responsible for adding objects to the database. 
    abstract class AddViewModelBase<T> : UserControlViewModelBase where T : IDBObject
    {
        private string _idNumber = ""; // the ID of the object the user wants to add. all objects in the database must have ID.
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
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage = ""; // Message that shows the errors, if there are, in the data of the object the user wants to add.
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


        // Commands for handling user interactions.
        public RelayCommand ClearCommand { get; } 
        public RelayCommand AddCommand { get; }
        public RelayCommand RandIdCommand { get; }


        public AddViewModelBase()
        {
            AddCommand = new RelayCommand(Add, IsAllDataFilled);
            ClearCommand = new RelayCommand(Clear);
            RandIdCommand = new RelayCommand(GetRandId);
        }




        // Method to add an object to the database.
        protected abstract void Add(object? parameter);

        // Method to check if all required data is filled before adding an object.
        protected abstract bool IsAllDataFilled(object? parameter);

        // Method to clear input data.
        protected abstract void Clear(object? parameter);


        // Method to generate a random ID for the object type 'T' that the user wante to create and add to the database.
        private void GetRandId(object? parameter)
        {
            try
            {
                int RandomId = LogicalTier.IdGenerator.GenerateAvailableIdNumber<T>(); // return '0' if there is no available Id number. 
                if (RandomId != 0)
                    IdNumber = RandomId.ToString();
                else
                    MessageBox.Show("Avalable Id number cannot be generated.\n The amount of '{typeof(T).Name}' objects in the database has reach the limit.\n"
                        , "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception e) { ErrorHandler(e); }
        }
    }
}
