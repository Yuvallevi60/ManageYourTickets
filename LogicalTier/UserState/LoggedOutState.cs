namespace LogicalTier.UserState
{
    // class to be used when the current user is logged out. Part of the "State" design pattern.
    public class LoggedOutState : State
    {
        public LoggedOutState(CurrentUser currentUser) : base(currentUser)
        {
        }


        
        public override void HandleTimeout() 
        {
            // User is already logged out, no action needed.
        }

        public override void HandleActivity() 
        {
            // User is already logged out, activity wont cahange his State.
        }

        public override string ToString()
        {
            return "Logged Out";
        }
    }
}
