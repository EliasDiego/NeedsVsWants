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
        [Header("Bill")]
        [SerializeField]
        Sprite _Icon;
        [SerializeField]
        bool _ShowAmount = false;

        double _CurrentBalance = 0;

        public Sprite icon => _Icon;

        public double currentBalance => _CurrentBalance;

        public override bool showOnCalendar => true;

        public bool showAmount => _ShowAmount;

        public override void Initialize()
        {
            _CurrentBalance = 0;
        }

        public abstract double CalculateBill(DateTime dateTime);

        public override void Invoke(DateTime dateTime)
        {
            _CurrentBalance += CalculateBill(dateTime);
        }

        public void PayBill(double amount)
        {
            _CurrentBalance -= amount;
        }
    }
}