using LogicalTier;
using LogicalTier.DBObjects;
using System;
using System.Windows;
using UITier.Views;

namespace UITier.ViewModels
{
    // EditViewModelBase is an abstract base class that extends UserControlViewModelBase
    // designed for ViewModels responsible for editing objects from the database.
    abstract class EditViewModelBase<T> : UserControlViewModelBase where T : class, IDBObject
    {
        private IDBObject? _selectedObject; // The object from the data base that the user want to edit
        public IDBObject? SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
                ResetCommand.RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage = ""; // Message that shows the errors, if there are, in the data of the object the user wants to edit.
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
        public RelayCommand EnterIdCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand ResetCommand { get; }



        public EditViewModelBase(IDBObject? selectedObject)
        {
            EnterIdCommand = new RelayCommand(EnterId);
            EditCommand = new RelayCommand(Edit, IsAllDataFilled);
            ResetCommand = new RelayCommand(Reset, (_ => SelectedObject != null));

            SelectedObject = selectedObject;
            PropertiesInitialize();
        }


        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        protected abstract void PropertiesInitialize();

        // Method for performing the edit operation and save SelectedObject to the database with the new data.
        protected abstract void Edit(object? parameter);

        // Method to check if all required data is filled before performing the edit operation.
        protected abstract bool IsAllDataFilled(object? parameter);


        // Method to undo all the changes the user did in the form and fill the input fields with the 'SelectedObject' data.
        protected void Reset(object? parameter)
        {
            try 
            { 
                PropertiesInitialize(); 
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // The method use 'EnterIdView' to ask from the user the ID of the type 'T' object he wants to edit.
        // If the database contain object with the given ID it will retrieve the object, and update the SelectedObject property.
        private void EnterId(object? parameter)
        {
            // create the View and ViewModel for getting the ID from the user.
            EnterIdViewModel enterIdViewModel = new();
            EnterIdView enterIdView = new()
            {
                DataContext = enterIdViewModel
            };

            bool? dialogResult = enterIdView.ShowDialog();
            if (dialogResult == true) // the user enter string (not necessarily valid).
            {
                try  // Attempt to retrieve a T-type object using the entered ID.
                {
                    SelectedObject = null;
                    if (!int.TryParse(enterIdViewModel.InputId, out int idNumber)) // check if the user input is valid int
                    {
                        MessageBox.Show("Id number must contain only digits.", "Cannot Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        T? anObject = DatabaseMethods.GetObject<T>(idNumber); // try to get the object from the db.
                        if (anObject == null)
                        {
                            MessageBox.Show($"There is no {typeof(T).Name} with ID {idNumber} in the database.", "Cannot Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            string resultString = anObject.IsChangeable();
                            if (string.IsNullOrEmpty(resultString)) // editable
                                SelectedObject = anObject;
                            else
                                MessageBox.Show(resultString, "Cannot Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    PropertiesInitialize();
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }
    }
}
