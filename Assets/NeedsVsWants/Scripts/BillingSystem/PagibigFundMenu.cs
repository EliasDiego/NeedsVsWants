using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    public class PagibigFundMenu : BillMenu
    {
        [SerializeField]
        double _Amount;

        protected override string billEventName => "Pagibig Fund";

        public override bool IsWithinDate(DateTime dateTime)
        {    
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override double CalculateBill(DateTime dateTime)
        {
            return _Amount;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}