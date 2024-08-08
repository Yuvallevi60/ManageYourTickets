using LogicalTier.DBObjects;

namespace LogicalTier.TicketPricing
{
    // Part of the "Strategy" design pattern to calculate the ticket price
    public class FixedPricing : ITicketPricing
    {
        // The price remains the same and is not affected by any parameter

        public int CalculatePrice(Event anEvent, Occurrence occurrence)
        {
            return anEvent.TicketPrice;
        }
    }
}
