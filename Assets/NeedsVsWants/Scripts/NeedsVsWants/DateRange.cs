using System;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants
{
    [Serializable]
    public struct DateRange
    {
        public Date min;          
        public Date max;

        public DateRange(Date minDate, Date maxDate)
        {
            min = minDate;
            max = maxDate;
        }

        public bool IsWithinRange(DateTime dateTime) => min <= dateTime && dateTime <= max;
    }
}