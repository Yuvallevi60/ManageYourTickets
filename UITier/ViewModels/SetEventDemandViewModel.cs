using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;

namespace UITier.ViewModels
{
    // This ViewModel class is used for displaying a Window to set the Demand value of Event objects.
    internal class SetEventDemandViewModel: WindowViewModelBase
    {
        private List<Event>? _eventsList;
        public List<Event>? EventsList
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
                if (value != null) 
                    Demand = value.Demand;
                OnPropertyChanged();
                SetDemandCommand.RaiseCanExecuteChanged();
            }
        }

        private int _demand;
        public int Demand
        {
            get
            {
                return _demand;
            }
            set
            {
                _demand = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand SetDemandCommand { get; }



        public SetEventDemandViewModel()
        {
            _eventsList = LogicalTier.DatabaseMethods.GetList<Event>();
            SetDemandCommand = new RelayCommand(SetDemand, (_ => SelectedEvent!= null));
        }


        // Update in the database the SelectedEvent with the new Demand value
        private void SetDemand(object? parameter)
        {
            try
            {
                SelectedEvent?.UpdateDemand(Demand);
                SelectedEvent = null;
                Demand = 0;
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }
    }
}
