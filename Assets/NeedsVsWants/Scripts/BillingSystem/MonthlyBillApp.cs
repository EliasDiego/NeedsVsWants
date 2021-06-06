using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants;
using NeedsVsWants.CalendarSystem;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.Player;

using TMPro;

namespace NeedsVsWants.BillingSystem
{
    public class MonthlyBillApp : Menu
    {
        [SerializeField]
        TMP_Dropdown _Dropdown;

        CalendarEvent[] _BillEvents;

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _BillEvents = PlayerStatManager.instance.calendarEventList.Where(calendarEvent => calendarEvent.GetType().IsSubclassOf(typeof(BillEvent))).ToArray();

            _Dropdown.options = _BillEvents.Select(billEvent => new TMP_Dropdown.OptionData(billEvent.name)).ToList();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}