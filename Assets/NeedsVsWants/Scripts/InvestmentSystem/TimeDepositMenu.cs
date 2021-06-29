using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;
using NeedsVsWants.CalendarSystem;

using TMPro;

namespace NeedsVsWants.InvestmentSystem
{
    public class TimeDepositMenu : InvestmentMenu
    {
        [SerializeField]
        int _Effect;

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        protected override bool IsWithinRange(DateTime dateTime)
        {
            bool isNewYear = false;

            if(dateTime.Month == dateInvested.month)
                isNewYear = dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month) || dateTime.Day == dateInvested.day;

            return isNewYear;
        }

        protected override double CalculateGainLoss(DateTime dateTime, double money)
        {
            Debug.Log(dateTime);
            return 0;
        }
    }
}