using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Calendar Events/Pagibig")]
    public class Pagibig : BillEvent
    {
        public override bool IsWithinDate(DateTime dateTime)
        {    
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override float CalculateBill(DateTime dateTime)
        {
            return 100;
        }
    }
}