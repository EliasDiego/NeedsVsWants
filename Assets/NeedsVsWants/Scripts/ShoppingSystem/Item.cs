using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.WelfareSystem;

namespace NeedsVsWants.ShoppingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Shopping/Item")]
    public class Item : ScriptableObject
    {
        public double price;
        [TextArea]
        public string description;
        public Sprite previewImage;
        public WelfareOperator onBuyWelfareEffects;

        public virtual void OnBuy() 
        { 
            PlayerStatManager.instance.CalculateWelfare(onBuyWelfareEffects);
        }
    }
}