using System;
using System.Linq;
using System.Globalization;

using UnityEngine;

using UnityEditor;

using TMPro;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.CalendarSystem
{
        
    public class Calendar : SimpleSingleton<Calendar>
    {
        [Header("Colors")]
        [SerializeField]
        Color _NotInCurrentMonthColor = Color.white;
        [SerializeField]
        Color _InCurrentMonthColor = Color.white;
        [SerializeField]
        Color _HasEventsColor = Color.white;
        [SerializeField]
        Color _CurrentDayColor = Color.white;
        
        TMP_Text _MonthYearText;

        CalendarDay _CurrentDay;

        CalendarDay[] _CalendarDays;

#if UNITY_EDITOR
        [CustomEditor(typeof(Calendar))]
        class CalendarCustomEditor : Editor
        {
            int _Year;
            int _Month;
            int _Day;

            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();

                Calendar calendar = target as Calendar;

                if(!calendar)
                    return;

                if(Application.isPlaying)
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Debug", EditorStyles.boldLabel);

                    _Year = EditorGUILayout.IntField("Year", _Year);
                    _Month = EditorGUILayout.IntField("Month", _Month);

                    if(GUILayout.Button("Setup Calendar"))
                        calendar.SetupCalendar(_Year, _Month);
                }
            }
        }
#endif

        protected override void Awake() 
        {
            base.Awake();
            
            _CalendarDays = GetComponentsInChildren<CalendarDay>();

            _MonthYearText = GetComponentInChildren<TMP_Text>();
        }

        int GetWeekOfMonth(int year, int month, int day)
        {
            System.DateTime dateTime = new System.DateTime(year, month, 1);

            return Mathf.CeilToInt(((int)dateTime.DayOfWeek + day) / 7f);
        }

        public void SetupCalendar(int year, int month)
        {
            DateTime tempDate;

            int currentMonth = month;

            _CurrentDay = null; // Reset Current Day
            
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
                    day.color = _NotInCurrentMonthColor;

                else
                    day.color = _InCurrentMonthColor;

                // Go to next day
                tempDate = tempDate.AddDays(1);
            }
        }

        public void SetupCalendar(DateTime dateTime) => SetupCalendar(dateTime.Year, dateTime.Month);

        public void MarkCurrentDay(int year, int month, int day)
        {
            CalendarDay calendarDay = _CalendarDays.First(d => d.dateTime.Year == year && d.dateTime.Month == month && d.dateTime.Day == day);
            
            if(calendarDay)
            {
                if(_CurrentDay) // Reset Day
                    _CurrentDay.color = _InCurrentMonthColor;

                _CurrentDay = calendarDay;

                _CurrentDay.color = _CurrentDayColor;
            }
        }

        public void MarkCurrentDay(DateTime dateTime) => MarkCurrentDay(dateTime.Year, dateTime.Month, dateTime.Day);
    }
}