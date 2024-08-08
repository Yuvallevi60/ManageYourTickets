using LogicalTier.DBObjects;

namespace LogicalTier.TicketPricing
{
    // interface that implemet the "Strategy" design pattern to calculate the ticket price of event according to his pracing methode
    public interface ITicketPricing
    {
        public int CalculatePrice(Event anEvent, Occurrence occurrence);
    }
}
