using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Bills/SSS")]
    public class SSS : BillEvent
    {
        [SerializeField]
        double _Amount;

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override double CalculateBill(DateTime dateTime)
        {
            return _Amount;
        }
    }
}