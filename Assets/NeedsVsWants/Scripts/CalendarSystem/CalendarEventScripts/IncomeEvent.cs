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
        [SerializeField]
        double _StartIncomeRate;

        public double incomeRate { get; set; }

        public override bool showOnCalendar => true;

        public override void Initialize()
        {
            incomeRate = _StartIncomeRate;
        }

        public override void Invoke(DateTime dateTime)
        {
            if(dateTime.Month == 12)
                PlayerStatManager.instance.currentMoney += GetThirteenthMonthPay();

            PlayerStatManager.instance.currentMoney += incomeRate;
        }

        public override bool IsWithinDate(DateTime dateTime) => 
            dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

        public double GetThirteenthMonthPay()
        {
            return incomeRate + 5000;
        }

        public void IncreaseIncomeRate(float percentage)
        {
            incomeRate = incomeRate + incomeRate * (percentage / 100);
        }
    }
}