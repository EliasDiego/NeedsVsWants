using System;

using UnityEngine;

using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        public DateTime currentDateTime { get; set; } 

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
                }

                return _Instance;
            }
        }
    }
}