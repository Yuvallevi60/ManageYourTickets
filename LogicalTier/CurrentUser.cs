using LogicalTier.DBObjects;
using LogicalTier.UserState;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Timers;

namespace LogicalTier
{
    // a class that reprsent the current user that loged in to the program.
    public class CurrentUser : INotifyPropertyChanged
    {
        private static CurrentUser? _instance; // ststic _instance of CurrentUser, part of the Singleton design pattern

        public User User { get; set; } // the 'User' object with the details of the current loged in user.

        private UserState.State _state;
        public UserState.State State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                OnPropertyChanged();
            }
        }

        private TimeSpan _stateCountDown; // The remaining time till UserState will be changed
        public TimeSpan StateCountDown
        {
            get
            {
                return _stateCountDown;
            }
            set
            {
                _stateCountDown = value;
                OnPropertyChanged();
            }
        }

        // an inner Timer that every time its interval elapsed the 'StateCountDown' property is update
        private System.Timers.Timer _innerTimer;
        public System.Timers.Timer InnerTimer
        {
            get
            {
                return _innerTimer;
            }
            set
            {
                _innerTimer = value;
                OnPropertyChanged();
            }
        }



        // private builder,  part of the Singleton design pattern
        private CurrentUser(User user)
        {
            User = user;
            _state = new ActiveState(this);

            StateCountDown = TimeSpan.FromMilliseconds(Constants.ACTIVE_STATE_DURATION);
            _innerTimer = new System.Timers.Timer(1000);

            // When 'innerTimer' interval elapses, 'StateCountDown' will be reduce by one second.
            // If 'StateCountDown' reached zero then 'innerTimer' will stop, and HandleTimeout() rise.
            _innerTimer.Elapsed += ((object? sender, ElapsedEventArgs e) =>
            {
                StateCountDown -= TimeSpan.FromMilliseconds(InnerTimer.Interval);
                if (StateCountDown <= TimeSpan.Zero)
                {
                    StateCountDown = TimeSpan.Zero;
                    _innerTimer.Stop();
                    HandleTimeout();
                }
            });
            _innerTimer.Start();
        }



        // the methode is part of the Singleton design pattern
        // the methode returns the "_instance" property which may be null
        // if User object is given, then the methode will create new CurrentUser and save it in '_instance' before return it.
        public static CurrentUser? GetInstance(User? user = null)
        {
            if (user != null)
                _instance = new CurrentUser(user);
            
            return _instance;
        }


        // Rise when time runs out for the current 'UserState'.
        public void HandleTimeout()
        {
            State.HandleTimeout();
        }


        // Rise when the user do any activity in the program.
        public void HandleActivity()
        { 
            State.HandleActivity(); 
        }


        // Reset the StateCountDown acording to the current UserState
        public void ResetStateCountDown()
        {
            InnerTimer.Stop();
            switch (State)
            {
                case ActiveState:
                    StateCountDown = TimeSpan.FromMilliseconds(Constants.ACTIVE_STATE_DURATION);
                    break;
                case InactiveState:
                    StateCountDown = TimeSpan.FromMilliseconds(Constants.INACTIVE_STATE_DURATION);
                    break;
            }
            InnerTimer.Start();
        }


        // implement INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
