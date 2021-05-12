using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.DayProgressionSystem
{
    public class DayProgressor : MonoBehaviour
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
        
        DateTime _CurrentDateTime;

        int _CurrentMonth;
        int _CurrentDay;

        bool _IsPaused = false;

        void Awake()
        {
            _CurrentDateTime = new DateTime(_StartYear, _StartMonth, _StartDay);    

            _CurrentDay = _CurrentDateTime.Day;

            _CurrentMonth = _CurrentDateTime.Month;
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
                _CurrentDateTime = _CurrentDateTime.AddHours(_HourTimeDelta * _HourTimeScale * Time.deltaTime);

            else // if next day
            {
                _CurrentDay = _CurrentDateTime.Day; // Update day checker

                if(_CurrentMonth != _CurrentDateTime.Month) // If current month has passed
                {
                    _CurrentMonth = _CurrentDateTime.Month;

                    Calendar.instance.SetupCalendar(_CurrentDateTime); // Update Calendar
                }
                
                Calendar.instance.MarkCurrentDay(_CurrentDateTime);
            }

            Debug.Log(_CurrentDateTime);
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