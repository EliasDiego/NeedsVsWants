using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemCartMenu : Menu
    {
        [SerializeField]
        Transform _ContentTransform;
        [SerializeField]
        Toggle _SelectAllToggle;
        [SerializeField]
        TMP_Text _TotalPriceText;
        [SerializeField]
        CheckoutPopUp _CheckoutPopUp;

        Dictionary<Item, int> _ItemCartSlots = new Dictionary<Item, int>();

        List<ItemSlot> _ItemSlotList = new List<ItemSlot>();

        bool _BlockOnSelectAll = false;

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Item Slot");
        }

        void OnAfterCheckoutProcessing(double newMoney)
        {
            Player.PlayerStatManager.instance.currentMoney = newMoney;
            
            DeleteItems();
            
            GetComponentInParent<AppMenuGroup>().Return();
        }

        void OnQuantityChange()
        {
            UpdateTotalPrice();
        }

        void onToggleChange()
        {
            UpdateTotalPrice();

            foreach(ItemSlot itemSlot in _ItemSlotList)
            {
                if(!itemSlot.isToggled)
                {
                    _BlockOnSelectAll = true;
                    _SelectAllToggle.isOn = false;

                    return;
                }
            }

            _SelectAllToggle.isOn = true;
        }

        double GetTotalPrice()
        {
            double totalPrice = 0;

            foreach(ItemSlot itemSlot in _ItemSlotList)
            {
                if(itemSlot.isToggled)
                    totalPrice += itemSlot.item.price * itemSlot.quantity;
            }

            return totalPrice;
        }

        async void SpawnItemsSlots()
        {
            ItemSlot itemSlot;

            _ItemSlotList = new List<ItemSlot>();
            
            foreach(Item item in _ItemCartSlots.Keys)
            {
                itemSlot = ObjectPoolManager.instance.GetObject("Item Slot").GetComponent<ItemSlot>();

                itemSlot.AssignItem(item, _ItemCartSlots[item], OnQuantityChange, onToggleChange);
                
                _ItemSlotList.Add(itemSlot);
            }

            await Task.Delay(10);

            foreach(ItemSlot i in _ItemSlotList)
                i.transform.SetParent(_ContentTransform, false);
        }

        void UpdateDictionary()
        {
            foreach(ItemSlot itemSlot in _ItemSlotList)
                _ItemCartSlots[itemSlot.item] = itemSlot.quantity;
        }

        protected override void OnEnableMenu() 
        { 
            transform.SetActiveChildren(true);

            SpawnItemsSlots();

            UpdateTotalPrice();

            //_BlockOnSelectAll = true;
            _SelectAllToggle.isOn = false;
        }

        protected override void OnDisableMenu() 
        { 
            transform.SetActiveChildren(false);

            UpdateDictionary();

            _ItemSlotList.Clear();
        }

        protected override void OnReturn() { }

        protected override void OnSwitchFrom() { }
        public void DeleteItems()
        {
            List<ItemSlot> removedItemSlot = new List<ItemSlot>();
            
            foreach(ItemSlot itemSlot in _ItemSlotList)
            {
                if(itemSlot.isToggled)
                {
                    _ItemCartSlots.Remove(itemSlot.item);

                    itemSlot.gameObject.SetActive(false);

                    removedItemSlot.Add(itemSlot);

                    _ItemCartSlots.Remove(itemSlot.item);
                }
            }

            foreach(ItemSlot itemSlot in removedItemSlot)
                _ItemSlotList.Remove(itemSlot);
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

        public void SelectAll()
        {
            if(!_BlockOnSelectAll)
            {
                foreach(ItemSlot itemSlot in _ItemSlotList)
                {
                    itemSlot.blockOnToggleEvent = true;
                    itemSlot.isToggled = _SelectAllToggle.isOn;
                }
                    
                UpdateTotalPrice();
            }

            else
                _BlockOnSelectAll = false;
        }

        public void UpdateTotalPrice()
        {
            _TotalPriceText.text = StringFormat.ToPriceFormat(GetTotalPrice());
        }

        public void Checkout()
        {
            bool hasSelected = false;
            double newMoney;

            foreach(ItemSlot itemSlot in _ItemSlotList)
            {
                if(itemSlot.isToggled)
                {
                    hasSelected = true;
                    break;
                }
            }

            if(hasSelected)
            {
                newMoney = Player.PlayerStatManager.instance.currentMoney - GetTotalPrice();

                _CheckoutPopUp.hasSufficientFunds = newMoney >= 0;

                _CheckoutPopUp.onAfterProcessing = () => OnAfterCheckoutProcessing(newMoney);

                _CheckoutPopUp.EnablePopUp();
            }
        }
    }
}