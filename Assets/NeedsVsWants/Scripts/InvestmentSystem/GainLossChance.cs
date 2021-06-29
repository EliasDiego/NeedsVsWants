using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MoneySystem;

namespace NeedsVsWants.InvestmentSystem
{
    [System.Serializable]
    public struct GainLossChance
    {
        public MoneyOperator _Effect;
        public int chanceOfHappening;
    }
}