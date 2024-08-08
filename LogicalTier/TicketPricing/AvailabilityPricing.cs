using LogicalTier.DBObjects;

namespace LogicalTier.TicketPricing
{
    // Part of the "Strategy" design pattern to calculate the ticket price
    public class AvailabilityPricing : ITicketPricing
    {      
        // The price is more expensive the fewer available seats are left. 
        // The increasement is according to the ratio of the taken seats to the total seats in the hall.

        public int CalculatePrice(Event anEvent, Occurrence occurrence)
        { 
            Hall hall = LogicalTier.DatabaseMethods.GetObject<Hall>(occurrence.HallId) ?? throw new Exception("Hall not found for ID:" + occurrence.HallId);
            int totalSeats = hall.Lines * hall.SeatsInLine;

            List<Order> ordersList = LogicalTier.DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", occurrence.Id); // the occurrence's Order objects
            int takenSeats = ordersList.Sum(ord => ord.Seats.Count);

            double increasement = ((double)takenSeats / totalSeats) * LogicalTier.Constants.MAX_PRICE_INCREASEMENT; // the increasement in percentage.

            double paidPart = 1 + increasement / 100; // The decimal part of the price that need to be paid after the increasement.

            return (int)(anEvent.TicketPrice * paidPart);
        }
    }
}
