using LogicalTier.DBObjects;

namespace LogicalTier.TicketPricing
{
    // Part of the "Strategy" design pattern to calculate the ticket price
    public class EarlyBirdPricing : ITicketPricing
    {

        // The price's discount is decrease as more days pass since the Occurrence's 'CreateDate'
        // (in ratio to the number of days between the Occurrence's 'CreateDate' and its 'Date').
        // If the Occurrence's 'Date' and 'CreateDate' are the same date, the discount will be 0%.

        public int CalculatePrice(Event anEvent, Occurrence occurrence)
        {
            int daysFromCreationToOccurrence = (int)(occurrence.Date.ToDateTime(TimeOnly.MinValue) - occurrence.CreateDate.ToDateTime(TimeOnly.MinValue)).TotalDays;
            int daysFromTodayToOccurrence = (int)(occurrence.Date.ToDateTime(TimeOnly.MinValue) - DateTime.Now).TotalDays;

            double discount; // the discount in percentage
            if (daysFromCreationToOccurrence == 0)
                discount = 0;
            else
                discount = ((double)daysFromTodayToOccurrence / daysFromCreationToOccurrence) * LogicalTier.Constants.MAX_PRICE_DISCOUNT;

            double paidPart = 1 - discount / 100; // The decimal part of the price that need to be paid after the discount.

            return (int)(anEvent.TicketPrice * paidPart);
        }
    }
}
