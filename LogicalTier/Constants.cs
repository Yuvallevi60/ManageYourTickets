namespace LogicalTier
{
    // contains constants to use in the program
    public static class Constants
    {
        public const int ACTIVE_STATE_DURATION = 300000; // 5 minutes

        public const int INACTIVE_STATE_DURATION = 30000; // 30 seconeds

        public const int ADMIN_ID = 0; // the Id of the User object of the Admin

        public const string ADMIN_PASSWORD = "Admin"; // the Password of the User object of the Admin

        public const int ID_MAX_LENGTH = 6; // The max length of Id number

        public const int PASSWORD_MIN_LENGTH = 4; // The min length of user password

        public const int PASSWORD_MAX_LENGTH = 10; // The max length of user password

        public const int EVENT_OCCURRENCE_INTERVAL = 30; // The minimal minutes interval between two Occurrence of the same Event 

        public const int HALL_OCCURRENCE_INTERVAL = 30; // The minimal minutes interval between two Occurrence at the same Hall 

        public const int MIN_OPENING_HOUR = 6; // 06:00, The Min Hour a Hall can Open at.

        public const int MAX_SEATS_IN_HALL = 500; // The max number of total seats in a hall

        public const int MAX_LINES_IN_HALL = 25; // The max number of lines in a hall

        public const int MAX_SEATS_IN_LINE = 25; // The max number of seats (columns) in a line

        public const int MAX_PRICE_DISCOUNT = 25; // The max discount in % that can be given to a ticket price 

        public const int MAX_PRICE_INCREASEMENT = 25; // The max rise in % that can be given to a ticket price 



    }
}
