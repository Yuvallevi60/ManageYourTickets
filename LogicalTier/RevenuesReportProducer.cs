using System.Text;
using LogicalTier.DBObjects;

namespace LogicalTier
{
    // A class responsible for producing revenues reports based on given parameters.
    public class RevenuesReportProducer
    {
        // The parameters of the report
        public List<Event> Events { get; }

        public List<Hall> Halls { get; }

        public DateTime? StartDate { get; }

        public DateTime? EndDate { get; }


        // Optional categories to the report
        public bool ShowByEvents { get; }

        public bool ShowByHalls { get; }

        public bool ShowByDates { get; }



        public RevenuesReportProducer(List<Event> events, List<Hall> halls, DateTime? startDate, DateTime? endDate, bool showByEvents, bool showByHalls, bool showByDates)
        {
            Events = events;
            Halls = halls;
            StartDate = startDate;
            EndDate = endDate;
            ShowByEvents = showByEvents;
            ShowByHalls = showByHalls;
            ShowByDates = showByDates;
        }


        // method to produce the revenues report
        // first the method check if the parameters are valid for generating the report
        // If there is an error, return a Tuple with 'false' and the error message
        // If the parameters are valid, return a Tuple with 'true' and the generated report
        public Tuple<bool, string> ProduceReport()
        {
            string error = CanProduce();
            if (error != string.Empty)
            {
                return new Tuple<bool, string>(false, error);
            }
            else
            {
                List<PastOccurrence> list = GetPastOccurrencesList();
                string report = GetRevenuesString(list);

                return new Tuple<bool, string>(true, report);
            }
        }



        // checks the properties before producing revenues report.
        // return string with the erros ,or empty string if all the properties are valid to produce report.
        private string CanProduce()
        {
            string errorMessage = "";

            if (Events.Count == 0)
                errorMessage += "\nChoose at least one Event.\n";

            if (Halls.Count == 0)
                errorMessage += "\nChoose at least one Hall.\n";

            if (StartDate > EndDate)
                errorMessage += "\nStart date cannot be after End date.\n";

            return errorMessage;
        }


        // Returns a list of PastOccurrences from the database that match the criteria of the properties
        private List<PastOccurrence> GetPastOccurrencesList()
        {
            // Retrieve  list of all the 'PastOccurrence' objects in the DB
            List<PastOccurrence> list = LogicalTier.DatabaseMethods.GetList<PastOccurrence>();

            // Apply filters to the list based on selected events and halls.
            list = list.Where(PastOcc => Events.Any(e => e.Id == PastOcc.EventId)).ToList();
            list = list.Where(PastOcc => Halls.Any(hall => hall.Id == PastOcc.HallId)).ToList();

            // Apply date range filtering if dates are selected.
            if (StartDate != null)
            {
                DateOnly startDate = DateOnly.FromDateTime((DateTime)StartDate);
                list = list.Where(PastOcc => (PastOcc.Date >= startDate)).ToList();
            }
            if (EndDate != null)
            {
                DateOnly endDate = DateOnly.FromDateTime((DateTime)EndDate);
                list = list.Where(PastOcc => (PastOcc.Date <= endDate)).ToList();
            }

            return list;
        }


