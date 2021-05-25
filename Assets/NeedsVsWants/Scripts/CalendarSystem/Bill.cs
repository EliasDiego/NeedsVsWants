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

        public override void Invoke()
        {
            PlayerStatManager.instance.currentMoney -= _Amount;
        }
    }
}