using System;
using System.Linq;
using System.Globalization;

using UnityEngine;

using UnityEditor;

using TMPro;

namespace NeedsVsWants.CalendarSystem
{
        
    public class Calendar : MonoBehaviour
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
                        calendar.SetUpCalendar(_Year, _Month);
                }
            }
        }
#endif

        void Awake() 
        {
            _CalendarDays = GetComponentsInChildren<CalendarDay>();

            _MonthYearText = GetComponentInChildren<TMP_Text>();
        }

        void Start() 
        {
            SetUpCalendar(2021, 5);    

            MarkCurrentDay(2021, 5, 12);
        }

        int GetWeekOfMonth(int year, int month, int day)
        {
            System.DateTime dateTime = new System.DateTime(year, month, 1);

            return Mathf.CeilToInt(((int)dateTime.DayOfWeek + day) / 7f);
        }

        public void SetUpCalendar(int year, int month)
        {
            DateTime tempDate;

            int currentMonth = month;
            
            tempDate = new DateTime(year, month, 1);
            
            _MonthYearText.text = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month) + " " + year.ToString();

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

        public void MarkCurrentDay(int year, int month, int day)
        {
            CalendarDay calendarDay = _CalendarDays.First(d => d.dateTime.Year == year && d.dateTime.Month == month && d.dateTime.Day == day);
            
            if(calendarDay)
                calendarDay.color = _CurrentDayColor;
        }
    }
}