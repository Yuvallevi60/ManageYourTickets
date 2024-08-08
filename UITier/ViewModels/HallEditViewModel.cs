using LogicalTier.DBObjects;
using System;
using System.Windows;
using static LogicalTier.DBObjects.Hall;

namespace UITier.ViewModels
{
    // ViewModel for editing an existing Hall objcet from the database. Extends EditViewModelBase<T>.
    internal class HallEditViewModel: EditViewModelBase<Hall>
    {
        // properties for the data of the to-be edited object

        private string _hallName = "";
        public string HallName
        {
            get
            {
                return _hallName;
            }
            set
            {
                _hallName = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private HallType[] _hallTypesList;
        public HallType[] HallTypesList
        {
            get
            {
                return _hallTypesList;
            }
            set
            {
                _hallTypesList = value;
                OnPropertyChanged();
            }
        }

        private HallType _selectedType;
        public HallType SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged();
            }
        }

        private string _linesNumber = "";
        public string LinesNumber
        {
            get
            {
                return _linesNumber;
            }
            set
            {
                _linesNumber = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private string _seatsInLine = "";
        public string SeatsInLine
        {
            get
            {
                return _seatsInLine;
            }
            set
            {
                _seatsInLine = value;
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

        private DateTime _openingHour;
        public DateTime OpeningHour
        {
            get
            {
                return _openingHour;
            }
            set
            {
                _openingHour = value;
                OnPropertyChanged();
            }
        }

        private DateTime _closingHour;
        public DateTime ClosingHour
        {
            get
            {
                return _closingHour;
            }
            set
            {
                _closingHour = value;
                OnPropertyChanged();
            }
        }

        private bool _hasParking;
        public bool HasParking
        {
            get
            {
                return _hasParking;
            }
            set
            {
                _hasParking = value;
                OnPropertyChanged();
            }
        }

        private bool _hasCafeteria;
        public bool HasCafeteria
        {
            get
            {
                return _hasCafeteria;
            }
            set
            {
                _hasCafeteria = value;
                OnPropertyChanged();
            }
        }

        private bool _hasRestroom;
        public bool HasRestroom
        {
            get
            {
                return _hasRestroom;
            }
            set
            {
                _hasRestroom = value;
                OnPropertyChanged();
            }
        }



        public HallEditViewModel(IDBObject? selectedObject) : base(selectedObject)
        {
            _hallTypesList = (HallType[])Enum.GetValues(typeof(HallType));
        }



        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        // If SelectedObject is not Hall instance then the properties will initialize to default values.
        protected override void PropertiesInitialize()
        {
            ErrorMessage = "";
            if (SelectedObject is Hall selectedHall)
            {
                HallName = selectedHall.Name;
                SelectedType = selectedHall.Type;
                LinesNumber = selectedHall.Lines.ToString();
                SeatsInLine = selectedHall.SeatsInLine.ToString();
                City = selectedHall.City;
                StreetName = selectedHall.StreetName;
                StreetNumber = selectedHall.StreetNumber.ToString();
                ClosingHour = new DateTime(1, 1, 1) + selectedHall.ClosingHour;
                OpeningHour = new DateTime(1, 1, 1) + selectedHall.OpeningHour;
                HasParking = selectedHall.Facilities["Parking"];
                HasCafeteria = selectedHall.Facilities["Cafeteria"];
                HasRestroom = selectedHall.Facilities["Restroom"];
            }
            else
            {
                HallName = City = StreetName = StreetNumber = LinesNumber = SeatsInLine = "";
                SelectedType = HallType.Theatre;
                ClosingHour = OpeningHour = DateTime.MinValue;
                HasParking = HasCafeteria = HasRestroom = false;
            }
        }


        // Edit an existing Hall objcet from the database with new data that filed by the user.
        // If not all the data is valid, the editing will not be preformed and string with the erros will be stored in 'ErrorMessage'.
        protected override void Edit(object? parameter)
        {
            if (SelectedObject is Hall selectedHall)
            {
                HallName = HallName.Trim();
                LinesNumber = LinesNumber.Trim();
                SeatsInLine = SeatsInLine.Trim();
                City = City.Trim();
                StreetName = StreetName.Trim();
                StreetNumber = StreetNumber.Trim();

                try 
                {
                    ErrorMessage = selectedHall.Edit(HallName, SelectedType, LinesNumber, SeatsInLine, City, StreetName,StreetNumber, 
                                                                    OpeningHour.TimeOfDay, ClosingHour.TimeOfDay, HasParking, HasCafeteria, HasRestroom);
                    if (ErrorMessage == "")
                        MessageBox.Show($"Hall #{selectedHall.Id} edited successfully", "Hall edited", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((SelectedObject == null) || (string.IsNullOrWhiteSpace(HallName)) || (string.IsNullOrWhiteSpace(City)) || (string.IsNullOrWhiteSpace(StreetName)) ||
                (string.IsNullOrWhiteSpace(StreetNumber)) || (string.IsNullOrWhiteSpace(LinesNumber)) || (string.IsNullOrWhiteSpace(SeatsInLine)))
                return false;
            else
                return true;
        }
    }
}
