using System.Text;

namespace LogicalTier.DBObjects
{
    // the class represent an user in the program
    public class User : IDBObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string StreetName { get; set; }
        public int StreetNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set; }
        public Dictionary<string, bool> Permissions { get; set; }



        public User(int id, string firstName, string lastName, string city, string streetName, int streetNumber, string phoneNumber, string password,
                    bool usersPermission, bool hallsPermission, bool eventsPermission, bool occurrencesPermission, bool ordersPermission, bool revenuesPermission)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            City = city;
            StreetName = streetName;
            StreetNumber = streetNumber;
            PhoneNumber = phoneNumber;
            Password = password;

            Permissions = new Dictionary<string, bool>
            {
                { "Users", usersPermission },
                { "Halls", hallsPermission },
                { "Events", eventsPermission },
                { "Occurrences", occurrencesPermission },
                { "Orders", ordersPermission },
                { "Revenues", revenuesPermission}
            };

            IsDeleted = false;
        }



        // the method gets parameters to create User object.
        // the method check if all the parameters are valid for User object.
        // if valid, the methode create new User object with the given paramerters and add it to the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public static string Create(string idNumber, string firstName, string lastName, string city, string streetName, string streetNumber, string phoneNumber, string password,
                                    bool usersPermission, bool hallsPermission, bool eventsPermission, bool occurrencesPermission, bool ordersPermission, bool revenuesPermission)
        {
            string checkResult = DataCheck(idNumber, firstName, lastName, city, streetName, streetNumber, phoneNumber, password);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                if (DatabaseMethods.IsObjectExists<User>(int.Parse(idNumber)))
                    checkResult += "\nUser with the same id number already exists.\n";
                else
                {
                    User newUser = new(int.Parse(idNumber), firstName, lastName, city, streetName, int.Parse(streetNumber), phoneNumber,
                                                    password, usersPermission, hallsPermission, eventsPermission, occurrencesPermission, ordersPermission, revenuesPermission);
                    DatabaseMethods.AddObject(newUser);
                }
            }
            return checkResult;
        }


        // the method gets values to update the properties of the User.
        // the method check if all the parameters are valid for User object.
        // if valid, the methode set the properties to the given values and update the User object in the database.
        // the method return string with the erorrs in the given values, or empty string if all the valuse are valid.
        public string Edit(string firstName, string lastName, string city, string streetName, string streetNumber, string phoneNumber, string password,
                                  bool usersPermission, bool hallsPermission, bool eventsPermission, bool occurrencesPermission, bool ordersPermission, bool revenuesPermission)
        {
            string checkResult = DataCheck(Id.ToString(), firstName, lastName, city, streetName, streetNumber, phoneNumber, password);
            if (checkResult == "") // 'DataCheck' return empty string if all the values are valid
            {
                FirstName = firstName;
                LastName = lastName;
                City = city;
                StreetName = streetName;
                StreetNumber = int.Parse(streetNumber);
                PhoneNumber = phoneNumber;
                Password = password;
                Permissions["Users"] = usersPermission;
                Permissions["Halls"] = hallsPermission;
                Permissions["Events"] = eventsPermission;
                Permissions["Occurrences"] = occurrencesPermission;
                Permissions["Orders"] = ordersPermission;
                Permissions["Revenues"] = revenuesPermission;

                DatabaseMethods.UpdateObject(this);
            }
            return checkResult;
        }


        // check if the given parameters are valid for User object.
        // return string with all the erorrs in the parameters.
        private static string DataCheck(string idNumber, string firstName, string lastName, string city, string streetName, string streetNumber, string phoneNumber, string password)
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


            if (string.IsNullOrWhiteSpace(phoneNumber))
                checkResult += "\nPlease write phone number.\n";
            else
                checkResult += StringChecks.IsPhoneNumber(phoneNumber);


            if (string.IsNullOrWhiteSpace(password))
                checkResult += "\nPlease write password.\n";
            else
                checkResult += StringChecks.IsVallidPassword(password);


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
                return "The User is deleted.";

            if (Id == Constants.ADMIN_ID)
                return "Admin user cannot be deleted or edited.";

            return "";
        }


        // The methode gets string 'newPassword' and check if it is valid as user password.
        // If valid, the "Password" property of the object will be set to 'newPassword' value and the User object will be update in the database.
        // If not valid, the method return string with the reason why
        public string ChangePassword(string newPassword)
        {
            if (string.IsNullOrWhiteSpace(newPassword))
                return "Please write password.";
            else
            {
                string checkResult = StringChecks.IsVallidPassword(newPassword);
                if (string.IsNullOrWhiteSpace(checkResult))
                {
                    Password = newPassword;
                    DatabaseMethods.UpdateObject(this);
                }
                return checkResult;
            }
        }


        public override string ToString()
        {
            StringBuilder stringBuilder = new();

            stringBuilder.AppendLine($"ID: {Id}");
            stringBuilder.AppendLine($"First Name: {FirstName}");
            stringBuilder.AppendLine($"Last Name: {LastName}");
            stringBuilder.AppendLine($"City: {City}");
            stringBuilder.AppendLine($"Street Name: {StreetName}");
            stringBuilder.AppendLine($"Street Number: {StreetNumber}");
            stringBuilder.AppendLine($"Phone Number: {PhoneNumber}");
            stringBuilder.AppendLine($"Password: {Password}");
            stringBuilder.AppendLine("Permissions:");
            foreach (var permission in Permissions)
            {
                stringBuilder.AppendLine($"\t{permission.Key}: {(permission.Value ? "Available" : "Not Available")}");
            }
            stringBuilder.AppendLine($"Is Deleted: {IsDeleted}");

            return stringBuilder.ToString();
        }
    }
}
