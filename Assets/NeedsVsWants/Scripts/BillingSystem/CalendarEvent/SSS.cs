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
        float _Amount;

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }

        public override float CalculateBill(DateTime dateTime)
        {
            return _Amount;
        }
    }
}