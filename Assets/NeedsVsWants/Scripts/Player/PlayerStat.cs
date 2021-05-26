using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        List<CalendarEvent> _CalendarEventList = new List<CalendarEvent>();

        public DateTime currentDateTime { get; set; } 
        public List<CalendarEvent> calendarEventList => _CalendarEventList;

        public float currentMoney { get; set; }

        public WelfareValue healthWelfare { get; set; }
        public WelfareValue hungerWelfare  { get; set; }
        public WelfareValue socialWelfare  { get; set; }
        public WelfareValue happinessWelfare  { get; set; }
        
        static PlayerStat _Instance;

        public static PlayerStat instance 
        {
            get
            {
                if(!_Instance)
                {
                    _Instance = CreateInstance<PlayerStat>();

                    // For Testing, To Be Deleted
                    PlayerStat.instance.currentDateTime = new DateTime(2020, 1, 1);
                    
                    PlayerStat.instance.currentMoney = 10000;
                    
                    PlayerStat.instance.healthWelfare = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.hungerWelfare = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.happinessWelfare = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.socialWelfare = new WelfareSystem.WelfareValue(100, 100);

                    PlayerStat.instance.calendarEventList.AddRange(Resources.LoadAll<CalendarEvent>("CalendarEvents"));
                    
                    // For Testing As usual
                    IncomeEvent baseIncome = IncomeEvent.CreateInstance<IncomeEvent>();

                    baseIncome.name = "Base Income";
                    baseIncome.incomeRate = 5000;
                    
                    PlayerStat.instance.calendarEventList.Add(baseIncome);

                    // For testing as usual x2
                    WelfareReductionEvent welfareReductionEvent = WelfareReductionEvent.CreateInstance<WelfareReductionEvent>();

                    welfareReductionEvent.name = "Welfare Reduction";
                    
                    PlayerStat.instance.calendarEventList.Add(welfareReductionEvent);
                    
                }

                return _Instance;
            }
        }
    }
}