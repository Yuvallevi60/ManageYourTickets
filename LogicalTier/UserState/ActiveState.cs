namespace LogicalTier.UserState
{
    // Part of the "State" design pattern.
    // class to be used when the current user is active and recently perform some activities on the program
    public class ActiveState : State
    {
        public ActiveState(CurrentUser currentUser) : base(currentUser)
        {
        }

        public override void HandleTimeout()
        {
            // User has not done activity for too long. Change to Inactive state and reset the time counter.
            _currentUser.State = new InactiveState(_currentUser);
            _currentUser.ResetStateCountDown();
        }

        public override void HandleActivity()
        {
            // User done an activity. reset the time counter.
            _currentUser.ResetStateCountDown();
        }

        public override string ToString()
        {
            return "Active";
        }
    }
}
