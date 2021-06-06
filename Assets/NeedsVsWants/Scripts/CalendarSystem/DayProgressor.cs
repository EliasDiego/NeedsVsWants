using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;

using TMPro;

using NeedsVsWants.Player;

namespace NeedsVsWants.CalendarSystem
{
    public class DayProgressor : MonoBehaviour
    {
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

        void Awake()
        {
            _TimeText = GetComponentInChildren<TMP_Text>();
        }

        void Start() 
        {
            DateTime startDateTime = PlayerStatManager.instance.currentDate;

            _CurrentDay = startDateTime.Day;

            _CurrentMonth = startDateTime.Month;

            _CurrentDateTime = startDateTime;
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

        void OnMonth()
        {

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
            }

            // Put here Calendar Event stuff
            foreach(CalendarEvent calendarEvent in PlayerStatManager.instance.calendarEventList)
            {
                if(calendarEvent.IsWithinDate(_CurrentDateTime))
                    calendarEvent.Invoke(_CurrentDateTime);
            }

            PlayerStatManager.instance.currentDate = _CurrentDateTime;
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