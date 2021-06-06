using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.BillingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Calendar Events/Income Tax")]
    public class IncomeTax : BillEvent
    {
        [SerializeField]
        IncomeEvent _JobIncome;
        [SerializeField]
        SSS _SSS;
        [SerializeField]
        PhilHealth _PhilHealth;
        [SerializeField]
        Pagibig _Pagibig;

        public override bool IsWithinDate(DateTime dateTime)
        {
            return false;
        }

        public override float CalculateBill(DateTime dateTime)
        {
            return 0;
        }
    }
}