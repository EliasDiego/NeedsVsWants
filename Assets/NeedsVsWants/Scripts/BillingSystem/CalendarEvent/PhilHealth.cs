using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Bills/PhilHealth")]
    public class PhilHealth : BillEvent
    {
        [SerializeField]
        IncomeEvent _JobIncome;
        
        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override float CalculateBill(DateTime dateTime)
        {
            float bill = 0;

            if(10000.01 <= _JobIncome.incomeRate && _JobIncome.incomeRate <= 69999.99f)
                bill = _JobIncome.incomeRate * 0.035f;

            else
                bill = _JobIncome.incomeRate <= 10000 ? 350 : 2450;
                
            return bill;
        }
    }
}