using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;
using System;

namespace NeedsVsWants.InvestmentSystem
{
    public class InvestmentEvent : CalendarEvent
    {
        public event System.Func<DateTime, bool> onIsWithinDate;

        public bool isShownOnCalendar { get; set; }

        public override bool showOnCalendar => isShownOnCalendar;

        public override void Invoke(DateTime dateTime) { }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return onIsWithinDate?.Invoke(dateTime) ?? false;
        }
    }
}