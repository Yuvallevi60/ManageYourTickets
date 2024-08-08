using LogicalTier.DBObjects;
using System;
using System.Windows;
using static LogicalTier.DBObjects.Hall;

namespace UITier.ViewModels
{
    // ViewModel for adding a new Hall objcet to the database. Extends AddViewModelBase<T>.
    class HallAddViewModel : AddViewModelBase<Hall>
    {
        // properties for the input data from the user

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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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


        public HallAddViewModel()
        {
            _hallTypesList = (HallType[])Enum.GetValues(typeof(HallType));
        }


        // Add new Hall objcet to the database if all the data is valid,
        // otherwise it will store in 'ErrorMessage'  the errors in the data
        protected override void Add(object? parameter)
        {
            IdNumber = IdNumber.Trim();
            HallName = HallName.Trim();
            LinesNumber = LinesNumber.Trim();
            SeatsInLine = SeatsInLine.Trim();
            City = City.Trim();
            StreetName = StreetName.Trim();
            StreetNumber = StreetNumber.Trim();

            try
            {
                ErrorMessage = Hall.Create(IdNumber, HallName, SelectedType, LinesNumber, SeatsInLine, City, StreetName, StreetNumber,
                                                                OpeningHour.TimeOfDay, ClosingHour.TimeOfDay, HasParking, HasCafeteria, HasRestroom);
                if (ErrorMessage == "")
                {
                    MessageBox.Show($"Hall #{IdNumber} created and added to the database", "Hall Created", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear(parameter);
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((string.IsNullOrWhiteSpace(IdNumber)) || (string.IsNullOrWhiteSpace(HallName)) || (string.IsNullOrWhiteSpace(City)) || (string.IsNullOrWhiteSpace(StreetName)) || 
                (string.IsNullOrWhiteSpace(StreetNumber)) || (string.IsNullOrWhiteSpace(LinesNumber)) || (string.IsNullOrWhiteSpace(SeatsInLine)))
                return false;
            else
                return true;
        }


        // Clear all input fields.
        protected override void Clear(object? parameter)
        {
            IdNumber = HallName = City = StreetName = StreetNumber = LinesNumber = SeatsInLine = ErrorMessage = "";
            SelectedType = HallType.Theatre;
            ClosingHour = OpeningHour = DateTime.MinValue;
            HasParking = HasCafeteria = HasRestroom = false;
        }
    }
}
