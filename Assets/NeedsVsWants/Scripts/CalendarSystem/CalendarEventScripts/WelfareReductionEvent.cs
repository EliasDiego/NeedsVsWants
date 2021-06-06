using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.CalendarSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Calendar Events/Welfare Reduction Event")]
    public class WelfareReductionEvent : CalendarEvent
    {
        [SerializeField]
        int _DecrementHealthValue = -1;
        [SerializeField]
        int _DecrementHappinessValue = -1;
        [SerializeField]
        int _DecrementHungerValue = -1;
        [SerializeField]
        int _DecrementSocialValue = -1;

        public override bool isShowOnCalendar => false;

        WelfareValue SetValueIncrement(WelfareValue welfareValue, int increment)
        {
            welfareValue.value += increment;

            return welfareValue;
        }

        public override void Invoke(DateTime dateTime)
        {
            PlayerStatManager.instance.currentHealthWelfare = SetValueIncrement(PlayerStatManager.instance.currentHealthWelfare, _DecrementHealthValue);
            PlayerStatManager.instance.currentHappinessWelfare = SetValueIncrement(PlayerStatManager.instance.currentHappinessWelfare, _DecrementHappinessValue);
            PlayerStatManager.instance.currentHungerWelfare = SetValueIncrement(PlayerStatManager.instance.currentHungerWelfare, _DecrementHungerValue);
            PlayerStatManager.instance.currentSocialWelfare = SetValueIncrement(PlayerStatManager.instance.currentSocialWelfare, _DecrementSocialValue);
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return true;
        }
    }
}