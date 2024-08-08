using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for adding a new Occurrence objcet to the database. Extends AddViewModelBase<T>.
    class OccurrenceAddViewModel : AddViewModelBase<Occurrence>
    {
        // properties for the input data from the user

        private List<Event>? _eventsList;
        public  List<Event>? EventsList
        {
            get
            {
                return _eventsList;
            }
            set 
            { 
                _eventsList = value;
                OnPropertyChanged();
            }
        }

        private Event? _selectedEvent;
        public Event? SelectedEvent
        {
            get
            {
                return _selectedEvent;
            }
            set
            {
                _selectedEvent = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private List<Hall>? _hallsList;
        public List<Hall>? HallsList
        {
            get
            {
                return _hallsList;
            }
            set
            {
                _hallsList = value;
                OnPropertyChanged();
            }
        }

        private Hall? _selectedHall;
        public Hall? SelectedHall
        {
            get
            {
                return _selectedHall;
            }
            set
            {
                _selectedHall = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime? _selectedDate;
        public DateTime? SelectedDate
        {
            get
            {
                return _selectedDate;
            }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _selectedHour;
        public DateTime SelectedHour
        {
            get
            {
                return _selectedHour;
            }
            set
            {
                _selectedHour = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }


        public OccurrenceAddViewModel()
        {
            EventsList = LogicalTier.DatabaseMethods.GetList<Event>().Where(e => e.Demand > 0).ToList(); // all the Events with demands. 
            HallsList = LogicalTier.DatabaseMethods.GetList<Hall>();
            _selectedHour = DateTime.MinValue;
        }


        // Add new Occurrence objcet to the database if all the data is valid,
        // otherwise it will store in 'ErrorMessage' the errors in the data
        protected override void Add(object? parameter)
        {
            if ((SelectedEvent != null) && (SelectedHall != null) && (SelectedDate != null))
            {
                IdNumber = IdNumber.Trim();

                try
                {
                    ErrorMessage = Occurrence.Create(IdNumber, SelectedEvent, SelectedHall, DateOnly.FromDateTime((DateTime)SelectedDate), SelectedHour.TimeOfDay);
                    if (ErrorMessage == "")
                    {
                        MessageBox.Show($"Occurrence #{IdNumber} created and added to the database", "Occurrence Created", MessageBoxButton.OK, MessageBoxImage.Information);
                        EventsList = LogicalTier.DatabaseMethods.GetList<Event>().Where(e => e.Demand > 0).ToList(); // update becuase the change in the demand.
                        Clear(parameter);
                    }
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if (string.IsNullOrWhiteSpace(IdNumber) || (SelectedEvent == null) || (SelectedHall == null) || (SelectedDate == null))
                return false;
            else
                return true;
        }


        // Clear all input fields.
        protected override void Clear(object? parameter)
        {
            IdNumber = "";
            SelectedHall = null;
            SelectedEvent = null;
            SelectedHour = DateTime.MinValue;
            SelectedDate = null;
        }
    }
}
