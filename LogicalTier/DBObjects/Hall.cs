using System.Text;

namespace LogicalTier.DBObjects
{
    // the class represent a hall that can be add to the database of the program
    public class Hall : IDBObject
    {
        public enum HallType
        {
            Theatre, // One stand in front of the stage
            Traverse, // Two stands, one in each side of the stage
            Thrust, // Three stands, in the left, right and front of the stage
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public HallType Type { get; set; }
        public int Lines { get; set; }
        public int SeatsInLine { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public TimeSpan OpeningHour { get; set; }
        public TimeSpan ClosingHour { get; set; }
        public Dictionary<string, bool> Facilities { get; set; }
        public bool IsDeleted { get; set; }


        public Hall(int id, string name, HallType type, int lines, int seatsInLine, string city, string streetName, int streetNumber,
                            TimeSpan openingHour, TimeSpan closingHour, bool hasParking, bool hasCafeteria, bool hasRestroom)
        {
            Id = id;
            Name = name;
            Type = type;
            Lines = lines;
            SeatsInLine = seatsInLine;
            City = city;
            StreetName = streetName;
            StreetNumber = streetNumber;
            OpeningHour = openingHour;
            ClosingHour = closingHour;

            Facilities = new Dictionary<string, bool>
            {
                { "Parking", hasParking },
                { "Cafeteria", hasCafeteria },
                { "Restroom", hasRestroom },
            };
            IsDeleted = false;
        }



        // the method gets parameters to create Hall object.
        // the method check if all the parameters are valid for Hall object.
        // if valid, the methode create new Hall object with the given paramerters and add it to the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public static string Create(string idNumber, string hallName, HallType type, string lines, string seatsInLine, string city, string streetName,
                                    string streetNumber, TimeSpan openingHour, TimeSpan closingHour, bool hasParking, bool hasCafeteria, bool hasRestroom)
        {
            string checkResult = DataCheck(idNumber, hallName, type, lines, seatsInLine, city, streetName, streetNumber, openingHour, closingHour);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                if (DatabaseMethods.IsObjectExists<Hall>(int.Parse(idNumber)))
                    checkResult += "\nHall with the same id number already exists.\n";
                else
                {
                    Hall newHall = new(int.Parse(idNumber), hallName, type, int.Parse(lines), int.Parse(seatsInLine), city, streetName,
                                            int.Parse(streetNumber), openingHour, closingHour, hasParking, hasCafeteria, hasRestroom);
                    DatabaseMethods.AddObject(newHall);
                }
            }
            return checkResult;
        }


