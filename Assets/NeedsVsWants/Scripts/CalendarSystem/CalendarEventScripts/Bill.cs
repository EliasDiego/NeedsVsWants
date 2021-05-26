using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;

namespace NeedsVsWants.CalendarSystem
{
    [CreateAssetMenu(menuName = "NeedsVSWants/CalendarEvent/Bill")]
    public class Bill : CalendarEvent
    {
        [Header("Bill")]
        [SerializeField]
        string _Name;
        [SerializeField]
        float _Amount;
        [SerializeField]
        Date _MinDate;
        [SerializeField]
        Date _MaxDate;

        public override bool isShowOnCalendar => true;

        public override void Invoke()
        {
            PlayerStatManager.instance.currentMoney -= _Amount;
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            DateTime min = new DateTime(_MinDate.year, _MinDate.month, _MinDate.day);
            DateTime max = new DateTime(_MaxDate.year, _MaxDate.month, _MaxDate.day, 23, 59, 59);

            return min <= dateTime && dateTime <= max;
        }
    }
}