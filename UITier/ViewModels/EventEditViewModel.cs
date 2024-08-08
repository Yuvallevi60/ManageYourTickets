using LogicalTier.DBObjects;
using System;
using System.Windows;
using static LogicalTier.DBObjects.Event;

namespace UITier.ViewModels
{
    // ViewModel for editing an existing Event objcet from the database. Extends EditViewModelBase<T>.
    internal class EventEditViewModel : EditViewModelBase<Event>
    {
        // properties for the data of the to-be edited object

        private string _eventName = "";
        public string EventName
        {
            get
            {
                return _eventName;
            }
            set
            {
                _eventName = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime _eventDuration;
        public DateTime EventDuration
        {
            get
            {
                return _eventDuration;
            }
            set
            {
                _eventDuration = value;
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private string _ticketPrice = "";
        public string TicketPrice
        {
            get
            {
                return _ticketPrice;
            }
            set
            {
                _ticketPrice = value;                
                OnPropertyChanged();
                EditCommand.RaiseCanExecuteChanged();
            }
        }

        private TicketPricingMethod[] _pricingMethodList;
        public TicketPricingMethod[] PricingMethodList
        {
            get
            {
                return _pricingMethodList;
            }
            set
            {
                _pricingMethodList = value;
                OnPropertyChanged();
            }
        }

        private TicketPricingMethod _selectedMethod;
        public TicketPricingMethod SelectedMethod
        {
            get
            {
                return _selectedMethod;
            }
            set
            {
                _selectedMethod = value;
                OnPropertyChanged();
            }
        }

        private bool _hasBrochure;
        public bool HasBrochure
        {
            get
            {
                return _hasBrochure;
            }
            set
            {
                _hasBrochure = value;
                OnPropertyChanged();
            }
        }

        private bool _hasPoster;
        public bool HasPoster
        {
            get
            {
                return _hasPoster;
            }
            set
            {
                _hasPoster = value;
                OnPropertyChanged();
            }
        }

        private bool _hasShirt;
        public bool HasShirt
        {
            get
            {
                return _hasShirt;
            }
            set
            {
                _hasShirt = value;
                OnPropertyChanged();
            }
        }



        public EventEditViewModel(IDBObject? selectedObject) : base(selectedObject)
        {
            _pricingMethodList = (TicketPricingMethod[])Enum.GetValues(typeof(TicketPricingMethod));
        }


        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        // If SelectedObject is not Event instance then the properties will initialize to default values.
        protected override void PropertiesInitialize()
        {
            ErrorMessage = "";
            if (SelectedObject is Event selectedEvent)
            {
                EventName = selectedEvent.Name;
                EventDuration = new DateTime(1, 1, 1, selectedEvent.EventDuration.Hours, selectedEvent.EventDuration.Minutes, 0);
                TicketPrice = selectedEvent.TicketPrice.ToString();
                HasBrochure = selectedEvent.Merchandise["Brochure"];
                HasPoster = selectedEvent.Merchandise["Poster"];
                HasShirt = selectedEvent.Merchandise["Shirt"];
                SelectedMethod = selectedEvent.PricingMethod;
            }
            else
            {
                EventName = TicketPrice = "";
                EventDuration = new DateTime();
                SelectedMethod = TicketPricingMethod.Fixed;
                HasBrochure = HasPoster = HasShirt = false;
            }
        }


        // Edit an existing Event objcet from the database with new data that filed by the user.
        // If not all the data is valid, the editing will not be preformed and string with the erros will be stored in 'ErrorMessage'.
        protected override void Edit(object? parameter)
        {
            if (SelectedObject is Event selectedEvent)
            {
                EventName = EventName.Trim();
                TicketPrice = TicketPrice.Trim();
                try
                {
                    ErrorMessage = selectedEvent.Edit(EventName, EventDuration.TimeOfDay, TicketPrice, SelectedMethod, HasBrochure, HasPoster, HasShirt);
                    if (ErrorMessage == "")
                        MessageBox.Show($"Event #{selectedEvent.Id} edited successfully", "Event edited", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if (SelectedObject == null || (string.IsNullOrWhiteSpace(EventName)) || (EventDuration.TimeOfDay.Equals(TimeSpan.Zero)) || (string.IsNullOrWhiteSpace(TicketPrice)))
                return false;
            else
                return true;
        }
    }
}
