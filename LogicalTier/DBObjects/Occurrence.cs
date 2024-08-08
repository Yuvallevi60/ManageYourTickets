using System.Text;

namespace LogicalTier.DBObjects
{
    // the class represent an occurrence of event that can be add to the database of the program
    public class Occurrence : IDBObject
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; }
        public int HallId { get; set; }
        public string HallName { get; set; }
        public DateOnly Date { get; set; }
        public TimeSpan Hour { get; set; }
        public DateOnly CreateDate { get; set; } // creation date of the object
        public bool HallApproval { get; set; } // does the hall approve the occurrence
        public bool IsDeleted { get; set; }


        public Occurrence(int id, int eventId, string eventName, int hallId, string hallName, DateOnly date, TimeSpan hour, DateOnly createDate)
        {
            Id = id;
            EventId = eventId;
            EventName = eventName;
            HallId = hallId;
            HallName = hallName;
            Date = date;
            Hour = hour;
            CreateDate = createDate;
            HallApproval = false;
            IsDeleted = false;
        }





        // the method gets parameters to create Occurrence object.
        // the method check if all the parameters are valid for Occurrence object.
        // if valid, the methode create new Occurrence object with the given paramerters and add it to the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public static string Create(string idNumber, Event anEvent, Hall hall, DateOnly date, TimeSpan hour)
        {
            string checkResult = DataCheck(idNumber, anEvent, hall, date, hour, false);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                if (anEvent.Demand <= 0)
                    checkResult += "\nThere is no demand for new occurrence for the selected event.\n";
                else if (DatabaseMethods.IsObjectExists<Occurrence>(int.Parse(idNumber)))
                    checkResult += "\nOccurrence with the same id number already exists.\n";
                else
                {
                    Occurrence newOccurrence = new(int.Parse(idNumber), anEvent.Id, anEvent.Name, hall.Id, hall.Name, date, hour, DateOnly.FromDateTime(DateTime.Now));
                    DatabaseMethods.AddObject(newOccurrence);
                    anEvent.UpdateDemand(anEvent.Demand - 1); // reduce the demand of occurrences to anEvent
                }
            }
            return checkResult;
        }



        // the method gets values to update the object properties.
        // the method check if all the parameters are valid for Occurrence object.
        // if valid, the methode set the Occurrence properties to the given values and update the Occurrence object in the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public string Edit(Event anEvent, Hall hall, DateOnly date, TimeSpan hour)
        {
            string checkResult = DataCheck(Id.ToString(), anEvent, hall, date, hour, true);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                EventId = anEvent.Id;
                EventName = anEvent.Name;
                HallId = hall.Id;
                HallName = hall.Name;
                Date = date;
                Hour = hour;
                HallApproval = false; // the occurance need to be re-approve by the hall  
                DatabaseMethods.UpdateObject(this);
            }
            return checkResult;
        }



        // check if the given parameters are valid for Occurrence object.
        // return string with all the erorrs in the parameters.
        // 'existingObject' is to indicate if the method used with existing object data.
        private static string DataCheck(string idNumber, Event anEvent, Hall hall, DateOnly date, TimeSpan hour, bool existingObject)
        {
            string checkResult = "";

            if (string.IsNullOrWhiteSpace(idNumber))
                checkResult += "\nPlease write ID number.\n";
            else
                checkResult += StringChecks.IsVallidIdNumber(idNumber);


            if (date <= DateOnly.FromDateTime(DateTime.Now))
                checkResult += "\nThe selected date must be at least one day after the current date.\n";


            if (hour >= TimeSpan.FromHours(24))
                checkResult += "\nThe selected hour must be a valid value between 00:00 and 23:59.\n";
            else
            {
                List<Occurrence> occurrencesList = DatabaseMethods.GetListByIntProperty<Occurrence>("EventId", anEvent.Id); // all Occurrence objects of the same Event
                if (existingObject && int.TryParse(idNumber, out int idInt))
                {
                    occurrencesList.RemoveAll(occ => occ.Id == idInt); // Remove from the list the object whose information is checked. 
                }
                TimeSpan interval = TimeSpan.FromMinutes(Constants.EVENT_OCCURRENCE_INTERVAL);
                TimeSpan newOccStart = hour;
                TimeSpan newOccEnd = hour + anEvent.EventDuration;
                foreach (Occurrence occ in occurrencesList)
                {
                    if (occ.Date.Equals(date))
                    {
                        TimeSpan existingOccStart = occ.Hour;
                        TimeSpan existingOccEnd = occ.Hour + anEvent.EventDuration;
                        if (newOccStart < existingOccEnd + interval && existingOccStart < newOccEnd + interval)
                        {
                            checkResult += "\nThe selected hour conflicts with another occurrence of the event at the same date.\n" +
                                            $"Existing occurrence: {existingOccStart:hh\\:mm} - {existingOccEnd:hh\\:mm}.\n" +
                                            $"New occurrence: {newOccStart:hh\\:mm} - {newOccEnd:hh\\:mm}.\n" +
                                            $"There should be an interval of at least {Constants.EVENT_OCCURRENCE_INTERVAL} minutes between two occurrences of the same event.\n";
                        }
                    }
                }



                if (hour < hall.OpeningHour)
                {
                    checkResult += $"\nThe selected hour is before the opening of the selected hall at {hall.OpeningHour:hh\\:mm}.\n";
                }
                else if (newOccEnd > hall.ClosingHour)
                {
                    checkResult += $"\nAt the selected hour the event will end at {newOccEnd:hh\\:mm}.\n" +
                                     $"This is after the closing of the selected hall at {hall.ClosingHour:hh\\:mm}.\n";
                }
                else
                {
                    occurrencesList = DatabaseMethods.GetListByIntProperty<Occurrence>("HallId", hall.Id); // all Occurrence objects of the same Hall
                    if (existingObject && int.TryParse(idNumber, out idInt))
                    {
                        occurrencesList.RemoveAll(occ => occ.Id == idInt); // Remove from the list the object whose data is checked.
                    }
                    interval = TimeSpan.FromMinutes(Constants.HALL_OCCURRENCE_INTERVAL);
                    foreach (Occurrence occ in occurrencesList)
                    {
                        if (occ.Date.Equals(date))
                        {
                            Event eventInHall = DatabaseMethods.GetObject<Event>(occ.EventId) ?? throw new Exception("Event not found for ID:" + occ.EventId);
                            TimeSpan existingOccStart = occ.Hour;
                            TimeSpan existingOccEnd = occ.Hour + eventInHall.EventDuration;
                            if (newOccStart < existingOccEnd + interval && existingOccStart < newOccEnd + interval)
                            {
                                checkResult += "\nThe selected hour conflicts with another occurrence in the selected hall at the same date.\n" +
                                                $"Existing occurrence: {existingOccStart:hh\\:mm} - {existingOccEnd:hh\\:mm}.\n" +
                                                $"New occurrence: {newOccStart:hh\\:mm} - {newOccEnd:hh\\:mm}.\n" +
                                                $"There should be an interval of at least {Constants.HALL_OCCURRENCE_INTERVAL} minutes between two occurrences at the same hall.\n";
                            }
                        }
                    }
                }
            }

            return checkResult;
        }





        // Delete the object from the datadbase. Increse its Event's Demand if its Date not pass.
        public void Delete()
        {
            if (Date >= DateOnly.FromDateTime(DateTime.Now))
            {
                Event eve = DatabaseMethods.GetObject<Event>(EventId) ?? throw new Exception("Event not found for ID:" + EventId);
                eve.UpdateDemand(eve.Demand + 1);
            }
            DatabaseMethods.DeleteObject(this);
        }



        // if the object can be chanegd (through Edit or Delete), return empty string.
        // if the object can not, return string with the reason why.
        public string IsChangeable()
        {
            if (IsDeleted)
                return "The Occurrence is deleted.";

            List<Order> orderList = DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", Id);
            if (orderList.Count != 0)
                return "There are Orders for this Occurrence.";
            return "";
        }




        // The method return boolean metriex.
        // the metrix (i,j) indexes are the lines number (i) and the number of seats in line (j) in the hall of the Occurrence
        // In index [i,j] of the array the value is 'true' if there is an order to the j seat in line i.
        public bool[,] GetSeatsMetrix()
        {
            Hall eventHall = DatabaseMethods.GetObject<Hall>(HallId) ?? throw new Exception("Falied to create Seats Metrix for the occurrence.\nHall not found for ID:" + HallId);

            bool[,] SeatMetrix = new bool[eventHall.Lines, eventHall.SeatsInLine];

            List<Order> soldTicketsList = DatabaseMethods.GetListByIntProperty<Order>("OccurrenceId", Id);

            foreach (Order order in soldTicketsList)
            {
                foreach ((int row, int column) seat in order.Seats)
                    SeatMetrix[seat.row - 1, seat.column - 1] = true;
            }

            return SeatMetrix;
        }


        // if the method get 'true' it set the Occurrence.HallApproval to 'true' and update the database
        // if the method get 'false' it delete the Occurrence from the data base
        public void UpdateHallApproval(bool isApproval)
        {
            if (isApproval)
            {
                HallApproval = isApproval;
                DatabaseMethods.UpdateObject(this);
            }
            else
                Delete();
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"ID: {Id}");
            stringBuilder.AppendLine($"Event ID: {EventId}");
            stringBuilder.AppendLine($"Event Name: {EventName}");
            stringBuilder.AppendLine($"Hall ID: {HallId}");
            stringBuilder.AppendLine($"Hall Name: {HallName}");
            stringBuilder.AppendLine($"Date: {Date:dd-MM-yyyy}");
            stringBuilder.AppendLine($"Hour: {Hour:hh\\:mm}");
            stringBuilder.AppendLine($"Create Date: {CreateDate:dd-MM-yyyy}");
            stringBuilder.AppendLine($"Is Deleted: {IsDeleted}");

            return stringBuilder.ToString();
        }
    }
}
