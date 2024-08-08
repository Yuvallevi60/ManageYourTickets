using LogicalTier.DBObjects;
using LogicalTier.TicketPricing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using UITier.HallMap;
using static LogicalTier.DBObjects.Hall;

namespace UITier.ViewModels
{
    // ViewModel for adding a new Order objcet to the database. Extends AddViewModelBase<T>.
    internal class OrderAddViewModel : AddViewModelBase<Order>
    {
        // properties for the input data from the user

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
                if (SelectedEvent != null)
                {
                    try
                    {
                        var list = LogicalTier.DatabaseMethods.GetListByIntProperty<Occurrence>("EventId", SelectedEvent.Id);
                        OccurrencesList = list.Where(o => o.HallApproval).OrderBy(o => o.Date).ToList();
                    }
                    catch (Exception ex) { ErrorHandler(ex); }
                }
                else
                    OccurrencesList = null;
                OnPropertyChanged();
            }
        }

        private List<Occurrence>? _occurrencesList;
        public List<Occurrence>? OccurrencesList
        {
            get
            {
                return _occurrencesList;
            }
            set
            {
                _occurrencesList = value;
                OnPropertyChanged();
            }
        }

        private Occurrence? _selectedOccurrence;
        public Occurrence? SelectedOccurrence
        {
            get
            {
                return _selectedOccurrence;
            }
            set
            {
                _selectedOccurrence = value;
                OnPropertyChanged();
                SetSeatsSelectionCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _isOccurrenceSelected;
        public bool IsOccurrenceSelected
        {
            get
            {
                return _isOccurrenceSelected;
            }
            set
            {
                _isOccurrenceSelected = value;
                OnPropertyChanged();
            }
        }

        private Grid? _hallMap;
        public Grid? HallMap
        { 
            get 
            { 
                return _hallMap; 
            } 
            set 
            {
                _hallMap = value; 
                OnPropertyChanged(); 
            }
        }

        private Button? _selectedSeatButton;
        public Button? SelectedSeatButton
        {
            get
            {
                return _selectedSeatButton;
            }
            set
            {
                _selectedSeatButton = value;
                OnPropertyChanged();
            }
        }

        private bool _isSeatSelected;
        public bool IsSeatSelected
        { 
            get 
            { 
                return _isSeatSelected;
            }
            set 
            {
                _isSeatSelected = value; 
                OnPropertyChanged();
                OrderReadyCommand.RaiseCanExecuteChanged();
            }
        }

        private List<(int, int)> _selectedSeats;
        public List<(int, int)> SelectedSeats
        {
            get
            {
                return _selectedSeats;
            }
            set
            {
                _selectedSeats = value;
                OnPropertyChanged();
            }
        }

        private int _ticketPrice;
        public int TicketPrice
        {
            get
            {
                return _ticketPrice;
            }
            set
            {
                _ticketPrice = value;
                OnPropertyChanged();
            }
        }

        private int _seatsCount;
        public int SeatsCount
        {
            get
            {
                return _seatsCount;
            }
            set
            {
                _seatsCount = value;
                OnPropertyChanged();
            }
        }

        private int _orderPrice;
        public int OrderPrice
        {
            get
            {
                return _orderPrice;
            }
            set
            {
                _orderPrice = value;
                OnPropertyChanged();
            }
        }

        private bool _isOrderReady;
        public bool IsOrderReady
        {
            get
            {
                return _isOrderReady;
            }
            set
            {
                _isOrderReady = value;
                OnPropertyChanged();
            }
        }

        private string _customerFirstName = "";
        public string CustomerFirstName
        {
            get
            {
                return _customerFirstName;
            }
            set
            {
                _customerFirstName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _customerLastName = "";
        public string CustomerLastName
        {
            get
            {
                return _customerLastName;
            }
            set
            {
                _customerLastName = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _phoneNumber = "";
        public string PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }

        private string _creditCard = "";
        public string CreditCard
        {
            get
            {
                return _creditCard;
            }
            set
            {
                _creditCard = value;
                OnPropertyChanged();
                AddCommand.RaiseCanExecuteChanged();
            }
        }


        public RelayCommand BackCommand { get; }
        public RelayCommand SetSeatsSelectionCommand { get; }
        public RelayCommand OrderReadyCommand { get; }
        

        public OrderAddViewModel()
        {
            EventsList = LogicalTier.DatabaseMethods.GetList<Event>().ToList();
            _selectedSeats = new List<(int, int)>();

            BackCommand = new RelayCommand(Back);
            SetSeatsSelectionCommand = new RelayCommand(SetSeatsSelection, _ => (SelectedEvent != null && SelectedOccurrence != null)) ;
            OrderReadyCommand = new RelayCommand((_ => IsOrderReady = true), (_ => IsSeatSelected));
        }



        // Add new Hall objcet to the database if all the data is valid,
        // otherwise it will store in 'ErrorMessage'  the errors in the data
        protected override void Add(object? parameter)
        {
            if (SelectedOccurrence != null)
            {
                IdNumber = IdNumber.Trim();
                CustomerFirstName = CustomerFirstName.Trim();
                CustomerLastName = CustomerLastName.Trim();
                PhoneNumber = PhoneNumber.Trim();
                CreditCard = CreditCard.Trim();

                try
                {
                    ErrorMessage = Order.Create(IdNumber, SelectedOccurrence.Id, SelectedSeats, OrderPrice, CustomerFirstName, CustomerLastName, PhoneNumber, CreditCard);
                    if (ErrorMessage == "")
                    {
                        MessageBox.Show($"Order #{IdNumber} created and added to the database", "Order Created", MessageBoxButton.OK, MessageBoxImage.Information);

                        // return the ViewModel to its start state
                        CustomerFirstName = CustomerLastName = PhoneNumber = CreditCard = ErrorMessage = IdNumber = "";
                        IsOccurrenceSelected = IsSeatSelected = IsOrderReady = false;
                        SeatsCount = OrderPrice = TicketPrice = 0;
                        SelectedSeats = new List<(int, int)>();
                        HallMap = null;
                        SelectedOccurrence = null;
                        SelectedEvent = null;
                    }
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if ((string.IsNullOrWhiteSpace(IdNumber)) || (SelectedEvent == null) || (SelectedOccurrence == null) || (SelectedSeats.Count == 0) || string.IsNullOrWhiteSpace(CustomerFirstName) ||
                        string.IsNullOrWhiteSpace(CustomerLastName) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(CreditCard))
                return false;
            else
                return true;
        }


        // Clear all input fields of the current step in the order process.
        protected override void Clear(object? parameter)
        {
            if (IsOrderReady) // current step is fill customer data
            {
                CustomerFirstName = CustomerLastName = PhoneNumber = CreditCard = ErrorMessage = IdNumber = "";
            }
            else if (IsOccurrenceSelected) // current step is seats selection
            {
                IsSeatSelected = false;
                SeatsCount = OrderPrice = 0;
                SelectedSeats = new List<(int, int)>();
                try
                {
                    CreateHallMapGrid();
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
            else // current step is event and date selection
            {
                IdNumber = "";
                SelectedOccurrence = null;
                SelectedEvent = null;
            }
        }


        // Reset the input fields when going back to the previous step in the order process.
        private void Back(object? parameter)
        {
            if (IsOrderReady) // going back to seats selection
            {
                CustomerFirstName = CustomerLastName = PhoneNumber = CreditCard = ErrorMessage = "";
                IsOrderReady = false;
            }
            else // going back to event and date selection
            {
                IsSeatSelected = false;
                SeatsCount = OrderPrice = 0;
                SelectedSeats = new List<(int, int)>();
                TicketPrice = 0;
                HallMap = null;
                IsOccurrenceSelected = false;
            }
        }


        // Set the properties needed for the seats selections. Calculate the ticket price and create the hall map for the selected event occurrence.
        private void SetSeatsSelection(object? parameter)
        {
            IsOccurrenceSelected = true;
            try
            {
                SetTicketPrice();
                CreateHallMapGrid();
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        //  sets the price an Occurrence's ticket according to its PricingMethod.
        private void SetTicketPrice()
        {
            if (SelectedEvent != null && SelectedOccurrence != null)
            {
                // Choose the pricing method based on SelectedEvent's TicketPricingMethod.  Part of the "Strategy" design pattern.
                ITicketPricing pricing = SelectedEvent.PricingMethod switch
                {
                    Event.TicketPricingMethod.Fixed => new FixedPricing(),
                    Event.TicketPricingMethod.EarlyBird => new EarlyBirdPricing(),
                    Event.TicketPricingMethod.LastMinute => new LastMinutePricing(),
                    Event.TicketPricingMethod.Availability => new AvailabilityPricing(),
                    Event.TicketPricingMethod.ByDemand => new ByDemandPricing(),
                    _ => throw new Exception("Invalid TicketPricingMethod value: " + SelectedEvent.PricingMethod)
                };
                TicketPrice = pricing.CalculatePrice(SelectedEvent, SelectedOccurrence);
            }
        }


        // Create the Grid representing the map of the selected hall for the chosen occurrence.
        private void CreateHallMapGrid()
        {
            if (SelectedOccurrence != null)
            {
                // Retrieve information about the selected hall.
                Hall selectedHall = LogicalTier.DatabaseMethods.GetObject<Hall>(SelectedOccurrence.HallId) ?? throw new Exception("Hall not found for ID: " + SelectedOccurrence.HallId);
                int lines = selectedHall.Lines;
                int seatsInLine = selectedHall.SeatsInLine;
                bool[,] takenSeats = SelectedOccurrence.GetSeatsMetrix();
                RelayCommand clickSeatCommand = new(ClickSeat);

                // Create the appropriate HallMapGrid object based on the selected hall type. Part of the "Template Method" design pattern.
                HallMapGrid hallMapGrid = selectedHall.Type switch
                {
                    HallType.Theatre => new TheatreMapGrid(),
                    HallType.Traverse => new TraverseMapGrid(),
                    HallType.Thrust => new ThrustMapGrid(),
                    _ => throw new Exception("Invalid HallType value: " + selectedHall.Type)
                };
                HallMap = hallMapGrid.CreateGrid(lines, seatsInLine, takenSeats, clickSeatCommand);
            }
        }


        // Select or deselect seats when a seat button is clicked in the hall map by the user.
        private void ClickSeat(object? parameter)
        {
            if (parameter is Tuple<Button, int, int> buttonTuple)
            {
                int row = buttonTuple.Item2;
                int column = buttonTuple.Item3;

                if (SelectedSeats.Contains((row, column)))
                {
                    buttonTuple.Item1.Background = Brushes.LightBlue;
                    buttonTuple.Item1.Foreground = Brushes.Black;
                    SelectedSeats.Remove((row, column));
                    SeatsCount--;
                }
                else
                {
                    buttonTuple.Item1.Background = Brushes.Blue;
                    buttonTuple.Item1.Foreground = Brushes.White;
                    SelectedSeats.Add((row, column));
                    SeatsCount++;
                }
                IsSeatSelected = (SeatsCount != 0);
                OrderPrice = SeatsCount * TicketPrice;
            }
        }
    }
}
