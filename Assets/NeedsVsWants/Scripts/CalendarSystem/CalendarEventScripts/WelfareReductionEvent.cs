using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.CalendarSystem
{
    public class WelfareReductionEvent : CalendarEvent
    {
        public override bool isShowOnCalendar => false;

        WelfareValue SetValueIncrement(WelfareValue welfareValue, int increment)
        {
            welfareValue.value += increment;

            return welfareValue;
        }

        public override void Invoke()
        {
            PlayerStatManager.instance.currentHealthWelfare = SetValueIncrement(PlayerStatManager.instance.currentHealthWelfare, -1);
            PlayerStatManager.instance.currentHappinessWelfare = SetValueIncrement(PlayerStatManager.instance.currentHappinessWelfare, -1);
            PlayerStatManager.instance.currentHungerWelfare = SetValueIncrement(PlayerStatManager.instance.currentHungerWelfare, -1);
            PlayerStatManager.instance.currentSocialWelfare = SetValueIncrement(PlayerStatManager.instance.currentSocialWelfare, -1);
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return true;
        }
    }
}