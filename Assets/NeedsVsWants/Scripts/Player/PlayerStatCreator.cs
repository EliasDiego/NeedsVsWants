using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.Player
{
    public class PlayerStatCreator : SimpleSingleton<PlayerStatCreator>
    {
        [Header("Starting Date")]
        [SerializeField]
        int _Year;
        [SerializeField]
        int _Month;
        [SerializeField]
        int _Day;

        [Header("Starting Money")]
        [SerializeField]
        float _Money;

        [Header("Starting Welfare")]
        [SerializeField]
        float _HealthValue;
        [SerializeField]
        float _HealthMaxValue;

        [Space]
        [SerializeField]
        float _HappinessValue;
        [SerializeField]
        float _HappinessMaxValue;
        
        [Space]
        [SerializeField]
        float _HungerValue;
        [SerializeField]
        float _HungerMaxValue;
        
        [Space]
        [SerializeField]
        float _SocialValue;
        [SerializeField]
        float _SocialMaxValue;
        
        public void CreateFreshPlayerStat()
        {
            PlayerStat.instance.currentDateTime = new System.DateTime(_Year, _Month, _Day);

            PlayerStat.instance.currentMoney = _Money;

            PlayerStat.instance.healthValue = new WelfareSystem.WelfareValue(_HealthValue, _HealthMaxValue);
            PlayerStat.instance.hungerValue = new WelfareSystem.WelfareValue(_HungerValue, _HungerMaxValue);
            PlayerStat.instance.happinessValue = new WelfareSystem.WelfareValue(_HappinessValue, _HappinessMaxValue);
            PlayerStat.instance.socialValue = new WelfareSystem.WelfareValue(_SocialValue, _SocialMaxValue);
        }

        public void LoadPlayerStat(int slot)
        {

        }
    }
}