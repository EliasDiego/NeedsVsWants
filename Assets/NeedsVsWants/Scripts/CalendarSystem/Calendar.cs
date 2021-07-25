using System;
using System.Linq;
using System.Globalization;

using UnityEngine;

using NeedsVsWants.Player;

using TMPro;

namespace NeedsVsWants.CalendarSystem
{
    public class Calendar : MonoBehaviour
    {
        [Header("Colors")]
        [SerializeField]
        Color _NotInMonthColor = Color.white;
        [SerializeField]
        Color _InMonthColor = Color.white;
        [SerializeField]
        Color _HasEventsColor = Color.white;
        [SerializeField]
        Color _CurrentDayColor = Color.white;

        [Header("Text")]
        [SerializeField]
        TMP_Text _MonthYearText;

        CalendarDay _MarkedCurrentDay;

        CalendarDay[] _CalendarDays;

        DateTime _CurrentDisplayMonthYear;

        DateTime _CurrentDisplayDate;

        public DateTime currentDisplayDate 
        { 
            get => _CurrentDisplayDate;
            set
            {
                MarkCurrentDay(value);

                _CurrentDisplayDate = value;
            } 
        }

        void Start()
        {
            _CalendarDays = GetComponentsInChildren<CalendarDay>();

            PlayerStatManager.instance.onDateChange += date => 
            {
                if(!_CurrentDisplayDate.IsOnSameMonth(date, true))
                    SetupCalendar(date);

                currentDisplayDate = date;
            };

            SetupCalendar(PlayerStatManager.instance.currentDate);

            currentDisplayDate = PlayerStatManager.instance.currentDate;
        }

        int GetWeekOfMonth(int year, int month, int day)
        {
            System.DateTime dateTime = new System.DateTime(year, month, 1);

            return Mathf.CeilToInt(((int)dateTime.DayOfWeek + day) / 7f);
        }

        void MarkDay(int year, int month, int day)
        {

        }

        void MarkCurrentDay(CalendarDay calendarDay)
        {
            if(_MarkedCurrentDay) // Reset Day
                _MarkedCurrentDay.color = _InMonthColor;

            _MarkedCurrentDay = calendarDay; // switch to new date

            _MarkedCurrentDay.color = _CurrentDayColor;
        }

        void MarkCurrentDay(int year, int month, int day)
        {
            if(_CurrentDisplayMonthYear.Year != year || _CurrentDisplayMonthYear.Month != month)
                return;

            CalendarDay calendarDay = _CalendarDays.First(d => d.dateTime.Year == year && d.dateTime.Month == month && d.dateTime.Day == day);
            
            if(calendarDay)
                MarkCurrentDay(calendarDay);
        }

        void MarkCurrentDay(DateTime dateTime) => MarkCurrentDay(dateTime.Year, dateTime.Month, dateTime.Day);

        bool IsThereEvent(DateTime dateTime)
        {
            foreach(CalendarEvent calendarEvent in PlayerStatManager.instance.calendarEvents)
            {
                if(calendarEvent.showOnCalendar && calendarEvent.IsWithinDate(dateTime))
                    return true;
            }

            return false;
        }

        public void SetupCalendar(int year, int month)
        {
            DateTime tempDate;

            int currentMonth = month;

            _CurrentDisplayMonthYear = new DateTime(year, month, 1); // Update Current Displayed Month & Year

            _MarkedCurrentDay = null; // Reset Current Day, to return previous current day to normal colors
            
            tempDate = new DateTime(year, month, 1); // Get First Day of the current Month
            
            _MonthYearText.text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + " " + year.ToString(); // Write the current month and year

            // Check if prev month can be seen
            if(tempDate.DayOfWeek != DayOfWeek.Sunday)
            { 
                month -= 1;
                
                // If Month is less than January
                if(month < 1)
                {
                    month = 12;

                    year -= 1;
                }

                // Set start date, to draw, at the prev month date where it can be seen
                tempDate = new DateTime(year, month, DateTime.DaysInMonth(year, month) - ((int)tempDate.DayOfWeek) + 1);
            }

            foreach(CalendarDay day in _CalendarDays)
            {
                // Save a copy of the date to CalendarDay
                day.dateTime = tempDate;

                
                // If not current month
                if(tempDate.Month != currentMonth)
                {
                    day.isWithinMonth = false;

                    day.color = _NotInMonthColor;
                }

                else
                {
                    day.isWithinMonth = true;

                    day.calendarEvents = PlayerStatManager.instance.calendarEvents.Where(calendarEvent => calendarEvent.showOnCalendar && 
                        calendarEvent.IsWithinDate(day.dateTime))?.ToArray();
                    
                    if(tempDate.IsOnSameDay(currentDisplayDate, true))
                    {
                        day.color = _CurrentDayColor;

                        _MarkedCurrentDay = day;
                    }

                    else
                    {
                        if(day.calendarEvents.Length > 0 && tempDate > _CurrentDisplayDate) // If there is an event and has not yet passed the current displayed date
                            day.color = _HasEventsColor;

                        else
                            day.color = _InMonthColor;
                    } 
                } 

                // Go to next day
                tempDate = tempDate.AddDays(1);
            }
        }

        public void SetupCalendar(DateTime dateTime) => SetupCalendar(dateTime.Year, dateTime.Month);

        public void DisplayNextMonth() => SetupCalendar(_CurrentDisplayMonthYear.AddMonths(1));

        public void DisplayPreviousMonth() => SetupCalendar(_CurrentDisplayMonthYear.AddMonths(-1));
    }
}