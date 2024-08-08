using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for editing an existing Order objcet from the database. Extends EditViewModelBase<T>.
    internal class OrderEditViewModel : EditViewModelBase<Order>
    {
        // properties for the data of the to-be edited object

        private Occurrence? _orderOccurrence;
        public Occurrence? OrderOccurrence
        {
            get
            {
                return _orderOccurrence;
            }
            set
            {
                _orderOccurrence = value;
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

        private List<(int, int)> _orderSeats = new();
        public List<(int, int)> OrderSeats
        {
            get
            {
                return _orderSeats;
            }
            set
            {
                _orderSeats = value;
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
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
                EditCommand.RaiseCanExecuteChanged();
            }
        }


        public OrderEditViewModel(IDBObject? selectedObject) : base(selectedObject)
        {
        }


        // Initializing the properties of the ViewModel according to the data of SelectedObject.
        // If SelectedObject is not Order instance then the properties will initialize to default values.
        protected override void PropertiesInitialize()
        {
            ErrorMessage = "";
            if (SelectedObject is Order selectedOrder)
            {
                CustomerFirstName = selectedOrder.CustomerFirstName;
                CustomerLastName = selectedOrder.CustomerLastName;
                PhoneNumber = selectedOrder.PhoneNumber;
                CreditCard = selectedOrder.CreditCard;
                OrderPrice = selectedOrder.Price;
                OrderSeats = selectedOrder.Seats;
                OrderOccurrence = LogicalTier.DatabaseMethods.GetObject<Occurrence>(selectedOrder.OccurrenceId) ?? throw new Exception("Occurrence not found for ID:" + selectedOrder.OccurrenceId);
            }
            else
            {
                CustomerFirstName = CustomerLastName = PhoneNumber = CreditCard = "";
                OrderPrice = 0;
                OrderSeats = new List<(int, int)>();
                OrderOccurrence = null;
            }
        }


        // Edit an existing Order objcet from the database with new data that filed by the user.
        // If not all the data is valid, the editing will not be preformed and string with the erros will be stored in 'ErrorMessage'.
        protected override void Edit(object? parameter)
        {
            if (SelectedObject is Order selectedOrder)
            {
                CustomerFirstName = CustomerFirstName.Trim();
                CustomerLastName = CustomerLastName.Trim();
                PhoneNumber = PhoneNumber.Trim();
                CreditCard = CreditCard.Trim();

                try
                {
                    ErrorMessage = selectedOrder.Edit(CustomerFirstName, CustomerLastName, PhoneNumber, CreditCard);
                    if (ErrorMessage == "")
                        MessageBox.Show($"Order #{selectedOrder.Id} edited successfully", "Order edited", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex) { ErrorHandler(ex); }
            }
        }


        // Check if all required data is filled.
        protected override bool IsAllDataFilled(object? parameter)
        {
            if (SelectedObject == null || string.IsNullOrWhiteSpace(CustomerFirstName) || string.IsNullOrWhiteSpace(CustomerLastName) || string.IsNullOrWhiteSpace(PhoneNumber) || string.IsNullOrWhiteSpace(CreditCard))
                return false;
            else
                return true;
        }
    }
}
