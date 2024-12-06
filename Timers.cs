using System;

namespace CRUD_System
{
    public class Timers
    {
        // Instance properties to hold the times
        public static TimeSpan TimeOnlineStart { get; private set; }
        public static TimeSpan TimeActiveStart { get; private set; }
        public static TimeSpan TimeAwayStart { get; private set; }
        public static TimeSpan TimeBreakStart { get; private set; }
        public static TimeSpan CurrentTime { get; private set; }
        public static int CurrentYear { get; private set; } = DateTime.Now.Year;

        // Constructor to initialize the times
        public Timers()
        {
            TimeOnlineStart = DateTime.Now.TimeOfDay;
            TimeActiveStart = DateTime.Now.TimeOfDay;
            TimeAwayStart = DateTime.Now.TimeOfDay;
            TimeBreakStart = DateTime.Now.TimeOfDay;
            CurrentTime = DateTime.Now.TimeOfDay;
            CurrentYear = DateTime.Now.Year;
        }

        // Static method to set times (if needed)
        public static void SetTimes()
        {
            TimeOnlineStart = DateTime.Now.TimeOfDay;
            TimeActiveStart = DateTime.Now.TimeOfDay;
            TimeAwayStart = DateTime.Now.TimeOfDay;
            TimeBreakStart = DateTime.Now.TimeOfDay;
            CurrentTime = DateTime.Now.TimeOfDay;
            CurrentYear = DateTime.Now.Year;

            // Optionally, you can do something with these times here
        }
    }
}
