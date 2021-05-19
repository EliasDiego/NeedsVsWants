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
            
        }
    }
}