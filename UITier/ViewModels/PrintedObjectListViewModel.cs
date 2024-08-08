using System.Collections.Generic;
using System.Text;

namespace UITier.ViewModels
{
    // A ViewModel class for generating a printable list of objects of type T.
    class PrintedObjectListViewModel<T> : WindowViewModelBase
    {
        private string _objectListString = "";
        public string ObjectListString
        {
            get
            {
                return _objectListString;
            }
            set
            {
                _objectListString = value;
                OnPropertyChanged();
            }
        }



        public PrintedObjectListViewModel(List<T> objectsList)
        {
            ObjectListString = CreateObjectListString(objectsList);
        }


        // create a string representation of the objects in 'objectsList'.
        private static string CreateObjectListString(List<T> objectsList)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"{typeof(T).Name} List ({objectsList.Count} objects):\n\n");

            // Iterate through the list of objects and add their string representations to the string builder.
            foreach (T anObject in objectsList)
                if (anObject != null)
                    stringBuilder.AppendLine(anObject.ToString()+"\n");

            return stringBuilder.ToString();
        }
    }
}
