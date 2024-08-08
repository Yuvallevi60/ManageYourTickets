using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace UITier.ViewModels
{
    // Abstract base class for ViewModel classes in the WPF application (the UI tire).
    // It implements the INotifyPropertyChanged interface to provide property change notification.
    internal abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged; // Event raised when a property value changes.

        // The method is used to notify the UI of property changes.
        // [CallerMemberName] attribute allows automatic property name detection when called within property setters.
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        // Method for handling exceptions.
        // It assumes that the application's main window's DataContext is a MainWindowViewModel and have method 'ShowErrorPage(Execption ex)'.
        protected static void ErrorHandler(Exception ex)
        {
            ((MainWindowViewModel)Application.Current.MainWindow.DataContext).ShowErrorPage(ex);
        }
    }
}
