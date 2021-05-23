using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;
using NeedsVsWants.Player;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareManager : MonoBehaviour
    {
        [SerializeField]
        WelfareSlider _HealthWelfare;
        [SerializeField]
        WelfareSlider _HungerWelfare;
        [SerializeField]
        WelfareSlider _HappinessWelfare;
        [SerializeField]
        WelfareSlider _SocialWelfare;

        void Start() 
        {
            PlayerStatManager.instance.onHealthChange += welfare => 
            {
                if(_HealthWelfare.value != welfare.value)
                    _HealthWelfare.value = welfare.value;
                    
                if(_HealthWelfare.maxValue != welfare.maxValue)
                    _HealthWelfare.maxValue = welfare.maxValue;
            };
            
            PlayerStatManager.instance.onHungerChange += welfare => 
            {
                if(_HungerWelfare.value != welfare.value)
                    _HungerWelfare.value = welfare.value;
                    
                if(_HungerWelfare.maxValue != welfare.maxValue)
                    _HungerWelfare.maxValue = welfare.maxValue;
            };
            
            PlayerStatManager.instance.onHappinessChange += welfare => 
            {
                if(_HappinessWelfare.value != welfare.value)
                    _HappinessWelfare.value = welfare.value;
                    
                if(_HappinessWelfare.maxValue != welfare.maxValue)
                    _HappinessWelfare.maxValue = welfare.maxValue;
            };
            
            PlayerStatManager.instance.onSocialChange += welfare => 
            {
                if(_SocialWelfare.value != welfare.value)
                    _SocialWelfare.value = welfare.value;
                    
                if(_SocialWelfare.maxValue != welfare.maxValue)
                    _SocialWelfare.maxValue = welfare.maxValue;
            };
        }
    }
}