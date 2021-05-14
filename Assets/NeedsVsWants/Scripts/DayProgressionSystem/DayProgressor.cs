using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;
using NeedsVsWants.CalendarSystem;

using TMPro;

namespace NeedsVsWants.DayProgressionSystem
{
    public class DayProgressor : SimpleSingleton<DayProgressor>
    {
        [Header("Start Date")]
        [SerializeField]
        int _StartYear;
        [SerializeField]
        int _StartMonth;
        [SerializeField]
        int _StartDay;

        [Header("Day Speed")]
        [SerializeField]
        float _HourTimeDelta = 1;
        [SerializeField]
        float _HourTimeScale = 1;
        
        int _CurrentMonth;
        int _CurrentDay;

        bool _IsPaused = false;
        
        DateTime _CurrentDateTime;

        TMP_Text _TimeText;

        public DateTime currentDateTime => _CurrentDateTime;

        protected override void Awake()
        {
            base.Awake();

            _CurrentDateTime = new DateTime(_StartYear, _StartMonth, _StartDay);    

            _CurrentDay = _CurrentDateTime.Day;

            _CurrentMonth = _CurrentDateTime.Month;

            _TimeText = GetComponentInChildren<TMP_Text>();
        }

        void Start() 
        {
            Calendar.instance.SetupCalendar(_CurrentDateTime);
            Calendar.instance.MarkCurrentDay(_CurrentDateTime);
        }

        void Update()
        {
            if(_IsPaused)
                return;

            if(_CurrentDay == _CurrentDateTime.Day) // if still the same day
                OnSameDay();

            else // if next day
                OnNextDay();
        }

        void OnSameDay()
        {
            int hour;

            _CurrentDateTime = _CurrentDateTime.AddHours(_HourTimeDelta * _HourTimeScale * Time.deltaTime);

            if(_CurrentDateTime.Hour > 12)
            {
                hour = _CurrentDateTime.Hour - 12;

                if(hour <= 0)
                    hour = 1; 
            }

            else
                hour = _CurrentDateTime.Hour;
            
            _TimeText.text = (hour < 10 ? "0" : "") + hour + ":00" + (_CurrentDateTime.Hour > 12 ? " PM" : " AM");
        }

        void OnNextDay()
        {
            _CurrentDay = _CurrentDateTime.Day; // Update day checker

            if(_CurrentMonth != _CurrentDateTime.Month) // If current month has passed
            {
                _CurrentMonth = _CurrentDateTime.Month;

                Calendar.instance.SetupCalendar(_CurrentDateTime); // Update Calendar
            }
            
            Calendar.instance.MarkCurrentDay(_CurrentDateTime);
        }

        public void SetTimeScale(float scale) => _HourTimeScale = scale;

        public void Pause()
        {
            _IsPaused = true;
        }

        public void Unpause()
        {
            _IsPaused = false;

            _HourTimeScale = 1;
        }
    }
}