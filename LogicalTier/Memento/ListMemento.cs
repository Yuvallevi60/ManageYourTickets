namespace LogicalTier.Memento
{
    // The class is a snapshot to the list of objects before she been filtered and the setting of the filter that applied to it.
    public class ListMemento
    {
        public List<int> IdList { get; private set; } // The Id's of the objects that were in the list before it was filtered

        // The settings of the applied filter to the list represented by 'IdList'
        public string FilterProperty { get; private set; }
        public string FilterType { get; private set; }
        public string FilterValue { get; private set; }

        public ListMemento(List<int> idList, string property, string Type, string value)
        {
            IdList = idList;
            FilterProperty = property;
            FilterType = Type;
            FilterValue = value;
        }
    }
}
