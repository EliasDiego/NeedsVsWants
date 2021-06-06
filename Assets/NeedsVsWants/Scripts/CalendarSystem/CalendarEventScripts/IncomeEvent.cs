using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using System;

namespace NeedsVsWants.CalendarSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Calendar Events/Income Event")]
    public class IncomeEvent : CalendarEvent
    {
        public float incomeRate;

        public override bool isShowOnCalendar => true;

        public override void Invoke(DateTime dateTime)
        {
            if(dateTime.Month == 12)
                PlayerStatManager.instance.currentMoney += incomeRate + 5000;

            PlayerStatManager.instance.currentMoney += incomeRate;
        }

        public override bool IsWithinDate(DateTime dateTime) => 
            dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        public float GetThirteenthMonthPay()
        {
            return 0;
        }

        // public override bool IsWithinDateRange(DateTime dateTime) => 
        //     dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }
}