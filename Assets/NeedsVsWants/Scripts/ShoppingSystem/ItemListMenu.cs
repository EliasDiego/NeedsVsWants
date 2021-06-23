using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemListMenu : Menu
    {
        [SerializeField]
        VerticalLayoutGroup _ContentLayoutGroup;
        [SerializeField]
        RectTransform _LeftList;
        [SerializeField]
        RectTransform _RightList;
        [SerializeField]
        ItemViewerMenu _ItemViewerMenu;

        Item[] _Item;

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Item Button");    
        }

        async void UpdateItemList()
        {
            AppMenuGroup appMenuGroup = GetComponentInParent<AppMenuGroup>();

            Item[] items = Player.PlayerStatManager.instance.currentShopItemList;

            ItemButton[] itemButtons = ObjectPoolManager.instance.GetObjects("Item Button", items.Length).
                Select(g => g.GetComponent<ItemButton>()).ToArray();

            for(int i = 0; i < itemButtons.Length; i++)
                itemButtons[i].AssignItem(items[i], appMenuGroup, _ItemViewerMenu);

            for(int i = 0; i < itemButtons.Length; i++)
                itemButtons[i].transform.SetParent(i % 2 == 0 ? _LeftList : _RightList, false);
            
            await System.Threading.Tasks.Task.Delay(20);

            _ContentLayoutGroup.enabled = false;
            _ContentLayoutGroup.enabled = true;
        }

        protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            UpdateItemList();
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);
        }

        protected override void OnReturn() { }

        protected override void OnSwitchFrom() { }
    }
}