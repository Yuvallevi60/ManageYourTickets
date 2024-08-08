using LogicalTier.DBObjects;
using System;
using System.Windows;
using static LogicalTier.DBObjects.Event;

namespace UITier.ViewModels
{
    // ViewModel for adding a new Event objcet to the database. Extends AddViewModelBase<T>.
    internal class EventAddViewModel : AddViewModelBase<Event>
    {
        // properties for the input data from the user

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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
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
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private TicketPricingMethod[] _pricingMethodList = (TicketPricingMethod[])Enum.GetValues(typeof(TicketPricingMethod));
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

        private bool _hasBrochure = false;
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

        private bool _hasPoster = false;
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

        private bool _hasShirt = false;
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




        // Add new Event objcet to the database if all the input data is valid,
        // otherwise it will store in 'ErrorMessage'  the errors in the data
        protected override void Add(object? parameter)
        {
            IdNumber = IdNumber.Trim();
            EventName = EventName.Trim();
            TicketPrice = TicketPrice.Trim();

            try
            {
                ErrorMessage = Event.Create(IdNumber, EventName, EventDuration.TimeOfDay, TicketPrice, SelectedMethod, HasBrochure, HasPoster, HasShirt);
                if (ErrorMessage == "")
                {
                    MessageBox.Show($"Event #{IdNumber} created and added to the database", "Event Created", MessageBoxButton.OK, MessageBoxImage.Information);
                    Clear(parameter);
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((string.IsNullOrWhiteSpace(IdNumber)) || (string.IsNullOrWhiteSpace(EventName)) || (EventDuration.TimeOfDay.Equals(TimeSpan.Zero)) || (string.IsNullOrWhiteSpace(TicketPrice)))
                return false;
            else
                return true;
        }


        // Clear all input fields.
        protected override void Clear(object? parameter)
        {
            IdNumber = EventName = TicketPrice = ErrorMessage = "";
            EventDuration = new DateTime();
            SelectedMethod = TicketPricingMethod.Fixed;
            HasBrochure = HasPoster = HasShirt = false;
        }
    }
}
