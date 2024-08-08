namespace LogicalTier
{
    // the class contain methods that get string and check it acording to some conditions 
    internal static class StringChecks
    {
        // return true if all the chars in aString are digits
        public static bool IsDigitsOnly(string aString)
        {
            foreach (char c in aString)
            {
                if (!char.IsDigit(c))
                    return false;
            }
            return true;
        }

        // return true if all the chars in aString are english letters or spaces(' ')
        public static bool IsLettersOrSpaces(string aString)
        {
            foreach (char c in aString.Replace(" ", ""))
            {
                if (!(c >= 'A' && c <= 'Z') && !(c >= 'a' && c <= 'z'))
                    return false;
            }
            return true;
        }

        // return empty string if "phoneNumber" is valid phone number, if not it returns the reason way
        public static string IsPhoneNumber(string phoneNumber)
        {
            string[] parts = phoneNumber.Split('-');
            if ((parts.Length == 2) && IsDigitsOnly(parts[0]) && (parts[0].Length == 3) && IsDigitsOnly(parts[1]) && (parts[1].Length == 7))
            {
                return string.Empty;
            }
            return "\nPhone Number should be in the format \"xxx-xxxxxxx\", where 'x' is a digit.\n";
        }

        // return empty string if "creditNumber" is valid credit card number, if not it returns the reason way
        public static string IsCreditCardNumber(string creditNumber)
        {
            string[] parts = creditNumber.Split('-');
            if ((parts.Length == 4) && IsDigitsOnly(parts[0]) && (parts[0].Length == 4) && IsDigitsOnly(parts[1]) && (parts[1].Length == 4)
                                    && IsDigitsOnly(parts[2]) && (parts[2].Length == 4) && IsDigitsOnly(parts[3]) && (parts[3].Length == 4))
            {
                return string.Empty;
            }
            return "\nCredit Card Number should be in the format \"xxxx-xxxx-xxxx-xxxx\", where 'x' is a digit.\n";
        }

        // return empty string if "password" is valid password, if not it returns the reason way
        public static string IsVallidPassword(string password)
        {
            if (password.Contains('\n') || password.Contains('\t') || password.Contains(' '))
                return "\nPassword can not contian whitespace characters.\n";

            if (password.Length < Constants.PASSWORD_MIN_LENGTH)
                return $"\nPassword must be at least {Constants.PASSWORD_MIN_LENGTH}-characters long.\n";

            if (password.Length > Constants.PASSWORD_MAX_LENGTH)
                return $"\nPassword must be no more then {Constants.PASSWORD_MAX_LENGTH}-characters long.\n";

            return string.Empty;
        }

        // return empty string if "idString" is valid ID number, if not it returns the reason way
        public static string IsVallidIdNumber(string idString)
        {
            if (!StringChecks.IsDigitsOnly(idString)) 
                return "\nID number must contain only digits.\n";

            if (idString.Length > Constants.ID_MAX_LENGTH)
                return $"\nID number max length is {Constants.ID_MAX_LENGTH}.\n";

            int idNumber = int.Parse(idString);
            if (idNumber <= 0)
                return "\nID number must be positive number.\n";

            return string.Empty;
        }
    }
}
