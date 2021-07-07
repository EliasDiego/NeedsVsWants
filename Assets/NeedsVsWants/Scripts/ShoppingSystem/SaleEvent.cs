using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MoneySystem;
using NeedsVsWants.CalendarSystem;


namespace NeedsVsWants.ShoppingSystem
{    
    [CreateAssetMenu(menuName = "NeedsVsWants/Shopping/Sale Event")]
    public class SaleEvent : CalendarEvent
    {
        [SerializeField]
        Date _SaleDate;
        [SerializeField]
        int _SaleDays = 1;
        [SerializeField]
        bool _IsYearSpecific = false;
        [SerializeField]
        DiscountItem[] _DiscountItems;

        int _SaleDaysCount = 0;

        Item[] _TempDiscountShopitems;

        public override bool showOnCalendar => true;

        void CalculateItemPrice(Item item, MoneyOperator discount)
        {
            double tempMoney;
            
            tempMoney = discount.CalculateMoney(item.price);
            
            item.isDiscounted = true;
            item.discountPrice = tempMoney < 0 ? 0 : tempMoney;
        }

        void OnStartSale()
        {
            Dictionary<Item, MoneyOperator> itemDiscount = new Dictionary<Item, MoneyOperator>();
            
            _TempDiscountShopitems = _DiscountItems.Select(discountItem => discountItem.item).Except(PlayerStatManager.instance.ShopItems).Where(item => 
            {
                // If GameObjectItem has already been bought 
                if(item.GetType() == typeof(GameObjectItem))
                {
                    if((item as GameObjectItem).isAlreadyActive)
                        return false;
                }

                return true;
            }).ToArray();
            
            foreach(DiscountItem discountItem in _DiscountItems)
                itemDiscount.Add(discountItem.item, discountItem.discount);

            PlayerStatManager.instance.AddShopItem(_TempDiscountShopitems);

            // Apply Discounts
            PlayerStatManager.instance.EditItem(item => 
            {
                bool isContained = itemDiscount.Keys.Contains(item);

                if(isContained)
                    CalculateItemPrice(item, itemDiscount[item]);

                return isContained;
            });
        }

        void OnEndSale()
        {
            PlayerStatManager.instance.EditItem(item => 
            {
                bool isContained = _DiscountItems.Select(discountItem => discountItem.item).Contains(item);

                if(isContained)
                    item.isDiscounted = false;

                return isContained;
            });
                
            PlayerStatManager.instance.RemoveShopItem(_TempDiscountShopitems);
        }

        public override void Initialize()
        {
            _SaleDaysCount = 0;
        }

        public override void Invoke(DateTime dateTime)
        {
            _SaleDaysCount++;

            if(_SaleDaysCount == 1)
                OnStartSale();
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            DateTime startSaleDay = new DateTime(dateTime.Year, _SaleDate.month, _SaleDate.day);
            DateTime endSaleDay = new DateTime(dateTime.Year, _SaleDate.month, _SaleDate.day).AddDays(_SaleDays);

            if(_SaleDaysCount >= _SaleDays)
            {
                _SaleDaysCount = 0;

                OnEndSale();
            }

            return startSaleDay <= dateTime && dateTime < endSaleDay;
        }
    }
}