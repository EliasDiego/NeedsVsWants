using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MoneySystem;

namespace NeedsVsWants.ShoppingSystem
{
    [System.Serializable]
    public struct DiscountItem
    {
        public Item item;
        public MoneyOperator discount;
    }
}