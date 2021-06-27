using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.ShoppingSystem
{
    public class ItemCartMenu : Menu
    {
        [Header("UI")]
        [SerializeField]
        VerticalLayoutGroup _ContentLayoutGroup;
        [SerializeField]
        Toggle _SelectAllToggle;
        [SerializeField]
        TMP_Text _TotalPriceText;
        
        [Header("Cart Indication")]
        [SerializeField]
        Indicator[] _Indicators;
        
        [Header("Pop Up")]
        [SerializeField]
        CheckoutPopUp _CheckoutPopUp;
        [SerializeField]
        LoadingPopUp _LoadingPopUp;
        [SerializeField]
        AppPopUp _ItemNotifPopUp;
        [SerializeField]
        TMP_Text _PopUpText;
        [SerializeField]
        Button _CloseButton;
        [SerializeField, Multiline]
        string _OnEdit;
        [SerializeField, Multiline]
        string _OnRemove;

        Dictionary<Item, int> _ItemCartSlots = new Dictionary<Item, int>();

        List<ItemSlot> _ItemSlotList = new List<ItemSlot>();

        Item _BuyItem;

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Item Slot");

            PlayerStatManager.instance.onEditItems += items => 
            {
                IEnumerable<ItemSlot> editedCartItems;

                if(isActive) 
                {
                    editedCartItems = _ItemSlotList.Where(itemSlot => items.Contains(itemSlot.item));

                    if(editedCartItems.Count() > 0)
                    {
                        _PopUpText.text = _OnEdit;

                        _CloseButton.onClick.RemoveAllListeners();
                        _CloseButton.onClick.AddListener(async() => 
                        {
                            _ItemNotifPopUp.DisablePopUp();

                            _ItemNotifPopUp.DisablePopUp();
                            _ItemNotifPopUp.transform.SetActiveChildren(false);
                            
                            _LoadingPopUp.EnablePopUp();

                            foreach(ItemSlot itemSlot in editedCartItems)
                                itemSlot.UpdateItemDetails();

                            UpdateTotalPrice();

                            _ContentLayoutGroup.enabled = false;

                            await System.Threading.Tasks.Task.Delay(100);
                            
                            _ContentLayoutGroup.enabled = true;
                            
                            _LoadingPopUp.DisablePopUp();
                        });
                        
                        _ItemNotifPopUp.EnablePopUp();
                    }
                }
            };

            PlayerStatManager.instance.onRemoveItems += items =>
            {   
                IEnumerable<ItemSlot> removedItemSlotItems;

                if(isActive) 
                {
                    removedItemSlotItems = _ItemSlotList.Where(itemSlot => items.Contains(itemSlot.item));

                    if(removedItemSlotItems.Count() > 0)
                    {
                        _PopUpText.text = _OnRemove;

                        _CloseButton.onClick.RemoveAllListeners();
                        _CloseButton.onClick.AddListener(async() => 
                        {
                            List<ItemSlot> safeMarkedItemSlot = new List<ItemSlot>();

                            _ItemNotifPopUp.DisablePopUp();
                            _ItemNotifPopUp.transform.SetActiveChildren(false);
                            
                            _LoadingPopUp.EnablePopUp();

                            // Mark Item Slots for Removal
                            foreach(ItemSlot itemSlot in _ItemSlotList)
                            {
                                if(removedItemSlotItems.Contains(itemSlot))
                                    itemSlot.isToggled = true;

                                // If ItemSlot is toggled but not part of items to be removed, keep track but untoggle
                                else if(itemSlot.isToggled)
                                {
                                    itemSlot.isToggled = false;

                                    safeMarkedItemSlot.Add(itemSlot);
                                }

                                itemSlot.UpdateItemDetails();
                            }

                            DeleteItems();

                            // Re toggle Item Slots
                            foreach(ItemSlot itemSlot in safeMarkedItemSlot)
                                itemSlot.isToggled = true;

                            UpdateTotalPrice();

                            _ContentLayoutGroup.enabled = false;

                            await System.Threading.Tasks.Task.Delay(100);
                            
                            _ContentLayoutGroup.enabled = true;
                            
                            _LoadingPopUp.DisablePopUp();
                        });
                        
                        _ItemNotifPopUp.EnablePopUp();
                    }
                }
            };
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
            
            menuGroup.ReturnToPreviousMenu<ItemListMenu>();
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
                    totalPrice += (itemSlot.item.isDiscounted ? itemSlot.item.discountPrice : itemSlot.item.price) * itemSlot.quantity;
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
                i.transform.SetParent(_ContentLayoutGroup.transform, false);
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
                newMoney = PlayerStatManager.instance.currentMoney - GetTotalPrice();

                _CheckoutPopUp.hasSufficientFunds = newMoney >= 0;

                _CheckoutPopUp.onAfterProcessing = () => OnAfterCheckoutProcessing(newMoney);

                _CheckoutPopUp.EnablePopUp();
            }
        }
    }
}