using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.BillingSystem
{
    public abstract class BillEvent : CalendarEvent
    {
        // [Header("Bill")]
        // [SerializeField]
        // float _Amount;
        // [SerializeField]
        // Date _MinDate;
        // [SerializeField]
        // Date _MaxDate;

        float _CurrentAmount = 0;

        bool _HasPayed = false;

        public float currentAmount => _CurrentAmount;

        public override bool isShowOnCalendar => true;

        public abstract float CalculateBill(DateTime dateTime);

        public override void Invoke(DateTime dateTime)
        {
            if(_HasPayed)
                _CurrentAmount += CalculateBill(dateTime);
        }

        public void PayBill(float amount)
        {
            _CurrentAmount -= amount;
        }

        // public override void Invoke()
        // {
        //     PlayerStatManager.instance.currentMoney -= _Amount;
        // }

        // public override bool IsWithinDate(DateTime dateTime)
        // {
        //     DateTime min = new DateTime(_MinDate.year, _MinDate.month, _MinDate.day);
        //     DateTime max = new DateTime(_MaxDate.year, _MaxDate.month, _MaxDate.day, 23, 59, 59);

        //     return min <= dateTime && dateTime <= max;
        // }
    }
}