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
        WelfareOperator _WelfareOperator;

        public override bool showOnCalendar => false;

        public override void Invoke(DateTime dateTime)
        {
            PlayerStatManager.instance.currentHealthWelfare = _WelfareOperator.GetHealth(PlayerStatManager.instance.currentHealthWelfare);
            PlayerStatManager.instance.currentHappinessWelfare =_WelfareOperator.GetHappiness(PlayerStatManager.instance.currentHappinessWelfare);
            PlayerStatManager.instance.currentHungerWelfare = _WelfareOperator.GetHunger(PlayerStatManager.instance.currentHungerWelfare);
            PlayerStatManager.instance.currentSocialWelfare = _WelfareOperator.GetSocial(PlayerStatManager.instance.currentSocialWelfare);
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return true;
        }
    }
}