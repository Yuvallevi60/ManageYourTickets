using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for editing an existing Occurrence objcet from the database. Extends EditViewModelBase<T>.
    internal class OccurrenceEditViewModel : EditViewModelBase<Occurrence>
    {
        // properties for the data of the to-be edited object

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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
            }
        }

        public OccurrenceEditViewModel(IDBObject? selectedObject) : base(selectedObject)
        {
        }


        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        // If SelectedObject is not Occurrence instance then the properties will initialize to default values.
        protected override void PropertiesInitialize()
        {
            ErrorMessage = "";
            if (SelectedObject is Occurrence selectedOccurrence)
            {
                SelectedDate = selectedOccurrence.Date.ToDateTime(TimeOnly.Parse("10:00 PM"));
                SelectedHour = new DateTime(selectedOccurrence.Hour.Ticks);
                HallsList = LogicalTier.DatabaseMethods.GetList<Hall>();
                SelectedHall = HallsList.FirstOrDefault(h => h.Id == selectedOccurrence.HallId) ?? throw new Exception("Hall not found for ID:" + selectedOccurrence.HallId);
            }
            else
            {
                SelectedHall = null;
                SelectedDate = null;
                SelectedHour = DateTime.MinValue;
            }
        }


        // Edit an existing Occurrence objcet from the database with new data that filed by the user.
        // If not all the data is valid, the editing will not be preformed and string with the erros will be stored in 'ErrorMessage'.
        protected override void Edit(object? parameter)
        {
            if ((SelectedObject is Occurrence selectedOccurrence) && (SelectedHall != null) && (SelectedDate != null))
            {
                try
                {
                    Event SelectedEvent = LogicalTier.DatabaseMethods.GetObject<Event>(selectedOccurrence.EventId) ?? throw new Exception("Event not found for ID:" + selectedOccurrence.EventId);

                    ErrorMessage = selectedOccurrence.Edit(SelectedEvent, SelectedHall, DateOnly.FromDateTime((DateTime)SelectedDate), SelectedHour.TimeOfDay);
                    if (ErrorMessage == "")
                        MessageBox.Show($"Occurrence #{SelectedObject.Id} edited successfully", "Occurrence edited", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            } 
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((SelectedObject == null) || (SelectedHall == null) || (SelectedDate == null))
                return false;
            else
                return true;
        }
    }
}
