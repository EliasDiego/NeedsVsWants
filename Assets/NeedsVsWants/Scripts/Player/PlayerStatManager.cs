using System;
using System.Collections;
using System.Collections.Generic;

using NeedsVsWants.Patterns;
using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStatManager : SimpleSingleton<PlayerStatManager>
    {
        PlayerStat _PlayerStat;

        public DateTime currentDate 
        { 
            get => _PlayerStat.currentDateTime; 
            set
            {
                _PlayerStat.currentDateTime = value;

                onDateChange?.Invoke(_PlayerStat.currentDateTime);
            }
        }

        public List<CalendarEvent> calendarEventList => _PlayerStat.calendarEventList;

        public float currentMoney
        {
            get => _PlayerStat.currentMoney;

            set
            {
                _PlayerStat.currentMoney = value;

                onMoneyChange?.Invoke(value);
            }
        }

        public int currentYear 
        { 
            get => _PlayerStat.currentDateTime.Year;
            set
            {
                DateTime newDate = _PlayerStat.currentDateTime;

                _PlayerStat.currentDateTime = new DateTime(value, newDate.Month, newDate.Day);

                onDateChange?.Invoke(_PlayerStat.currentDateTime);
            }
        }
        public int currentMonth 
        {
            get => _PlayerStat.currentDateTime.Month;
            set
            {
                DateTime newDate = _PlayerStat.currentDateTime;

                _PlayerStat.currentDateTime = new DateTime(newDate.Year, value, newDate.Day);

                onDateChange?.Invoke(_PlayerStat.currentDateTime);
            }
        }
        public int currentDay
        {
            get => _PlayerStat.currentDateTime.Day;
            set
            {
                DateTime newDate = _PlayerStat.currentDateTime;

                _PlayerStat.currentDateTime = new DateTime(newDate.Year, newDate.Month, value);

                onDateChange?.Invoke(_PlayerStat.currentDateTime);
            }
        }

        public WelfareValue currentHealthWelfare
        {
            get => _PlayerStat.healthWelfare; 
            set
            {
                _PlayerStat.healthWelfare = value;

                onHealthChange?.Invoke(_PlayerStat.healthWelfare);
            }
        }
        // public float healthValue 
        // { 
        //     get => _PlayerStat.healthWelfare.value; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.healthWelfare;

        //         welfareValue.value = value;

        //         _PlayerStat.healthWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.healthWelfare);

        //     }
        // }
        // public float healthMaxValue
        // { 
        //     get => _PlayerStat.healthWelfare.maxValue; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.healthWelfare;

        //         welfareValue.maxValue = value;

        //         _PlayerStat.healthWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.healthWelfare);

        //     }
        // }
        
        public WelfareValue currentSocialWelfare
        {
            get => _PlayerStat.socialWelfare; 
            set
            {
                _PlayerStat.socialWelfare = value;

                onSocialChange?.Invoke(_PlayerStat.socialWelfare);
            }
        }
        // public float socialValue
        // { 
        //     get => _PlayerStat.socialWelfare.value; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.socialWelfare;

        //         welfareValue.value = value;

        //         _PlayerStat.socialWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.socialWelfare);

        //     }
        // }
        // public float socialMaxValue
        // { 
        //     get => _PlayerStat.socialWelfare.maxValue; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.socialWelfare;

        //         welfareValue.maxValue = value;

        //         _PlayerStat.socialWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.socialWelfare);

        //     }
        // }
        
        public WelfareValue currentHungerWelfare
        {
            get => _PlayerStat.hungerWelfare; 
            set
            {
                _PlayerStat.hungerWelfare = value;

                onHungerChange?.Invoke(_PlayerStat.hungerWelfare);
            }
        }

        // public float hungerValue
        // { 
        //     get => _PlayerStat.hungerWelfare.value; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.hungerWelfare;

        //         welfareValue.value = value;

        //         _PlayerStat.hungerWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.hungerWelfare);

        //     }
        // }
        // public float hungerMaxValue
        // { 
        //     get => _PlayerStat.hungerWelfare.maxValue; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.hungerWelfare;

        //         welfareValue.maxValue = value;

        //         _PlayerStat.hungerWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.hungerWelfare);

        //     }
        // }
        
        public WelfareValue currentHappinessWelfare
        {
            get => _PlayerStat.happinessWelfare; 
            set
            {
                _PlayerStat.happinessWelfare = value;

                onHappinessChange?.Invoke(_PlayerStat.happinessWelfare);
            }
        }

        // public float happinessValue
        // { 
        //     get => _PlayerStat.happinessWelfare.value; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.happinessWelfare;

        //         welfareValue.value = value;

        //         _PlayerStat.happinessWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.happinessWelfare);

        //     }
        // }
        // public float happinessMaxValue
        // { 
        //     get => _PlayerStat.happinessWelfare.maxValue; 
        //     set
        //     {
        //         WelfareValue welfareValue = _PlayerStat.happinessWelfare;

        //         welfareValue.maxValue = value;

        //         _PlayerStat.happinessWelfare = welfareValue;

        //         onHealthChange?.Invoke(_PlayerStat.happinessWelfare);
        //     }
        // }

        public event Action<float> onMoneyChange;
        public event Action<DateTime> onDateChange;
        public event Action<WelfareValue> onHealthChange;
        public event Action<WelfareValue> onHungerChange;
        public event Action<WelfareValue> onHappinessChange;
        public event Action<WelfareValue> onSocialChange;

        protected override void Awake()
        {
            base.Awake();

            _PlayerStat = PlayerStat.instance;
        }

        void Start() 
        {
            onMoneyChange?.Invoke(currentMoney);

            onDateChange?.Invoke(currentDate);

            onHealthChange?.Invoke(currentHealthWelfare);
            onHappinessChange?.Invoke(currentHappinessWelfare);
            onHungerChange?.Invoke(currentHungerWelfare);
            onSocialChange?.Invoke(currentSocialWelfare);  
        }
    }
}