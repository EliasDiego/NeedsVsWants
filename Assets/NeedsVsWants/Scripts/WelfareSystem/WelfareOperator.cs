using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.WelfareSystem
{
    [System.Serializable]
    public class WelfareOperator
    {
        [SerializeField]
        AmountType _AmountType;
        [SerializeField]
        int _HealthValue = 0;
        [SerializeField]
        int _HappinessValue = 0;
        [SerializeField]
        int _HungerValue = 0;
        [SerializeField]
        int _SocialValue = 0;

        public AmountType amountType => _AmountType;

        public int healthValue => _HealthValue;
        public int happinessValue => _HappinessValue;
        public int hungerValue => _HungerValue;
        public int socialValue => _SocialValue;

        WelfareValue CalculateWelfare(WelfareValue welfareValue, int value)
        {
            switch(_AmountType)
            {
                case AmountType.Value:
                    welfareValue.value = Mathf.Clamp(welfareValue.value + value, 0, welfareValue.maxValue);
                    break;
                
                case AmountType.Percentage:
                    welfareValue.value = Mathf.Clamp(welfareValue.value + Mathf.RoundToInt(welfareValue.value * (value / 100)), 0, welfareValue.maxValue);
                    break;
            }

            return welfareValue;
        }

        public WelfareValue GetHealth(WelfareValue welfareValue) => CalculateWelfare(welfareValue, _HealthValue);
        public WelfareValue GetHappiness(WelfareValue welfareValue) => CalculateWelfare(welfareValue, _HappinessValue);
        public WelfareValue GetHunger(WelfareValue welfareValue) => CalculateWelfare(welfareValue, _HungerValue);
        public WelfareValue GetSocial(WelfareValue welfareValue) => CalculateWelfare(welfareValue, _SocialValue);
    }    
}