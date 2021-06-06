using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Calendar Events/SSS")]
    public class SSS : BillEvent
    {
        public override bool IsWithinDate(DateTime dateTime)
        {
            return false;
        }

        public override float CalculateBill(DateTime dateTime)
        {
            return 1125f;
        }
    }
}