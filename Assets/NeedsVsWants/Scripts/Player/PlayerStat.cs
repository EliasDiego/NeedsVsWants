using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.WelfareSystem;
using NeedsVsWants.ShoppingSystem;
using NeedsVsWants.CalendarSystem;
using NeedsVsWants.MessagingSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        public DateTime currentDateTime; 


        public double currentMoney;

        public WelfareValue healthWelfare;
        public WelfareValue hungerWelfare;
        public WelfareValue socialWelfare;
        public WelfareValue happinessWelfare;

        public List<Chat> chatList = new List<Chat>();

        public List<CalendarEvent> calendarEventList = new List<CalendarEvent>();
        
        public List<Item> ShopItemList = new List<Item>();

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
            
            foreach(CalendarEvent calendarEvent in Resources.LoadAll<CalendarEvent>("CalendarEvents"))
            {
                calendarEvent.Initialize();

                _Instance.calendarEventList.Add(calendarEvent);
            }

            foreach(Item item in Resources.LoadAll<Item>("CalendarEvents/SaleEvents/StartingShopItems"))
            {
                item.isDiscounted = false;

                _Instance.ShopItemList.Add(item);
            }
        }
    }
}