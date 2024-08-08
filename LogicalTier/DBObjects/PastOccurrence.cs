namespace LogicalTier.DBObjects
{
    // the class represent a past occurrence of event that can be add to the database of the program
    public class PastOccurrence : IDBObject
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int HallId { get; set; }
        public DateOnly Date { get; set; }
        public int TicketsSold { get; set; }
        public int Revenue { get; set; }
        public bool IsDeleted { get; set; }


        public PastOccurrence(int id, int eventId, int hallId, DateOnly date, int ticketsSold, int revenue)
        {
            Id = id;
            EventId = eventId;
            HallId = hallId;
            Date = date;
            TicketsSold = ticketsSold;
            Revenue = revenue;
            IsDeleted = false;
        }


        // the method gets Occurrence.
        // the method sum all the tickets sold for the Occurrence and its total revenue.
        // then it creates PastOccurrence object and add it to the DataBase.
        public static void Create(Occurrence occurrence)
        {
            int id = IdGenerator.GenerateAvailableIdNumber<PastOccurrence>(); // id for the new PastOccurrence object
            if (id == 0)
                throw new Exception($"No Avalible Id number for 'PastOccurrence' object for #{occurrence.Id} Occurrence");

            List<Order> ordersList = DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", occurrence.Id, true); // the occurrence's Order objects
            int totalTicketSold = ordersList.Sum(ord => ord.Seats.Count);
            int totalRevenue = ordersList.Sum(ord => ord.Price);

            PastOccurrence pastOccurrence = new(id, occurrence.EventId, occurrence.HallId, occurrence.Date, totalTicketSold, totalRevenue);
            DatabaseMethods.AddObject(pastOccurrence);
        }


        // Delete the object from the datadbase.
        public void Delete()
        {
            DatabaseMethods.DeleteObject(this);
        }


        // if the object can be chanegd (through Edit or Delete), return empty string.
        // if the object can not, return string with the reason why. 
        public string IsChangeable()
        {
            return "PastOccurrences are intended to calculate previous profits and cannot be edited or deleted.";
        }
    }
}
