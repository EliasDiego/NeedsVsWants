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

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        protected override bool IsWithinRange(DateTime dateTime)
        {
            int daysInMonth;

            bool isNewYear = false;

            if(dateTime.Month == dateInvested.month)
            {
                daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

                isNewYear = dateTime.Day == dateInvested.day || (daysInMonth < dateInvested.day && dateTime.Day == daysInMonth);
            }

            return isNewYear;
        }

        protected override double CalculateGainLoss(DateTime dateTime)
        {
            return capital * ((float)_Effect / (float)100);
        }
    }
}