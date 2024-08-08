namespace LogicalTier.UserState
{
    // Part of the "State" design pattern.
    // class to be used when the current user is become inactive due to him not perform any activity on the program for a while
    public class InactiveState : State
    {
        public InactiveState(CurrentUser currentUser) : base(currentUser)
        {
        }
        
        public override void HandleTimeout()
        {
            // User has been inactive for too long. Change to Logged Out state
            _currentUser.State = new LoggedOutState(_currentUser);
        }
        
        public override void HandleActivity()
        {
            // User done an activity. Change to Active state and reset the timeout counter.
            _currentUser.State = new ActiveState(_currentUser);
            _currentUser.ResetStateCountDown();
        }

        public override string ToString()
        {
            return "Inactive";
        }
    }
}
