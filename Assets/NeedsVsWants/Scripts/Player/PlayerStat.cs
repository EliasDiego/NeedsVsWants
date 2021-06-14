using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;
using NeedsVsWants.MessagingSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        public DateTime currentDateTime; 
        public List<CalendarEvent> calendarEventList = new List<CalendarEvent>();

        public float currentMoney;

        public WelfareValue healthWelfare;
        public WelfareValue hungerWelfare;
        public WelfareValue socialWelfare;
        public WelfareValue happinessWelfare;

        public List<Chat> chatList = new List<Chat>();
        
        static PlayerStat _Instance;

        public static PlayerStat instance 
        {
            get
            {
                if(!_Instance)
                    PlayerStat.CreateNewInstance();

                return _Instance;
            } 
            set => _Instance = value;
        }

        public static void CreateNewInstance()
        {
            PlayerStatStartReference startReference = Resources.Load<PlayerStatStartReference>("Player Stat Start Reference");

            _Instance = CreateInstance<PlayerStat>();

            _Instance.currentDateTime = startReference.startDate;
        
            _Instance.currentMoney = startReference.startMoney;
        
            _Instance.healthWelfare = startReference.startHealthWelfare;
            _Instance.hungerWelfare = startReference.startHungerWelfare;
            _Instance.happinessWelfare = startReference.startHappinessWelfare;
            _Instance.socialWelfare = startReference.startSocialWelfare;

            _Instance.calendarEventList.AddRange(Resources.LoadAll<CalendarEvent>("CalendarEvents")); 
            
            foreach(CalendarEvent calendarEvent in _Instance.calendarEventList)
                calendarEvent.Initialize();

            // For Testing, To Be Deleted
            // PlayerStat.instance.currentDateTime = new DateTime(2020, 1, 1);
            
            // PlayerStat.instance.currentMoney = 10000;
            
            // PlayerStat.instance.healthWelfare = new WelfareSystem.WelfareValue(100, 100);
            // PlayerStat.instance.hungerWelfare = new WelfareSystem.WelfareValue(100, 100);
            // PlayerStat.instance.happinessWelfare = new WelfareSystem.WelfareValue(100, 100);
            // PlayerStat.instance.socialWelfare = new WelfareSystem.WelfareValue(100, 100);

            // PlayerStat.instance.calendarEventList.AddRange(Resources.LoadAll<CalendarEvent>("CalendarEvents")); 

            // foreach(CalendarEvent calendarEvent in PlayerStat.instance.calendarEventList)
            //     calendarEvent.Initialize();
        }
    }
}