        // gets a list of PastOccurrence objects and returns a string represting the revenue report of this list.
        // The report always includes summary for the entire list.
        // The report optionally displays summaries by events, halls, or dates based on the user selections.
        private string GetRevenuesString(List<PastOccurrence> list)
        {
            int totalOccurrences, totalRevenues, totalTicketsSold;
            StringBuilder stringBuilder = new();

            // Calculate totals for the entire list of past occurrences.
            totalOccurrences = list.Count;
            totalTicketsSold = list.Sum(occ => occ.TicketsSold);
            totalRevenues = list.Sum(occ => occ.Revenue);

            // Display the overall summary for the entire list.
            stringBuilder.AppendLine("Revenues Report");
            stringBuilder.AppendLine($"\nTotal Occurrences: {totalOccurrences}");
            stringBuilder.AppendLine($"\nTotal Tickets Sold: {totalTicketsSold}");
            stringBuilder.AppendLine($"\nTotal Revenues: {totalRevenues} ₪");
            stringBuilder.AppendLine("\n-----------------------------------------------------------------\n");




            // Optional categoris will not be printed if the list is empty 
            if (list.Count == 0)
            {
                return stringBuilder.ToString();
            }


            // Optionally, display summaries by Events.
            if (ShowByEvents)
            {
                stringBuilder.AppendLine("\nBy Events:");
                foreach (Event e in Events) // Calculate totals and add a section for each Event.
                {
                    totalOccurrences = list.Count(occ => occ.EventId == e.Id);
                    totalTicketsSold = list.Where(occ => occ.EventId == e.Id).Sum(occ => occ.TicketsSold);
                    totalRevenues = list.Where(occ => occ.EventId == e.Id).Sum(occ => occ.Revenue);

                    stringBuilder.AppendLine($"\n\n#{e.Id} - {e.Name}");
                    stringBuilder.AppendLine($"\n\tTotal Occurrences: {totalOccurrences}");
                    stringBuilder.AppendLine($"\n\tTotal Tickets Sold: {totalTicketsSold}");
                    stringBuilder.AppendLine($"\n\tTotal Revenues: {totalRevenues} ₪");
                }
                stringBuilder.AppendLine("\n-----------------------------------------------------------------\n");
            }

            
            // Optionally, display summaries Halls.
            if (ShowByHalls)
            {
                stringBuilder.AppendLine("\nBy Halls:");
                foreach (Hall hall in Halls) // Calculate totals and add a section for each Hall.
                {
                    totalOccurrences = list.Count(occ => occ.HallId == hall.Id);
                    totalTicketsSold = list.Where(occ => occ.HallId == hall.Id).Sum(occ => occ.TicketsSold);
                    totalRevenues = list.Where(occ => occ.HallId == hall.Id).Sum(occ => occ.Revenue);

                    stringBuilder.AppendLine($"\n\n#{hall.Id} - {hall.Name}");
                    stringBuilder.AppendLine($"\n\tTotal Occurrences: {totalOccurrences}");
                    stringBuilder.AppendLine($"\n\tTotal Tickets Sold: {totalTicketsSold}");
                    stringBuilder.AppendLine($"\n\tTotal Revenues: {totalRevenues} ₪");
                }
                stringBuilder.AppendLine("\n-----------------------------------------------------------------\n");
            }


            // Optionally, display summaries by the start and end dates.
            if (ShowByDates)
            {
                stringBuilder.AppendLine("\nBy Dates:");
                 
                DateOnly startDate = list.Min(occ => occ.Date); ;
                DateOnly endDate = list.Max(occ => occ.Date);

                for (DateOnly date = startDate; date <= endDate; date = date.AddDays(1)) // Iterate through each date
                {
                    if (list.Any(occ => occ.Date == date))  // If there are occurrences on the date, calculate totals add a section for it in the report.
                    {
                        totalOccurrences = list.Count(occ => occ.Date == date);
                        totalTicketsSold = list.Where(occ => occ.Date == date).Sum(occ => occ.TicketsSold);
                        totalRevenues = list.Where(occ => occ.Date == date).Sum(occ => occ.Revenue);

                        stringBuilder.AppendLine($"\n\n{date}");
                        stringBuilder.AppendLine($"\n\tTotal Occurrences: {totalOccurrences}");
                        stringBuilder.AppendLine($"\n\tTotal Tickets Sold: {totalTicketsSold}");
                        stringBuilder.AppendLine($"\n\tTotal Revenues: {totalRevenues}  ₪");
                    }
                }
                stringBuilder.AppendLine("\n-----------------------------------------------------------------\n");
            }

            return stringBuilder.ToString();
        }

    }
}
