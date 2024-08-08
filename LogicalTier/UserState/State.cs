namespace LogicalTier.UserState
{
    // Class that implement the "State" design pattern and represent the state of the current user that use the program
    public abstract class State
    {
        protected CurrentUser _currentUser;

        public State(CurrentUser currentUser) 
        { 
            this._currentUser = currentUser;
        }

        public abstract void HandleTimeout(); // rise when time runs out for the current state
        
        public abstract void HandleActivity(); // rise when the user do any activity in the program
    }
}
