using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
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
        [SerializeField][Range(1, 100)]
        float _Discount;
        [SerializeField]
        Item[] _SaleItems;

        public float discount => _Discount;

        public override bool showOnCalendar => true;

        public override void Invoke(DateTime dateTime)
        {
            PlayerStatManager.instance.AddShopItemTolist(_SaleItems);
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            DateTime startSaleDay = new DateTime(dateTime.Year, _SaleDate.month, _SaleDate.day);
            DateTime endSaleDay = new DateTime(dateTime.Year, _SaleDate.month, _SaleDate.day).AddDays(_SaleDays);

            return startSaleDay <= dateTime && dateTime < endSaleDay;
        }
    }
}