        // the method gets values to update the Hall properties.
        // the method check if all the parameters are valid for Hall object.
        // if valid, the methode set the Hall properties to the given values and update the Hall object in the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public string Edit(string hallName, HallType type, string lines, string seatsInLine, string city, string streetName,
                                  string streetNumber, TimeSpan openingHour, TimeSpan closingHour, bool hasParking, bool hasCafeteria, bool hasRestroom)
        {
            string checkResult = DataCheck(Id.ToString(), hallName, type, lines, seatsInLine, city, streetName, streetNumber, openingHour, closingHour);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                Name = hallName;
                Lines = int.Parse(lines);
                SeatsInLine = int.Parse(seatsInLine);
                Type = type;
                City = city;
                StreetName = streetName;
                StreetNumber = int.Parse(streetNumber);
                OpeningHour = openingHour;
                ClosingHour = closingHour;
                Facilities["Parking"] = hasParking;
                Facilities["Cafeteria"] = hasCafeteria;
                Facilities["Restroom"] = hasRestroom;

                DatabaseMethods.UpdateObject(this);
            }
            return checkResult;
        }


        // check if the given parameters are valid for Hall object.
        // return string with all the erorrs in the parameters.
        private static string DataCheck(string idNumber, string hallName, HallType type, string lines, string seatsInLine, string city,
                                        string streetName, string streetNumber, TimeSpan openingHour, TimeSpan closingHour)
        {
            string checkResult = "";


            if (string.IsNullOrWhiteSpace(idNumber))
                checkResult += "\nPlease write ID number.\n";
            else
                checkResult += StringChecks.IsVallidIdNumber(idNumber);


            if (string.IsNullOrWhiteSpace(hallName))
                checkResult += "\nPlease write the hall name.\n";
            else if (!StringChecks.IsLettersOrSpaces(hallName))
                checkResult += "\nHall name must contain only english letters or spaces.\n";


            if (string.IsNullOrWhiteSpace(lines) || string.IsNullOrWhiteSpace(seatsInLine))
                checkResult += "\nPlease write lines number and seats in line.\n";
            else if (!StringChecks.IsDigitsOnly(lines) || !StringChecks.IsDigitsOnly(seatsInLine))
                checkResult += "\nLines number and seats in line number must contain only digits.\n";
            else
            {
                int seats = int.Parse(seatsInLine);
                int linesNumber = int.Parse(lines);
                if (seats == 0 || linesNumber == 0)
                    checkResult += "\nLines number and seats must be positive.\n";

                else if (linesNumber * seats > Constants.MAX_SEATS_IN_HALL)
                    checkResult = $"\nThe max number of total seats in a hall is {Constants.MAX_SEATS_IN_HALL} and this hall has {linesNumber * seats}.\n";

                else if (linesNumber > Constants.MAX_LINES_IN_HALL)
                    checkResult += $"\nThe max number of lines in a hall is {Constants.MAX_LINES_IN_HALL}.\n";

                else if (seats > Constants.MAX_SEATS_IN_LINE)
                    checkResult += $"\nThe max number of seats in a line is {Constants.MAX_SEATS_IN_LINE}.\n";

                else if (type == HallType.Thrust && seats % 3 != 0)
                    checkResult += "\nIn hall of type 'Thrust', seats in line must be a multiple of 3.\n";

                else if (type == HallType.Traverse && seats % 2 != 0)
                    checkResult += "\nIn hall of type 'Traverse', seats in line must be a multiple of 2.\n";
            }


            if (string.IsNullOrWhiteSpace(city))
                checkResult += "\nPlease write city name.\n";
            else if (!StringChecks.IsLettersOrSpaces(city))
                checkResult += "\nCity name must contain only english letters or spaces.\n";


            if (string.IsNullOrWhiteSpace(streetName))
                checkResult += "\nPlease write street name.\n";
            else if (!StringChecks.IsLettersOrSpaces(streetName))
                checkResult += "\nStreet name name must contain only english letters or spaces.\n";


            if (string.IsNullOrWhiteSpace(streetNumber))
                checkResult += "\nPlease write street number.\n";
            else if (!StringChecks.IsDigitsOnly(streetNumber))
                checkResult += "\nStreet number must contain only digits.\n";


            if (openingHour >= TimeSpan.FromHours(24) || closingHour >= TimeSpan.FromHours(24))
                checkResult += "\nOpening and closing hours must be a valid value between 00:00 and 23:59.\n";
            else
            {
                if (openingHour < TimeSpan.FromHours(Constants.MIN_OPENING_HOUR))
                    checkResult += $"\nOpening hour cannot be earlier then {TimeSpan.FromHours(Constants.MIN_OPENING_HOUR):hh\\:mm}.\n";

                else if (openingHour >= closingHour)
                    checkResult += "\nOpening hour must be before the closing hour.\n";
            }
            return checkResult;
        }



        // set IsDeleted to 'true' and update the database.
        public void Delete()
        {
            IsDeleted = true;
            DatabaseMethods.UpdateObject(this);
        }


        // if the object can be chanegd (through Edit or Delete), return empty string.
        // if the object can not, return string with the reason why.
        public string IsChangeable()
        {
            if (IsDeleted)
                return "The Hall is deleted.";

            List<Occurrence> occurrencesList = DatabaseMethods.GetListByIntProperty<Occurrence>("HallId", Id);
            if (occurrencesList.Count != 0) // check for Occurrence in this Hall
                return "There are still occurrence planned in this hall that have not happened yet.";

            return "";
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"ID: {Id}");
            stringBuilder.AppendLine($"Name: {Name}");
            stringBuilder.AppendLine($"Hall Type: {Type}");
            stringBuilder.AppendLine($"Lines: {Lines}");
            stringBuilder.AppendLine($"Seats in Line: {SeatsInLine}");
            stringBuilder.AppendLine($"City: {City}");
            stringBuilder.AppendLine($"Street Name: {StreetName}");
            stringBuilder.AppendLine($"Street Number: {StreetNumber}");
            stringBuilder.AppendLine($"Opening Hour: {OpeningHour:hh\\:mm}");
            stringBuilder.AppendLine($"Closing Hour: {ClosingHour:hh\\:mm}");
            stringBuilder.AppendLine("Facilities:");
            foreach (var facility in Facilities)
            {
                stringBuilder.AppendLine($"\t{facility.Key}: {(facility.Value ? "Available" : "Not Available")}");
            }
            stringBuilder.AppendLine($"Is Deleted: {IsDeleted}");

            return stringBuilder.ToString();
        }
    }
}
