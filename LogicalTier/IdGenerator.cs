using LogicalTier.DBObjects;

namespace LogicalTier
{
    public static class IdGenerator
    {
        // the method return a valid id number for type-T object or return 0 if it fails to do so.
        // The method tries to find a valid Id with codes of increasing complexity
        public static int GenerateAvailableIdNumber<T>() where T : IDBObject
        {
            List<T> objectList = DatabaseMethods.GetList<T>(true);
            int numberOfObjects = objectList.Count;
            int maxObjectsAmount = (int)Math.Pow(10, Constants.ID_MAX_LENGTH) - 1;

            if (numberOfObjects < maxObjectsAmount) // If the amount of object in the collection has not reached the limit there must be an abailable Id number.
            {
                // Rand ID in an increasing range.
                // This option may fail, but it will give more spread ID values in the database
                Random random = new();
                int maxNumber, randomNumber;
                for (int i = numberOfObjects.ToString().Length; i <= Constants.ID_MAX_LENGTH; i++)
                {
                    maxNumber = (int)Math.Pow(10, i) - 1; // Calculate the maximum number with 'i' digits
                    randomNumber = random.Next(1, maxNumber + 1);
                    if (!objectList.Any(anObject => anObject.Id == randomNumber))
                        return randomNumber;
                }


                // Increase Max ID - find the largest used ID and try to use the next one.
                // this option is less complex then the previous one, but it will result in less spread ID values in the database
                int maxId = objectList.Max(user => user.Id);
                if (maxId < maxObjectsAmount)
                    return maxId + 1;


                // Min Available ID - search any Id number until finding the abailable one
                // This option guarantee to find an available ID, but it requires going through every possible ID until an available one is found
                for (int i = 1; i <= maxObjectsAmount; i++) 
                {
                    if (!objectList.Any(anObject => anObject.Id == i))
                        return i;
                }
            }
            return 0;
        }
    }
}
