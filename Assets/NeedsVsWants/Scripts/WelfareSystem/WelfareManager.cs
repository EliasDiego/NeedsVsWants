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
        WelfareHandler _HealthWelfare;
        [SerializeField]
        WelfareHandler _HungerWelfare;
        [SerializeField]
        WelfareHandler _HappinessWelfare;
        [SerializeField]
        WelfareHandler _SocialWelfare;

        void Start() 
        {
            _HealthWelfare.welfareValue = PlayerStatManager.instance.currentHealthWelfare;
            _HungerWelfare.welfareValue = PlayerStatManager.instance.currentHungerWelfare;
            _HappinessWelfare.welfareValue = PlayerStatManager.instance.currentHappinessWelfare;
            _SocialWelfare.welfareValue = PlayerStatManager.instance.currentSocialWelfare;

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