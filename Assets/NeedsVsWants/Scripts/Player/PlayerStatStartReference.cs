using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.WelfareSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.Player
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Player/Player Stat Start Reference", fileName = "Player Stat Start Reference")]
    public class PlayerStatStartReference : ScriptableObject
    {
        [Header("Starting Date")]
        public Date startDate;

        [Header("Starting Money")]
        public double startMoney;

        [Header("Starting Welfare")]
        public WelfareValue startHealthWelfare;
        public WelfareValue startHappinessWelfare;
        public WelfareValue startHungerWelfare;
        public WelfareValue startSocialWelfare;
    }
}