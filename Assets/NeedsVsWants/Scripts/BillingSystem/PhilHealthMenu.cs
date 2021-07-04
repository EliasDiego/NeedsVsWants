using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MoneySystem;

namespace NeedsVsWants.BillingSystem
{
    public class PhilHealthMenu : BillMenu
    {
        [SerializeField]
        IncomeEvent _JobIncome;
        
        protected override string billEventName => "PhilHealth";

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override double CalculateBill(DateTime dateTime)
        {
            double bill = 0;

            if(10000.01 <= _JobIncome.incomeRate && _JobIncome.incomeRate <= 69999.99f)
                bill = _JobIncome.incomeRate * 0.035f;

            else
                bill = _JobIncome.incomeRate <= 10000 ? 350 : 2450;
                
            return bill;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}