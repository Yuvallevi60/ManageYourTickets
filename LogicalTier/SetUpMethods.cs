using LogicalTier.DBObjects;


namespace LogicalTier
{
    // class with methods to run at the start of the program to ensure valid setup of the program
    public static class SetUpMethods
    {
        // calls to all the other private methods in the class
        public static void SetUp()
        {
            AdminVerification();
            DeletePastOccurrencesAndOrders();           
        }



        // The methode check if admin user is saved in the database, and add it if it isnt.
        private static void AdminVerification()
        {
            User? admin = DatabaseMethods.GetObject<User>(Constants.ADMIN_ID);
            if (admin == null)
            {
                admin = new User(Constants.ADMIN_ID, "Admin", "", "", "", 0, "", Constants.ADMIN_PASSWORD, true, true, true, true, true, true);
                DatabaseMethods.AddObject(admin);
            }
        }


        // For each Occurrence object whose 'Date' has passed:
        //   1. The method creates a PastOccurrence object corresponding to this Occurrence object and all its Order objects.
        //   2. Add the PastOccurrence object to the database
        //   3. Deletes from the database the Occurrence object and all its Order objects.
        private static void DeletePastOccurrencesAndOrders() 
        {
            List<Occurrence> list = DatabaseMethods.GetList<Occurrence>(true);
            list = list.Where(occ => (occ.Date < DateOnly.FromDateTime(DateTime.Now))).ToList(); // only occurrences whice their date has passed.
            foreach (Occurrence occurrence in list)
            {
                if (occurrence.HallApproval) // if occurence date pass without it getting approval, dont create PastOccurrence.
                    PastOccurrence.Create(occurrence);

                List<Order> ordersList = DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", occurrence.Id, true); // the occurrence's Order objects
                foreach (Order order in ordersList)
                    order.Delete();

                occurrence.Delete();
            }
        }
    }
}
