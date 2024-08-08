using DataBaseTier;
using LogicalTier.DBObjects;

namespace LogicalTier
{
    // The class is responsible to connect the methods in the DataBaseTier.
    // No other class had a method with call to the DataBaseTier.
    public static class DatabaseMethods
    {

        // Add T object to the db.
        // db will update only if there isnt allready an object with the same "Id" property in the db.
        internal static void AddObject<T>(T anObject) where T : IDBObject
        {
            DataBaseTier.MangeDB.InsertObject(anObject);
        }



        // The methode gets object and delete it from the db.
        internal static void DeleteObject<T>(T anObject) where T : IDBObject
        {
            MangeDB.DeleteObject<T>(anObject.Id);
        }



        // Update the T object in the db that his "Id" value is the given 'id' with the given 'anObject'
        // db will update only if there is allready an object with the same "Id" property.
        internal static void UpdateObject<T>(T anObject) where T : IDBObject
        {
            DataBaseTier.MangeDB.UpdateObject(anObject, anObject.Id);
        }



        // return true if T-type object with the given "id" is exists in the db.
        internal static bool IsObjectExists<T>(int id) where T : IDBObject
        {
            return DataBaseTier.MangeDB.IsObjectExists<T>(id);
        }



        // Return the T-type object from the db with the given 'id'
        // Return null if the object does not exist.
        public static T? GetObject<T>(int id)
        {
            return (T)MangeDB.LoadObjectById<T>(id);
        }



        // Return list of the T-type objects in the db.
        // If "isDeleted" is false, filtr out all the objects with value 'true' in the property "IsDeleted".
        public static List<T> GetList<T>(bool isDeleted = false) where T : IDBObject
        {
            List<T> list = DataBaseTier.MangeDB.LoadObjects<T>();
            if (!isDeleted)
                list.RemoveAll(anObject => anObject.IsDeleted);
            return list.OrderBy(obj => obj.Id).ToList();
        }



        // Return list of the T-type objects in the db.
        // The list include only the object with the value 'propertyValue' in the property 'propertyName'.
        // If "isDeleted" is false, filte out all the objects with value 'true' in the property "IsDeleted".
        public static List<T> GetListByIntProperty<T>(string propertyName, int propertyValue, bool isDeleted = false) where T : IDBObject
        {
            if (typeof(T).GetProperty(propertyName) == null)
            {
                throw new ArgumentException($"The property '{propertyName}' does not exist in the {typeof(T).Name} class.");
            }

            List<T> list = DataBaseTier.MangeDB.LoadObjectsByIntProperty<T>(propertyName, propertyValue);

            if (!isDeleted)
                list.RemoveAll(anObject => anObject.IsDeleted);
            return list.OrderBy(obj => obj.Id).ToList();
        }
    }
}
