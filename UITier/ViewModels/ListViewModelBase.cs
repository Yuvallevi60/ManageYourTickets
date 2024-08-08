using LogicalTier;
using LogicalTier.DBObjects;
using LogicalTier.Memento;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using UITier.Views;

namespace UITier.ViewModels
{
    // ListViewModelBase is an abstract base class that extends UserControlViewModelBase and
    // is intended for use with view models responsible for displaying and managing lists of objects from the database.
    abstract class ListViewModelBase<T> : UserControlViewModelBase where T : IDBObject
    {
        // The list that shown in the UI
        private List<T> _objectsList;
        public List<T> ObjectsList
        {
            get
            {
                return _objectsList;
            }
            set
            {
                _objectsList = value;
                ListHeader = $"{typeof(T).Name} List" + (value != null ? $" ({ObjectsList.Count} objects)" : "");
                OnPropertyChanged();
            }
        }

        // string at the head of the View, indicate the type and the count of the objects in the list
        private string _listHeader;
        public string ListHeader
        {
            get
            {
                return _listHeader;
            }
            set
            {
                _listHeader = value;
                OnPropertyChanged();
            }
        }

        // an object from the list that the user selected through the UI
        private T? _selectedObject;
        public T? SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                _selectedObject = value;
                OnPropertyChanged();
                DeleteCommand.RaiseCanExecuteChanged();
                EditCommand.RaiseCanExecuteChanged();
                RestoreCommand.RaiseCanExecuteChanged();
            }
        }

        // indicate if list show object with 'true' value at the prperty 'IsDeleted'
        private bool _showDeleted = false;
        public bool ShowDeleted
        {
            get
            {
                return _showDeleted;
            }
            set
            {
                _showDeleted = value;
                OnPropertyChanged();
                ResetList(null);
            }
        }

        // properties of type 'T' that can be used for filtering the list
        private List<string> _propertiesList;
        public List<string> PropertiesList
        {
            get
            {
                return _propertiesList;
            }
            set
            {
                _propertiesList = value;
                OnPropertyChanged();
            }
        }

        // the selected property to filter the list by
        private string _selectedProperty = "";
        public string SelectedProperty
        {
            get
            {
                return _selectedProperty;
            }
            set
            {
                _selectedProperty = value;
                OnPropertyChanged();
                SetFilterTypesList();
                AddFilterCommand.RaiseCanExecuteChanged();
            }
        }

        // types of filters that user can chose from 
        private List<string> _filterTypesList = new();
        public List<string> FilterTypesList
        {
            get
            {
                return _filterTypesList;
            }
            set
            {
                _filterTypesList = value;
                OnPropertyChanged();
            }
        }

        // selected filter type to filter the list by
        private string _selectedFilterType = "";
        public string SelectedFilterType
        {
            get
            {
                return _selectedFilterType;
            }
            set
            {
                _selectedFilterType = value;
                OnPropertyChanged();
                AddFilterCommand.RaiseCanExecuteChanged();
            }
        }

        // the value to filter the list by
        private string _filterValue = "";
        public string FilterValue
        {
            get
            {
                return _filterValue;
            }
            set
            {
                _filterValue = value;
                OnPropertyChanged();
                AddFilterCommand.RaiseCanExecuteChanged();
            }
        }

        // indicate if the list is filtered
        private bool _isFiltered = false;
        public bool IsFiltered
        {
            get
            {
                return _isFiltered;
            }
            set
            {
                _isFiltered = value;
                OnPropertyChanged();
                UndoFilterCommand.RaiseCanExecuteChanged();
                ResetListCommand.RaiseCanExecuteChanged();
            }
        }

        // stack that used to undo filters on the list. Part of the "Memento" design pattern.
        private Stack<ListMemento> _listFilterHistory = new();


        // Commands for handling user interactions.
        public RelayCommand DeleteCommand { get; set; }
        public RelayCommand RestoreCommand { get; set; }
        public RelayCommand EditCommand { get; set; }
        public RelayCommand PrintCommand { get; set; }
        public RelayCommand AddFilterCommand { get; set; }
        public RelayCommand UndoFilterCommand { get; set; }
        public RelayCommand ResetListCommand { get; set; }
        public RelayCommand SortListCommand { get; set; }


        public ListViewModelBase()
        {
            _propertiesList = GetPropertyList();
            _objectsList = GetObjectsList();
            _listHeader = $"{typeof(T).Name} List ({ObjectsList.Count} objects)";

            DeleteCommand = new RelayCommand(Delete, (_ => SelectedObject != null && !SelectedObject.IsDeleted));
            RestoreCommand = new RelayCommand(Restore, (_ => SelectedObject != null && SelectedObject.IsDeleted));
            EditCommand = new RelayCommand(GoToEditPage, (_ => SelectedObject != null && !SelectedObject.IsDeleted));
            PrintCommand = new RelayCommand(OpenPrintWindow);
            AddFilterCommand = new RelayCommand(AddFilter, (_ => SelectedProperty != "" && SelectedFilterType != "" && !string.IsNullOrWhiteSpace(FilterValue)));
            UndoFilterCommand = new RelayCommand(UndoFilter, (_ => IsFiltered));
            ResetListCommand = new RelayCommand(ResetList);
            SortListCommand = new RelayCommand(SortList);
        }



        // Gets the objects list from the database.
        private List<T> GetObjectsList()
        {
            List<T> list = DatabaseMethods.GetList<T>(ShowDeleted);
            return list;
        }


        // Deletes 'SelectedObject' if it is deletable.
        protected virtual void Delete(object? parameter)
        {
            if (SelectedObject != null)
            {
                string messageCaption = $"Delete {SelectedObject.GetType().Name}";
                string messageText = $"Do you want to delete {SelectedObject.GetType().Name} #{SelectedObject.Id}?";

                MessageBoxResult result = MessageBox.Show(messageText, messageCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        string isDeleteable = SelectedObject.IsChangeable();
                        if (string.IsNullOrEmpty(isDeleteable)) // empty string is return when the object is deleteable
                        {
                            SelectedObject.Delete();
                            ResetList(null); //To update what shown in the DataGrid 
                        }
                        else
                            MessageBox.Show(isDeleteable, messageCaption + " Falied", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (Exception ex) { ErrorHandler(ex); }
                }
            }
        }


        // Restores 'SelectedObject' if applicable.
        private void Restore(object? parameter)
        {
            if (SelectedObject != null)
            {
                string messageCaption = $"Restore {SelectedObject.GetType().Name}";
                string messageText = $"Do you want to restore {SelectedObject.GetType().Name} #{SelectedObject.Id}?";

                MessageBoxResult result = MessageBox.Show(messageText, messageCaption, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        IDBObject.Restore(SelectedObject);
                        ResetList(null); //To update what shown in the DataGrid
                    }
                    catch (Exception ex) { ErrorHandler(ex); }
                }
            }
        }


        // changes the main view to the edit page for the 'SelectedObject'.
        // If the 'SelectedObject' cannot be edited it shows error message.
        private void GoToEditPage(object? parameter)
        {
            if (SelectedObject != null)
            {
                try
                {
                    // Check if the selected object can be edited and get error message if it cannot.
                    string checkResult = SelectedObject.IsChangeable();
                    if (string.IsNullOrEmpty(checkResult)) // Editable
                    {
                        // Set the current panel view model in the main window to the edit view model of the object's type.
                        MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)Application.Current.MainWindow.DataContext;
                        mainWindowViewModel.CurrentPageVM = SelectedObject.GetType().Name switch
                        {
                            "User" => new UserEditViewModel(SelectedObject),
                            "Event" => new EventEditViewModel(SelectedObject),
                            "Hall" => new HallEditViewModel(SelectedObject),
                            "Occurrence" => new OccurrenceEditViewModel(SelectedObject),
                            "Order" => new OrderEditViewModel(SelectedObject),
                            _ => throw new Exception($"'SelectedObject' type is {SelectedObject.GetType().Name}. \nThis type does not have edit page.")
                        };
                    }
                    else  // Uneditable. Show an error message.
                        MessageBox.Show(checkResult, "Cannot Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // The Method open new 'PrintedObjectListView' object and there it prints all the objects that are currently showing in the list.
        private void OpenPrintWindow(object? parameter)
        {
            try
            {
                if (ObjectsList.Count == 0)
                    MessageBox.Show("The list is empty.", "Print Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    PrintedObjectListViewModel<T> viewModel = new(ObjectsList);
                    PrintedObjectListView view = new()
                    {
                        DataContext = viewModel
                    };
                    view.Show();
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // The method sorts ObjectsList according to the DataGrid's sorting performed in the View
        // (sorting the DataGrid in the View does not sort the 'ItemsSource') 
        private void SortList(object? parameter)
        {
            try
            {
                if (parameter is DataGridColumn column) // The DataGrid's column that his header had been clickd in the View
                {
                    string propertyName = column.SortMemberPath; // the property that bind to the column
                    if (!string.IsNullOrWhiteSpace(propertyName))
                    {
                        PropertyInfo property = typeof(T).GetProperty(propertyName) ?? throw new ArgumentException($"Property '{propertyName}' does not exist in the {typeof(T).Name} class");

                        // SortDirection is the currnet sort direction, before the list was re-sorted (before the column's header click).
                        // So if SortDirection is 'Descending' it means that the new sort direction (after the column's header click) will be 'Ascending' and via versa. 
                        // And if SortDirection is null it means that currnetly the list is not sorted by 'column' and the new sort direction will be 'Ascending' by default.
                        if (column.SortDirection == ListSortDirection.Descending || column.SortDirection == null)
                        {
                            ObjectsList.Sort((x, y) => Comparer.Default.Compare(property.GetValue(x), property.GetValue(y))); // Ascending sort 
                        }
                        else if (column.SortDirection == ListSortDirection.Ascending)
                        {
                            ObjectsList.Sort((x, y) => Comparer.Default.Compare(property.GetValue(y), property.GetValue(x))); // Descending sort
                        }
                    }
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Method to apply filtering to the list of objects based on user-selected filter criteria.
        // If the list has changed, create memnto of the previous sate of the list and the applied filter.
        private void AddFilter(object? parameter)
        {
            try
            {
                FilterValue = FilterValue.Trim();

                PropertyInfo propertyInfo = typeof(T).GetProperty(SelectedProperty) ?? throw new ArgumentException($"The property '{SelectedProperty}' does not exist in the {typeof(T).Name} class.");

                List<T> filteredList = new();

                bool addFlag; // flag to indicate if the object meets the condition of the filter.

                foreach (T anObject in ObjectsList)
                {
                    addFlag = false;
                    
                    var propertyValue = propertyInfo.GetValue(anObject);
                  
                    if (propertyValue is int intValue && int.TryParse(FilterValue, out _)) // int property
                    {
                        int intFilterValue = int.Parse(FilterValue);

                        // Check if 'intValue' (the value from the object) meets the conditions to the filter based on the 'SelectedFilterType' and 'intFilterValue'.
                        addFlag = SelectedFilterType switch
                        {
                            "==" => intValue == intFilterValue,
                            "!=" => intValue != intFilterValue,
                            "<" => intValue < intFilterValue,
                            "<=" => intValue <= intFilterValue,
                            ">" => intValue > intFilterValue,
                            ">=" => intValue >= intFilterValue,
                            _ => throw new ArgumentException($"Failed to add filter.\n{SelectedFilterType} is unsupported filter type for integers."),
                        };
                    }
                    else if (propertyValue is string stringValue && stringValue != null) // string property
                    {
                        // Check if 'stringValue' (the value from the object) meets the conditions to the filter based on the 'SelectedFilterType' and 'FilterValue'.
                        addFlag = SelectedFilterType switch
                        {
                            "Equal" => stringValue == FilterValue,
                            "Including" => stringValue.Contains(FilterValue),
                            "Excluding" => !stringValue.Contains(FilterValue),
                            _ => throw new ArgumentException($"Failed to add filter.\n{SelectedFilterType} is unsupported filter type for strings."),
                        };
                    }
                    if (addFlag)
                        filteredList.Add(anObject);
                }


                if (filteredList.Count == ObjectsList.Count) // If the new filter did not change the list of displayed objects.
                {
                    MessageBox.Show("The added filter did not change the list of displayed objects.", "No Changes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (!filteredList.Any()) // If no objects match the new filter.
                        MessageBox.Show("There are no objects in the database that match all of the filters that have been added.", "No Results", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Create a memento to store the original list of object IDs and the applied filter.
                    List<int> OriginalList = ObjectsList.Select(user => user.Id).ToList();
                    ListMemento memento = new(OriginalList, SelectedProperty, SelectedFilterType, FilterValue);
                    _listFilterHistory.Push(memento);

                    // Update the displayed object list, set filtering flags, and clear filter criteria.
                    ObjectsList = filteredList;
                    IsFiltered = true;
                    FilterValue = "";
                    SelectedFilterType = "";
                    SelectedProperty = "";
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Undo the latest filter.
        // restore the list of objects that were shown before it was applied and rewrite this filter seting.
        private void UndoFilter(object? parameter)
        {
            try
            {
                FilterValue = "";
                SelectedFilterType = "";
                SelectedProperty = "";

                if (_listFilterHistory.Count > 0)
                {
                    ListMemento memento = _listFilterHistory.Pop();

                    if (PropertiesList.Contains(memento.FilterProperty))  // restoring the setting of the last filter
                    {
                        SelectedProperty = memento.FilterProperty;
                        if (FilterTypesList.Contains(memento.FilterType))
                        {
                            SelectedFilterType = memento.FilterType;
                            FilterValue = memento.FilterValue;
                        }
                    }

                    // list of object IDs from the filter history. Those IDs are of the objects that were in the list before the last filter.
                    List<int> RestoredIds = memento.IdList;
                    ObjectsList = GetObjectsList().Where(obj => RestoredIds.Contains(obj.Id)).ToList();

                    IsFiltered = (_listFilterHistory.Count > 0);
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Resets all filters, restoring the original 'ObjectsList' and clear the filters history.
        private void ResetList(object? parameter)
        {
            _listFilterHistory.Clear();
            IsFiltered = false;
            SelectedFilterType = "";
            SelectedProperty = "";
            FilterValue = "";
            try
            {
                ObjectsList = GetObjectsList();
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Returns a list of the properties's names of type T class that are valid for filtering the list.
        private static List<string> GetPropertyList()
        {
            List<string> propertyNames = new() { "" }; // empty string is add to the list so an empty option could be shown in the View

            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo property in properties) // Add properties names of type 'int' or 'string' only.
            {
                Type propertyType = property.PropertyType;
                if (propertyType == typeof(int) || propertyType == typeof(string))
                    propertyNames.Add(property.Name);
            }
            return propertyNames;
        }


        // Sets the available filter types based on the selected property's data type.
        private void SetFilterTypesList()
        {
            SelectedFilterType = "";

            PropertyInfo? property = typeof(T).GetProperty(SelectedProperty, BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                FilterTypesList = new List<string>() { "" }; // empty string is add to the list so an empty option could be shown in the View
            else
            {
                Type propertyType = property.PropertyType;

                if (propertyType == typeof(string))
                    FilterTypesList = new List<string> { "", "Equal", "Including", "Excluding" };

                else if (propertyType == typeof(int))
                    FilterTypesList = new List<string> { "", "==", "!=", "<", "<=", ">", ">=" };
            }
        }
    }
}
