using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Bills/Rent")]
    public class Rent : BillEvent
    {
        [SerializeField]
        float _Amount;

        public override float CalculateBill(DateTime dateTime)
        {
            return _Amount;
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return DateTime.DaysInMonth(dateTime.Year, dateTime.Month) == dateTime.Day;
        }
    }
}