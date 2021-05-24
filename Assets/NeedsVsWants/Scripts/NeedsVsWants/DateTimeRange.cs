using System;

namespace NeedsVsWants
{
    public struct DateTimeRange
    {
        public UDateTime min;          
        public UDateTime max;

        public DateTimeRange(DateTime minDate, DateTime maxDate)
        {
            min = minDate;
            max = maxDate;
        }

        public bool IsWithinRange(DateTime dateTime) => min <= dateTime && dateTime <= max;
    }
}