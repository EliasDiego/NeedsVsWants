using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemCartMenu : Menu
    {
        [SerializeField]
        Transform _ContentTransform;

        Dictionary<Item, int> _ItemCartSlots = new Dictionary<Item, int>();

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Item Slot");
        }

        ItemSlot[] GetItemSlots()
        {
            List<ItemSlot> itemSlotList = new List<ItemSlot>();

            for(int i = 0; i < _ContentTransform.childCount; i++)
                itemSlotList.Add(_ContentTransform.GetChild(i).GetComponent<ItemSlot>());

            return itemSlotList.ToArray();
        }

        async void UpdateItems()
        {
            ItemSlot itemSlot;

            List<ItemSlot> itemSlotList = new List<ItemSlot>();
            
            foreach(Item item in _ItemCartSlots.Keys)
            {
                itemSlot = ObjectPoolManager.instance.GetObject("Item Slot").GetComponent<ItemSlot>();

                itemSlot.AssignItem(item, _ItemCartSlots[item]);
                
                itemSlotList.Add(itemSlot);
            }

            await Task.Delay(10);

            foreach(ItemSlot i in itemSlotList)
                i.transform.SetParent(_ContentTransform, false);
        }

        void UpdateItemCartSlots()
        {
            foreach(ItemSlot itemSlot in GetItemSlots())
                _ItemCartSlots[itemSlot.item] = itemSlot.quantity;
        }

        protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            UpdateItems();
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);

            UpdateItemCartSlots();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        public void DeleteItems()
        {
            foreach(ItemSlot itemSlot in GetItemSlots())
            {
                if(itemSlot.isToggled)
                {
                    _ItemCartSlots.Remove(itemSlot.item);

                    itemSlot.gameObject.SetActive(false);
                }
            }
        }

        public void AddToCart(Item item)
        {
            if(_ItemCartSlots.ContainsKey(item))
            {
                if(item.GetType() != typeof(GameObjectItem))
                    _ItemCartSlots[item]++;
            }

            else
                _ItemCartSlots.Add(item, 1);
        }   
    }
}