using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    public class RentMenu : BillMenu
    {
        [SerializeField]
        double _Amount;
        
        protected override string billEventName => "Rent";

        public override double CalculateBill(DateTime dateTime)
        {
            return _Amount;
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}