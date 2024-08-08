using System.Text;

namespace LogicalTier.DBObjects
{
    // the class represent an order of tickets to an occurrence that can be add to the database of the program
    public class Order : IDBObject
    {
        public int Id { get; set; }
        public int OccurrenceId { get; set; }
        public List<(int, int)> Seats { get; set; }
        public int Price { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCard { get; set; }
        public DateOnly OrderDate { get; set; }
        public bool IsDeleted { get; set; }


        public Order(int idNumber, int occurrenceId, List<(int, int)> seats, int price, string customerFirstName,
                              string customerLastName, string phoneNumber, string creditCard, DateOnly orderDate)
        {
            Id = idNumber;
            OccurrenceId = occurrenceId;
            Seats = seats;
            Price = price;
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            PhoneNumber = phoneNumber;
            CreditCard = creditCard;
            OrderDate = orderDate;
            IsDeleted = false;
        }




        // the method gets parameters to create Order object.
        // the method check if all the parameters are valid for Order object.
        // if valid, the methode create new Order object with the given paramerters and add it to the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public static string Create(string idNumber, int occurrenceId, List<(int, int)> seats, int price, string firstName,
                                       string lastName, string phoneNumber, string creditCardNumber)
        {
            string checkResult = DataCheck(idNumber, firstName, lastName, phoneNumber, creditCardNumber);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                if (DatabaseMethods.IsObjectExists<Order>(int.Parse(idNumber)))
                    checkResult += "\nOrder with the same id number already exists.\n";
                else
                {
                    Order newOrder = new(int.Parse(idNumber), occurrenceId, seats, price, firstName, lastName, phoneNumber, creditCardNumber, DateOnly.FromDateTime(DateTime.Now));
                    DatabaseMethods.AddObject(newOrder);
                }
            }
            return checkResult;
        }


        // the method gets values to update the object properties.
        // the method check if all the parameters are valid for Order object.
        // if valid, the methode set the Order properties to the given values and update the Order object in the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public string Edit(string firstName, string lastName, string phoneNumber, string creditCard)
        {
            string checkResult = DataCheck(Id.ToString(), firstName, lastName, phoneNumber, creditCard);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                CustomerFirstName = firstName;
                CustomerLastName = lastName;
                PhoneNumber = phoneNumber;
                CreditCard = creditCard;
                DatabaseMethods.UpdateObject(this);
            }
            return checkResult;
        }


        // check if the given parameters are valid for Order object.
        // return string with all the erorrs in the parameters.
        private static string DataCheck(string idNumber, string firstName, string lastName, string phoneNumber, string creditCardNumber)
        {
            string checkResult = "";

            if (string.IsNullOrWhiteSpace(idNumber))
                checkResult += "\nPlease write ID number.\n";
            else
                checkResult += StringChecks.IsVallidIdNumber(idNumber);


            if (string.IsNullOrWhiteSpace(firstName))
                checkResult += "\nPlease write first name.\n";
            else if (!StringChecks.IsLettersOrSpaces(firstName))
                checkResult += "\nFirst name must contain only english letters or spaces.\n";


            if (string.IsNullOrWhiteSpace(lastName))
                checkResult += "\nPlease write last name.\n";
            else if (!StringChecks.IsLettersOrSpaces(lastName))
                checkResult += "\nLast name must contain only english letters or spaces.\n";


            if (string.IsNullOrWhiteSpace(phoneNumber))
                checkResult += "\nPlease write phone number.\n";
            else
                checkResult += StringChecks.IsPhoneNumber(phoneNumber);


            if (string.IsNullOrWhiteSpace(creditCardNumber))
                checkResult += "\nPlease write credit card number.\n";
            else
                checkResult += StringChecks.IsCreditCardNumber(creditCardNumber);

            return checkResult;
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
            if (IsDeleted)
                return "The Order is deleted.";

            Occurrence occurrence = DatabaseMethods.GetObject<Occurrence>(OccurrenceId) ?? throw new Exception("Occurrence not found for ID:" + OccurrenceId);
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);

            if (occurrence.Date.CompareTo(today) <= 0)
            {
                return "You can change or cancel a ticket order only up to a day before the event date.";
            }
            return "";
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"ID: {Id}");
            stringBuilder.AppendLine($"Event Occurrence ID: {OccurrenceId}");
            stringBuilder.AppendLine("Seats:");
            foreach (var seat in Seats)
            {
                stringBuilder.AppendLine($"  Row: {seat.Item1}, Seat Number: {seat.Item2}");
            }
            stringBuilder.AppendLine($"Price: {Price}");
            stringBuilder.AppendLine($"First Name: {CustomerFirstName}");
            stringBuilder.AppendLine($"Last Name: {CustomerLastName}");
            stringBuilder.AppendLine($"Phone Number: {PhoneNumber}");
            stringBuilder.AppendLine($"Credit Card: {CreditCard}");
            stringBuilder.AppendLine($"Order Date: {OrderDate:dd-MM-yyyy}");
            stringBuilder.AppendLine($"Is Deleted: {IsDeleted}");

            return stringBuilder.ToString();
        }
    }
}
