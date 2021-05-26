using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using System;

namespace NeedsVsWants.CalendarSystem
{
    public class IncomeEvent : CalendarEvent
    {
        public float incomeRate;

        public override bool isShowOnCalendar => true;

        public override void Invoke()
        {
            PlayerStatManager.instance.currentMoney += incomeRate;
        }

        public override bool IsWithinDate(DateTime dateTime) => 
            dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        // public override bool IsWithinDateRange(DateTime dateTime) => 
        //     dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }
}