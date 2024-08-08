using LogicalTier;
using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;

namespace UITier.ViewModels
{
    // the viewModel of a view where the user can produce revenue report according to halls, events and dates.
    class RevenuesReportViewModel : UserControlViewModelBase
    {
        private List<Event> _eventsList;
        public List<Event> EventsList
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

        private List<Event> _selectedEvents;
        public List<Event> SelectedEvents
        {
            get
            {
                return _selectedEvents;
            }
            set
            {
                _selectedEvents = value;
                OnPropertyChanged();
                ProduceCommand.RaiseCanExecuteChanged();
            }
        }

        private List<Hall> _hallsList;
        public List<Hall> HallsList
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

        private List<Hall> _selectedHalls;
        public List<Hall> SelectedHalls
        {
            get
            {
                return _selectedHalls;
            }
            set
            {
                _selectedHalls = value;
                OnPropertyChanged();
                ProduceCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime? _selectedStartDate;
        public DateTime? SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }
            set
            {
                _selectedStartDate = value;
                OnPropertyChanged();
                ProduceCommand.RaiseCanExecuteChanged();
            }
        }

        private DateTime? _selectedEndDate;
        public DateTime? SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }
            set
            {
                _selectedEndDate = value;
                OnPropertyChanged();
                ProduceCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _showByEvents;
        public bool ShowByEvents
        {
            get
            { 
                return _showByEvents; 
            }
            set 
            {
                _showByEvents = value; 
                OnPropertyChanged();
            }
        }

        private bool _showByHalls;
        public bool ShowByHalls
        {
            get
            {
                return _showByHalls;
            }
            set
            {
                _showByHalls = value;
                OnPropertyChanged();
            }
        }

        private bool _showByDates;
        public bool ShowByDates
        {
            get
            {
                return _showByDates;
            }
            set
            {
                _showByDates = value;
                OnPropertyChanged();
            }
        }

        private string _errorMessage = "";
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        private String _revenuesReport = "";
        public String RevenuesReport
        {
            get
            {
                return _revenuesReport;
            }
            set
            {
                _revenuesReport = value;
                OnPropertyChanged();
            }
        }


        public RelayCommand ProduceCommand { get; }
        public RelayCommand ClearCommand { get; }



        public RevenuesReportViewModel() 
        {
            _eventsList = LogicalTier.DatabaseMethods.GetList<Event>(true);
            _selectedEvents = new List<Event>();
            _hallsList = LogicalTier.DatabaseMethods.GetList<Hall>(true);
            _selectedHalls = new List<Hall>();

            ProduceCommand = new RelayCommand(Produce);
            ClearCommand = new RelayCommand(Clear);
        }



        // Produce the revenues report and store it in 'RevenuesReport' property
        // if there is erros in the user selections it will not produce report and instead it will store the erros in 'ErrorMessage'
        private void Produce(object? parameter)
        {
            try
            {
                RevenuesReportProducer producer = new(SelectedEvents, SelectedHalls, SelectedStartDate, SelectedEndDate, ShowByEvents, ShowByHalls, ShowByDates);
                Tuple<bool, string> predoucerResult = producer.ProduceReport();

                if (predoucerResult.Item1)
                {
                    RevenuesReport = predoucerResult.Item2;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    RevenuesReport = string.Empty;
                    ErrorMessage = predoucerResult.Item2;
                }
            }
            catch (Exception ex) { ErrorHandler(ex); }
        }


        // Clears user selections and reset report data.
        private void Clear(object? parameter)
        {
            SelectedEvents = new List<Event>();  // using "new()" and not "clear()" so the check-list in the view will update
            SelectedHalls = new List<Hall>();
            SelectedStartDate = null;
            SelectedEndDate = null;
            ShowByEvents = false;
            ShowByHalls = false;
            ShowByDates = false;
            RevenuesReport = "";
            ErrorMessage = "";
        }
    }
}
