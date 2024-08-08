using MongoDB.Bson;
using MongoDB.Driver;

namespace DataBaseTier
{

    // The class contain methods to connect and do operations on the MongoDB database of the program.
    public static class MangeDB
    {
        private const string MONGODB_CONNECTION_STRING = "mongodb+srv://Yuvallevi:Yuvallevi@cluster0.auxviba.mongodb.net/?retryWrites=true&w=majority";
        private const string DATA_BASE_NAME = "ManageYourTicketsDB";
        private const string USERS_COLLECTION = "Users";
        private const string EVENTS_COLLECTION = "Events";
        private const string HALLS_COLLECTION = "Halls";
        private const string ORDERS_COLLECTION = "Orders";
        private const string OCCURRENCES_COLLECTION = "Occurrences";
        private const string PASTOCCURRENCES_COLLECTION = "PastOccurrences";



        // Return the collection of 'T' object that store in the db.
        private static IMongoCollection<T> GetDBCollection<T>() 
        {
            string collectionName = GetCollectionName<T>(); 

            var clinet = new MongoClient(MONGODB_CONNECTION_STRING);
            var db = clinet.GetDatabase(DATA_BASE_NAME);
    
            var collection = db.GetCollection<T>(collectionName);
            return collection;
        }



        // Return the name of the T objects collection in the db.
        private static string GetCollectionName<T>() 
        {
            string objectName = typeof(T).Name;
            return objectName switch
            {
                "User" => USERS_COLLECTION,
                "Hall" => HALLS_COLLECTION,
                "Event" => EVENTS_COLLECTION,
                "Occurrence" => OCCURRENCES_COLLECTION,
                "PastOccurrence" => PASTOCCURRENCES_COLLECTION,
                "Order" => ORDERS_COLLECTION,
                _ => throw new InvalidOperationException($"Collection name not found for type {objectName}"), 
            };
        }



        // Insert T object to the db.
        // db will update only if there isnt allready an object with the same "Id" property in the db.
        public static void InsertObject<T>(T anObject) 
        { 
            var collection = GetDBCollection<T>();
            collection.InsertOne(anObject);           
        }



        // Return T object from the db with "Id" value as the given "id".
        // return null if object didnt exist.
        public static T LoadObjectById<T>(int id) 
        {
            var collection = GetDBCollection<T>();

            var filter = Builders<T>.Filter.Eq("Id", id);
            return collection.Find(filter).FirstOrDefault();
        }



        // Return list of the T objects collection from the db.
        public static List<T> LoadObjects<T>() 
        {
            var collection = GetDBCollection<T>();
            return collection.Find(new BsonDocument()).ToList();
        }



        // Return list of the T objects collection from the db.
        // The list include only the objects with the value 'propertyValue' in the property 'propertyName'.
        public static List<T> LoadObjectsByIntProperty<T>(string propertyName, int propertyValue) 
        {
            var collection = GetDBCollection<T>();
            var filter = Builders<T>.Filter.Eq(propertyName, propertyValue);
            return collection.Find(filter).ToList();
        }



        // replace the T object in the db that his "Id" value is the given 'id' with the given 'anObject'
        // db will update only if there is allready an object with the same "Id" property.
        public static void UpdateObject<T>(T anObject, int id)
        {
            var collection = GetDBCollection<T>();

            var filter = Builders<T>.Filter.Eq("Id", id);
            collection.ReplaceOne(filter, anObject);
        }



        // Return 'true' if there is T object with the given "id" in the db.
        public static bool IsObjectExists<T>(int id)
        {
            var collection = GetDBCollection<T>();
            var filter = Builders<T>.Filter.Eq("Id", id);

            return (collection.Find(filter).FirstOrDefault() != null) ;
        }



        // Delete object with the given 'id' from the T objects collection in the database.
        public static void DeleteObject<T>(int id) 
        {
            var collection = GetDBCollection<T>();
            var filter = Builders<T>.Filter.Eq("Id", id);

            collection.DeleteOne(filter);
        }
    }
}