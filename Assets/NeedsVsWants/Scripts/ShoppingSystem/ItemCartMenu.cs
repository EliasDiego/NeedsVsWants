using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

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
        [SerializeField]
        Indicator[] _Indicators;

        Dictionary<Item, int> _ItemCartSlots = new Dictionary<Item, int>();

        List<ItemSlot> _ItemSlotList = new List<ItemSlot>();

        Item _BuyItem;

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Item Slot");
        }

        void OnChangeToCart()
        {
            int itemsInCart = 0;

            foreach(Item item in _ItemCartSlots.Keys)
                itemsInCart += _ItemCartSlots[item];

            foreach(Indicator n in _Indicators)
                n.text = itemsInCart.ToString();
        }

        void OnAfterCheckoutProcessing(double newMoney)
        {
            Player.PlayerStatManager.instance.currentMoney = newMoney;

            foreach(ItemSlot itemSlot in _ItemSlotList)
            {
                if(itemSlot.isToggled)
                    itemSlot.item.OnBuy();
            }
            
            DeleteItems();
            
            // Return To Item List Menu
            while(menuGroup.currentMenu.GetType() != typeof(ItemListMenu))
                menuGroup.Return();
        }

        void OnQuantityChange()
        {
            UpdateTotalPrice();
        }

        void onToggleChange()
        {
            UpdateTotalPrice();
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

                // When On Buy Now
                if(item == _BuyItem)
                {
                    itemSlot.isToggled = true;

                    _BuyItem = null;
                }
                
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

            OnChangeToCart();
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

            OnChangeToCart();
        }   

        public void AddAndSelectItemToCart(Item item)
        {
            AddToCart(item);

            _BuyItem = item;
        }

        public void SelectAll()
        {
            foreach(ItemSlot itemSlot in _ItemSlotList)
                itemSlot.isToggled = _SelectAllToggle.isOn;
                
            UpdateTotalPrice();
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