using System.Text;

namespace LogicalTier.DBObjects
{
    // the class represent an event that can be add to the database of the program
    public class Event : IDBObject
    {
        public enum TicketPricingMethod
        {
            Fixed, // The price remains the same and is not affected by any parameter
            EarlyBird, // The price is cheaper the further the date of the event is
            LastMinute, // The price get cheaper as more days pass since the occurrence creation
            Availability, // The price is more expensive the fewer seats are left
            ByDemand, // The price decreases as more time passes since the last order
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan EventDuration { get; set; }
        public int TicketPrice { get; set; }
        public TicketPricingMethod PricingMethod { get; set; }
        public Dictionary<string, bool> Merchandise { get; set; }
        public int Demand { get; set; } // number of new occurrences demanded for the event
        public bool IsDeleted { get; set; }


        public Event(int idNumber, string eventName, TimeSpan eventDuration, int ticketPrice, TicketPricingMethod pricingMethod, bool hasBrochure, bool hasPoster, bool hasShirt)
        {
            Id = idNumber;
            Name = eventName;
            EventDuration = eventDuration;
            TicketPrice = ticketPrice;
            PricingMethod = pricingMethod;
            Merchandise = new Dictionary<string, bool>
            {
                { "Brochure", hasBrochure },
                { "Poster", hasPoster },
                { "Shirt", hasShirt },
            };
            Demand = 0;
            IsDeleted = false;
        }


        // the method gets parameters to create Event object.
        // the method check if all the parameters are valid for Event object.
        // if valid, the methode create new Event object with the given paramerters and add it to the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public static string Create(string idNumber, string eventName, TimeSpan eventDuration, string ticketPrice, TicketPricingMethod pricingMethod, bool hasBrochure, bool hasPoster, bool hasShirt)
        {
            string checkResult = DataCheck(idNumber, eventName, eventDuration, ticketPrice);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                if (DatabaseMethods.IsObjectExists<Event>(int.Parse(idNumber)))
                    checkResult += "\nEvent with the same id number already exists.\n";
                else
                {
                    Event newEvent = new(int.Parse(idNumber), eventName, eventDuration, int.Parse(ticketPrice), pricingMethod, hasBrochure, hasPoster, hasShirt);
                    DatabaseMethods.AddObject(newEvent);
                }
            }
            return checkResult;
        }


        // the method gets values to update the properties of the Event.
        // the method check if all the parameters are valid for Event object.
        // if valid, the methode set the Event properties to the given values and update the Event object in the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public string Edit(string eventName, TimeSpan eventDuration, string ticketPrice, TicketPricingMethod pricingMethod, bool hasBrochure, bool hasPoster, bool hasShirt)
        {
            string checkResult = DataCheck(Id.ToString(), eventName, eventDuration, ticketPrice);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                Name = eventName;
                EventDuration = eventDuration;
                TicketPrice = int.Parse(ticketPrice);
                PricingMethod = pricingMethod;
                Merchandise = new Dictionary<string, bool>
                {
                    { "Brochure", hasBrochure },
                    { "Poster", hasPoster },
                    { "Shirt", hasShirt },
                };
                Demand = 0; // Reset to zero.
                DatabaseMethods.UpdateObject(this);
            }
            return checkResult;
        }


        // check if the given parameters are valid for Event object.
        // return string with all the erorrs in the parameters.
        private static string DataCheck(string idNumber, string eventName, TimeSpan eventDuration, string ticketPrice)
        {
            string checkResult = "";

            if (string.IsNullOrWhiteSpace(idNumber))
                checkResult += "\nPlease write ID number.\n";
            else
                checkResult += StringChecks.IsVallidIdNumber(idNumber);


            if (string.IsNullOrWhiteSpace(eventName))
                checkResult += "\nPlease write event name.\n";
            else if (!StringChecks.IsLettersOrSpaces(eventName))
                checkResult += "\nEvent name must contain only english letters or spaces.\n";


            if (eventDuration >= TimeSpan.FromHours(24))
                checkResult += "\nThe duration of event must be less then 24 hours.\n";
            else if (eventDuration == TimeSpan.FromHours(0))
                checkResult += "\nThe duration of event cannot be 00:00.\n";

            if (string.IsNullOrWhiteSpace(ticketPrice))
                checkResult += "\nPlease write ticket price.\n";
            else if (!StringChecks.IsDigitsOnly(ticketPrice))
                checkResult += "\nTicket Price must contain only digits.\n";


            return checkResult;
        }





        // set IsDeleted to 'true', set Demand to 0 and update the database.
        public void Delete()
        {
            IsDeleted = true;
            Demand = 0;
            DatabaseMethods.UpdateObject(this);
        }


        // if the object can be chanegd (through Edit or Delete), return empty string.
        // if the object can not, return string with the reason why.
        public string IsChangeable()
        {
            if (IsDeleted)
                return "The Event is deleted.";

            List<Occurrence> occurrencesList = DatabaseMethods.GetListByIntProperty<Occurrence>("EventId", Id); // check for Occurrence of this Event
            if (occurrencesList.Count != 0)
                return "There are still occurrence planned to this event that have not passed yet.";

            return "";
        }


        // specifically update the 'Demand' property as it does not update through the "Edit" method.
        public void UpdateDemand(int demand)
        {
            if (demand >= 0)
            {
                Demand = demand;
                DatabaseMethods.UpdateObject(this);
            }
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"ID: {Id}");
            stringBuilder.AppendLine($"Name: {Name}");
            stringBuilder.AppendLine($"Event Duration: {EventDuration:hh\\:mm}");
            stringBuilder.AppendLine($"Ticket Price: {TicketPrice}");
            stringBuilder.AppendLine($"Pricing Method: {PricingMethod}");
            stringBuilder.AppendLine("Merchandise:");
            foreach (var merchandiseItem in Merchandise)
            {
                stringBuilder.AppendLine($"\t{merchandiseItem.Key}: {(merchandiseItem.Value ? "Available" : "Not Available")}");
            }
            stringBuilder.AppendLine($"Demand: {Demand}");
            stringBuilder.AppendLine($"Is Deleted: {IsDeleted}");

            return stringBuilder.ToString();
        }
    }


}
