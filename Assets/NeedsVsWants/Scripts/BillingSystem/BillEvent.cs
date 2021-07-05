using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;
using System;

namespace NeedsVsWants.BillingSystem
{
    public class BillEvent : CalendarEvent
    {
        public event System.Func<DateTime, bool> onIsWithinDate;

        public override bool showOnCalendar => true;

        public override void Invoke(DateTime dateTime) { }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return onIsWithinDate?.Invoke(dateTime) ?? false;
        }
    }
}