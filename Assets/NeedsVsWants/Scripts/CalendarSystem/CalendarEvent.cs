using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.CalendarSystem
{
    public abstract class CalendarEvent : ScriptableObject
    {
        public abstract bool showOnCalendar { get; }

        public virtual void Initialize() { }

        public abstract void Invoke(DateTime dateTime);

        public abstract bool IsWithinDate(DateTime dateTime);
    }
}