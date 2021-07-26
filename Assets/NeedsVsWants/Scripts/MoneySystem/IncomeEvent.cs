using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MoneySystem
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
            double income = 0;

            if(dateTime.Month == 12)
                income += GetThirteenthMonthPay();

            income += incomeRate;

            DropSystem.DropManager.instance.SpawnDropsOnAnne(income, 5);
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