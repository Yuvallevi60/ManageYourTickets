using LogicalTier.DBObjects;
using System.Windows;

namespace UITier.ViewModels
{
    // ViewModel for display and manage lsit of User objcets from the database. Extends ListViewModelBase<T>.
    internal class UserListViewModel: ListViewModelBase<User>
    {
        // override the "Delete" methode from ListViewModelBase<T>.
        // check if the SelectedObject is the User object of the current user of the program.
        // If true, SelectedObject cannot be deleted. If false the methode call for the base "Delete" methode to continum.
        protected override void Delete(object? parameter)
        {
            if (SelectedObject != null)
            {
                var mainWindowViewModel = (MainWindowViewModel)App.Current.MainWindow.DataContext;
                if (mainWindowViewModel.TheCurrentUser?.User.Id == SelectedObject.Id)
                    System.Windows.MessageBox.Show("Logged-in user cannot delete his own User.", "Delete User Falied", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    base.Delete(parameter);
            }
        }
    }
}
