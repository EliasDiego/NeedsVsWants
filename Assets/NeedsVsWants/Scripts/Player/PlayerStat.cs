using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.Player
{
    public class PlayerStat : ScriptableObject
    {
        public DateTime currentDateTime { get; set; } = new DateTime(2020, 1, 1);

        public float currentMoney { get; set; } = 10000;

        public WelfareValue healthValue;
        public WelfareValue hungerValue;
        public WelfareValue socialValue;
        public WelfareValue happinessValue;

        static PlayerStat _Instance;

        public static PlayerStat instance 
        {
            get
            {
                if(!_Instance)
                    _Instance = CreateInstance<PlayerStat>();

                return _Instance;
            }
        }
    }
}