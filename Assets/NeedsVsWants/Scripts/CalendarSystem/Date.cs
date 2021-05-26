using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.CalendarSystem
{
    [Serializable]
    public struct Date
    {
        public int year;
        public int month;
        public int day;

        public Date(int year, int month, int day)
        {
            this.year = Mathf.Clamp(year, 1, 9999);
            this.month = Mathf.Clamp(month, 1, 12);
            this.day = Mathf.Clamp(day, 1, DateTime.DaysInMonth(this.year, this.month));
        }

        public static implicit operator DateTime (Date date) 
        {
            int year = Mathf.Clamp(date.year, 1, 9999);
            int month = Mathf.Clamp(date.month, 1, 12);
            int day = Mathf.Clamp(date.day, 1, DateTime.DaysInMonth(year, month));

            return new DateTime(year, month, day);
        }
        
        // public static implicit operator Date (DateTime dateTime)
        // {
        //     return new Date(dateTime.Year, dateTime.Month, dateTime.Day);
        // }

        // public static bool operator > (Date lhs, Date rhs)
        // {
        //     return lhs.year > rhs.year && lhs.month > rhs.month && lhs.day > rhs.day;
        // }

        // public static bool operator < (Date lhs, Date rhs)
        // {
        //     return lhs.year < rhs.year && lhs.month < rhs.month && lhs.day < rhs.day;
        // }
        
        // public static bool operator >= (Date lhs, Date rhs)
        // {
        //     return lhs.year >= rhs.year && lhs.month >= rhs.month && lhs.day >= rhs.day;
        // }

        // public static bool operator <= (Date lhs, Date rhs)
        // {
        //     return lhs.year <= rhs.year && lhs.month <= rhs.month && lhs.day <= rhs.day;
        // }
    }
}