using LogicalTier.DBObjects;

namespace LogicalTier.TicketPricing
{
    // Part of the "Strategy" design pattern to calculate the ticket price 
    public class ByDemandPricing : ITicketPricing
    {
        // The price's discount increases as more days passes since the 'OrderDate' of the last Order.
        // Every day that passes equals 1% more to the discount until it reaches the constant 'MAX_PRICE_DISCOUNT'
        // If the Occurrence has no Order yet, the discount is calculate acording to the days that pass since the Occurrence's 'CreateDate'

        public int CalculatePrice(Event anEvent, Occurrence occurrence)
        {
            List<Order> ordersList = LogicalTier.DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", occurrence.Id);

            DateOnly lastOrder;
            if (ordersList.Count > 0)
                lastOrder = ordersList.OrderByDescending(order => order.OrderDate).First().OrderDate;
            else
                lastOrder = occurrence.CreateDate;

            int daysFromLastOrderToToday = (int)((DateTime.Now - lastOrder.ToDateTime(TimeOnly.MinValue)).TotalDays);

            double discount = Math.Min(daysFromLastOrderToToday, LogicalTier.Constants.MAX_PRICE_DISCOUNT);

            double paidPart = 1 - discount / 100; // The decimal part of the price that need to be paid after the discount.

            return (int)(anEvent.TicketPrice * paidPart);
        }
    }
}
