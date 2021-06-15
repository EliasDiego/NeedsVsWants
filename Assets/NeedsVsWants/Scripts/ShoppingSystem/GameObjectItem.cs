using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.ShoppingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Shopping/Game Object Item")]
    public class GameObjectItem : Item
    {
        public GameObject gameObject;

        public override void OnBuy()
        {
            base.OnBuy();

            gameObject.SetActive(true);

            // Tell To Player Stat about this
        }
    }
}