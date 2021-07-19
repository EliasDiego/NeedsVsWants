using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using NeedsVsWants.Player;
using NeedsVsWants.DropSystem;

namespace NeedsVsWants.ShoppingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Shopping/Game Object Item")]
    public class GameObjectItem : Item
    {
        [SerializeField]
        string _GameObjectName;

        GameObject _GameObject;

        public bool isAlreadyActive => FindGameObject() ? _GameObject.activeSelf : false;

        GameObject FindGameObject()
        {
            if(!_GameObject)
                _GameObject = GameObjectItemManager.instance.GetGameObject(_GameObjectName);

            return _GameObject;
        }

        public override void OnBuy()
        {
            GameObject shopItem = FindGameObject();

            DropManager.instance.SpawnDrops(onBuyWelfareEffects, 5, shopItem.transform.position);

            shopItem.SetActive(true);

            PlayerStatManager.instance.RemoveShopItem(this);
        }
    }
}