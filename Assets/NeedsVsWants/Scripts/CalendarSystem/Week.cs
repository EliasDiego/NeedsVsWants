using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.CalendarSystem
{
    public class Week : MonoBehaviour
    {
        Day[] _Days;

        public Day[] days => _Days;

        void Awake()
        {
            _Days = GetComponentsInChildren<Day>();
        }

        public void ClearDays()
        {
            foreach(Day day in _Days)
            {
                day.Clear();
            }
        }
    }
}