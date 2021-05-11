using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEditor;

namespace NeedsVsWants.CalendarSystem
{
        
    public class Calendar : MonoBehaviour
    {
        Week[] _Weeks;

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
                    _Year = EditorGUILayout.IntField("Year", _Year);
                    _Month = EditorGUILayout.IntField("Month", _Month);

                    if(GUILayout.Button("Setup Calendar"))
                        calendar.SetupCalendar(_Year, _Month);
                }
            }
        }
#endif

        void Awake() 
        {
            _Weeks = GetComponentsInChildren<Week>();
        }

        void Start() 
        {
            SetupCalendar(2021, 5);    
        }
        
        int GetWeekOfMonth(int year, int month, int day)
        {
            System.DateTime dateTime = new System.DateTime(year, month, 1);

            return Mathf.CeilToInt(((int)dateTime.DayOfWeek + day) / 7f);
        }

        public void SetupCalendar(int year, int month)
        {
            DateTime dateTime;

            int tempMonth;
            int tempYear;

            int daysInMonth = 0;

            int currentWeek = 0;

            foreach(Week week in _Weeks)
                week.ClearDays();

            // For current Month
            for(int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            {
                dateTime = new DateTime(year, month, day);

                _Weeks[GetWeekOfMonth(year, month, day) - 1].days[(int)dateTime.DayOfWeek].day = dateTime.Day;
            }

            // For previous Month 
            tempMonth = month - 1;
            tempYear = year;

            if(tempMonth < 1)
            {
                tempMonth = 12;

                tempYear -= 1;
            }

            daysInMonth = DateTime.DaysInMonth(tempYear, tempMonth);

            dateTime = new DateTime(year, month, 1);

            if(dateTime.DayOfWeek != DayOfWeek.Sunday)
            {
                for(int dayOfWeek = (int)dateTime.DayOfWeek; dayOfWeek > 0; dayOfWeek--)
                {
                    _Weeks[0].days[dayOfWeek - 1].day = daysInMonth;
                    
                    daysInMonth--;
                }
            }

            // For Next Month
 

            // V1
            // foreach(Week week in _Weeks)
            //     week.ClearDays();

            // for(int day = 1; day <= DateTime.DaysInMonth(year, month); day++)
            // {
            //     dateTime = new DateTime(year, month, day);

            //     _Weeks[GetWeekOfMonth(year, month, day) - 1].days[(int)dateTime.DayOfWeek].day = dateTime.Day;
            // }
        }

        public void MarkCurrentDay(int year, int month, int day)
        {

        }
    }
}