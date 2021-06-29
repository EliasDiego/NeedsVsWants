using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

namespace NeedsVsWants.InvestmentSystem
{
    public class StocksMenu : InvestmentMenu
    {
        [SerializeField]
        GainLossChance[] _GainLossChances;

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
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
            bool isNextMonth = false;

            if(dateTime.Month == dateInvested.month)
                isNextMonth = dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month) || dateTime.Day == dateInvested.day;

            return isNextMonth;
        }

        protected override double CalculateGainLoss(DateTime dateTime, double money)
        {
            return 0;
        }
    }
}