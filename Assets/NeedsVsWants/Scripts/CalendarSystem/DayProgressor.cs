using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

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

        Action _CurrentEvent;

        TMP_Text _TimeText;

        public static float hourTimeScale { get; set; }

        void Awake()
        {
            _TimeText = GetComponentInChildren<TMP_Text>();

            Unpause();
        }

        void Start() 
        {
            DateTime startDateTime = PlayerStatManager.instance.currentDate;

            _CurrentDay = startDateTime.Day;

            _CurrentMonth = startDateTime.Month;

            _CurrentDateTime = startDateTime;
            
            // Invoke calendar events and Initialize stuff
            OnNextDay();
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

            hour = _CurrentDateTime.Hour + 1;

            if(hour > 12 )
                hour = hour - 12;
                
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
            foreach(CalendarEvent calendarEvent in PlayerStatManager.instance.calendarEvents)
            {
                if(calendarEvent.IsWithinDate(_CurrentDateTime))
                    calendarEvent.Invoke(_CurrentDateTime);
            }

            PlayerStatManager.instance.currentDate = _CurrentDateTime;
        }

        public void Skip()
        {
            _IsPaused = false;

            hourTimeScale = _HourTimeScale = 2;

            _CurrentEvent = Skip;
        }

        public void Pause()
        {
            _IsPaused = true;

            _CurrentEvent = Pause;
        }

        public void Unpause()
        {
            _IsPaused = false;

            hourTimeScale = _HourTimeScale = 1;
            
            _CurrentEvent = Unpause;
        }

        public void Stop()
        {
            _IsPaused = true;
        }
        
        public void Resume()
        {
            _CurrentEvent?.Invoke();
        }
    }
}