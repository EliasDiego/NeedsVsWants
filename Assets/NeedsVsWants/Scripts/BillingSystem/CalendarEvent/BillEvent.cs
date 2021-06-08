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

        float _CurrentAmount = 0;

        bool _HasPayed = false;

        public Sprite icon => _Icon;

        public float currentAmount => _CurrentAmount;

        public override bool showOnCalendar => true;

        public bool showAmount => _ShowAmount;

        // Make sure to have amount at the start of the game (Unless there's change on story)
        
        void OnEnable() 
        {
            if(Application.isPlaying)
            {
                Debug.Log(_CurrentAmount);

                _CurrentAmount = CalculateBill(PlayerStatManager.instance.currentDate);
            }
        }

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
    }
}