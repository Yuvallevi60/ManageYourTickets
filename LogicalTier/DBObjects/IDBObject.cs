namespace LogicalTier.DBObjects
{
    // An interface for class of database object
    public interface IDBObject
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }


        // The methode gets IDBObject object, set his 'IsDeleted' property to false and update the database.
        public static void Restore<T>(T anObject) where T : IDBObject
        {
            string typeName = anObject.GetType().Name;
            if (typeName == "User" || typeName == "Event" || typeName == "Hall") // Occurrence and Order objects cannot be restored
            {
                anObject.IsDeleted = false;
                DatabaseMethods.UpdateObject(anObject);
            }
        }


        // Delete the object from the datadbase or set IsDeleted to 'true'.
        public void Delete();


        // if the object can be chanegd (through edit or delete), return empty string.
        // if the object can not, return string with the reason why.
        public string IsChangeable();
    }
}
