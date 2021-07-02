using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.TutorialSystem
{
    public class TutorialManager : SimpleSingleton<TutorialManager>
    {
        [SerializeField]
        DayProgressor _DayProgressor;

        public DayProgressor dayProgressor => _DayProgressor;
    }
}