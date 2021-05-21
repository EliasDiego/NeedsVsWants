using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;
using NeedsVsWants.CalendarSystem;

using TMPro;

using NeedsVsWants.Player;

namespace NeedsVsWants.DayProgressionSystem
{
    public class DayProgressor : SimpleSingleton<DayProgressor>
    {
        [Header("Day Speed")]
        [SerializeField]
        float _HourTimeDelta = 1;
        [SerializeField]
        float _HourTimeScale = 1;
        
        int _CurrentMonth;
        int _CurrentDay;

        bool _IsPaused = false;
        
        //DateTime _CurrentDateTime;

        TMP_Text _TimeText;

        protected override void Awake()
        {
            base.Awake(); 

            DateTime dateTime = PlayerStatManager.instance.currentDateTime;  

            _CurrentDay = dateTime.Day;

            _CurrentMonth = dateTime.Month;

            _TimeText = GetComponentInChildren<TMP_Text>();
        }

        void Start() 
        {
            DateTime dateTime = PlayerStatManager.instance.currentDateTime;  

            Calendar.instance.SetupCalendar(PlayerStatManager.instance.currentDateTime);
            Calendar.instance.MarkCurrentDay(PlayerStatManager.instance.currentDateTime);
        }

        void Update()
        {
            if(_IsPaused)
                return;

            if(_CurrentDay == PlayerStatManager.instance.currentDateTime.Day) // if still the same day
                OnSameDay();

            else // if next day
                OnNextDay();
        }

        void OnSameDay()
        {
            int hour;

            DateTime dateTime = PlayerStatManager.instance.currentDateTime;  

            dateTime = dateTime.AddHours(_HourTimeDelta * _HourTimeScale * Time.deltaTime);

            if(dateTime.Hour > 12)
            {
                hour = dateTime.Hour - 12;

                if(hour <= 0)
                    hour = 1; 
            }

            else
                hour = dateTime.Hour;
            
            _TimeText.text = (hour < 10 ? "0" : "") + hour + ":00" + (dateTime.Hour > 12 ? " PM" : " AM");

            PlayerStatManager.instance.currentDateTime = dateTime;
        }

        void OnNextDay()
        {
            DateTime dateTime = PlayerStatManager.instance.currentDateTime;  

            _CurrentDay = dateTime.Day; // Update day checker

            if(_CurrentMonth != dateTime.Month) // If current month has passed
            {
                _CurrentMonth = dateTime.Month;

                Calendar.instance.SetupCalendar(dateTime); // Update Calendar
            }
            
            Calendar.instance.MarkCurrentDay(dateTime);

            PlayerStatManager.instance.currentDateTime = dateTime;
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