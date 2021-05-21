using System;

using UnityEngine;

using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        public DateTime currentDateTime { get; set; } 

        public float currentMoney { get; set; }

        public WelfareValue healthValue { get; set; }
        public WelfareValue hungerValue  { get; set; }
        public WelfareValue socialValue  { get; set; }
        public WelfareValue happinessValue  { get; set; }
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
                    
                    PlayerStat.instance.healthValue = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.hungerValue = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.happinessValue = new WelfareSystem.WelfareValue(100, 100);
                    PlayerStat.instance.socialValue = new WelfareSystem.WelfareValue(100, 100);
                }

                return _Instance;
            }
        }
    }
}