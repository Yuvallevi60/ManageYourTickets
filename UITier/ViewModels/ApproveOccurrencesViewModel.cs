using LogicalTier.DBObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UITier.ViewModels
{
    // This ViewModel class is used for displaying a Window to approve or disapprove the Occurence at the Hall.
    class ApproveOccurrencesViewModel : WindowViewModelBase
    {
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
                SetApprovalCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand SetApprovalCommand { get; set; }



        public ApproveOccurrencesViewModel()
        {
            OccurrencesList = LogicalTier.DatabaseMethods.GetList<Occurrence>().Where(o => !o.HallApproval).OrderBy(o => o.HallName).ToList();
            SetApprovalCommand = new RelayCommand(SetApproval ,  (_ => SelectedOccurrence != null));
        }


        // Update the HallApproval of the SelectedOccurrence and reset the ViewModel properties
        private void SetApproval(object? parameter)
        {
            if ((SelectedOccurrence != null) && (parameter is string strParameter))
            {
                if (bool.TryParse(strParameter, out bool isApproved))
                {
                    try
                    {
                        SelectedOccurrence.UpdateHallApproval(isApproved);
                        OccurrencesList = LogicalTier.DatabaseMethods.GetList<Occurrence>().Where(o => !o.HallApproval).OrderBy(o => o.HallName).ToList();
                        SelectedOccurrence = null;
                    }
                    catch (Exception ex) { ErrorHandler(ex); }
                }
            }
        }
    }
}
