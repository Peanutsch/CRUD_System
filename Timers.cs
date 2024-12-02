using System;

namespace CRUD_System
{
    public class Timers
    {
        // Instance properties to hold the times
        public TimeSpan TimeOnlineStart { get; private set; }
        public TimeSpan TimeActiveStart { get; private set; }
        public TimeSpan TimeAwayStart { get; private set; }
        public TimeSpan TimeBreakStart { get; private set; }
        public TimeSpan CurrentTime { get; private set; }

        // Constructor to initialize the times
        public Timers()
        {
            TimeOnlineStart = DateTime.Now.TimeOfDay;
            TimeActiveStart = DateTime.Now.TimeOfDay;
            TimeAwayStart = DateTime.Now.TimeOfDay;
            TimeBreakStart = DateTime.Now.TimeOfDay;
            CurrentTime = DateTime.Now.TimeOfDay;
        }

        // Static method to set times (if needed)
        public static void SetTimes()
        {
            TimeSpan timeOnlineStart = DateTime.Now.TimeOfDay;
            TimeSpan timeActiveStart = DateTime.Now.TimeOfDay;
            TimeSpan timeAwayStart = DateTime.Now.TimeOfDay;
            TimeSpan timeBreakStart = DateTime.Now.TimeOfDay;
            TimeSpan currentTime = DateTime.Now.TimeOfDay;

            // Optionally, you can do something with these times here
        }
    }
}
