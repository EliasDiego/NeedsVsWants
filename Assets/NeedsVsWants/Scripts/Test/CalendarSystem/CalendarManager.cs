using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.CalendarSystem
{
    public class CalendarManager : SimpleSingleton<CalendarManager>
    {
        DayProgressor _DayProgressor;

        public DayProgressor dayProgressor => _DayProgressor;

        protected override void Awake()
        {
            base.Awake();

            _DayProgressor = GetComponentInChildren<DayProgressor>();
        }
    }
}