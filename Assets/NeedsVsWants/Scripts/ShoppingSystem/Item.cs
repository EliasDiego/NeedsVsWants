using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.DropSystem;
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

        [HideInInspector]
        public bool isDiscounted;
        [HideInInspector]
        public double discountPrice;

        public virtual void OnBuy() 
        { 
            DropManager.instance.SpawnDropsOnAnne(onBuyWelfareEffects, 5);
        }
    }
}