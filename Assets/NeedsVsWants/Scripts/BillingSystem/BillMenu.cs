using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.BillingSystem
{
    public abstract class BillMenu : Menu
    {
        [SerializeField]
        TMP_Text _BillBalanceText;
        [SerializeField]
        TMP_InputField _AmountInputField;
        [SerializeField]
        CheckoutPopUp _CheckoutPopUp;
        [SerializeField]
        Indicator _Indicator;

        double _BillBalanceDisplay = 0;

        double _CurrentBalance = 0;

        protected abstract string billEventName { get; }

        void Awake() 
        {
            PlayerStatManager.instance.onDateChange += dateTime => 
            {
                if(IsWithinDate(dateTime))
                {
                    _CurrentBalance += CalculateBill(dateTime);

                    if(isActive)
                        UpdateAmountDisplay();

                    else
                        _Indicator.gameObject.SetActive(true);

                    if(CalculateBill(dateTime) * 6 <= _CurrentBalance)
                        PlayerStatManager.instance.LoadEndMenuScene();
                }
            };

            BillEvent billEvent = BillEvent.CreateInstance<BillEvent>();

            billEvent.name = billEventName;
            billEvent.onIsWithinDate += IsWithinDate;

            PlayerStatManager.instance.AddCalendarEvent(billEvent);
        }

        void OnAfterProcessing(double inputAmount)
        {
            UpdateAmountDisplay();
            
            PlayerStatManager.instance.currentMoney -= inputAmount;

            _CurrentBalance -= inputAmount;
            
            if(_CurrentBalance <= 0)
                _Indicator.gameObject.SetActive(false);
                
            _AmountInputField.text = "";
        }

        void UpdateAmountDisplay()
        {
            _BillBalanceText.text = StringFormat.ToPriceFormat(_CurrentBalance);
        }
        
        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _AmountInputField.text = "";

            UpdateAmountDisplay();
        }

        public void OnAmountValueChange()
        {
            if(_BillBalanceDisplay != _CurrentBalance)
                UpdateAmountDisplay();

            _AmountInputField.text = Regex.Replace(_AmountInputField.text, @"[^0-9.]", "");
        }

        public void PayBill()
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
                return;

            double inputAmount = double.Parse(_AmountInputField.text);

            if(_CurrentBalance <= 0 || inputAmount <= 0)
                return;

            _CheckoutPopUp.hasSufficientFunds = PlayerStatManager.instance.currentMoney >= inputAmount;
            _CheckoutPopUp.onAfterProcessing = () => OnAfterProcessing(inputAmount);
            _CheckoutPopUp.EnablePopUp();
        }

        public void PayAllBill()
        {
            if(_CurrentBalance <= 0)
                return;

            _CheckoutPopUp.hasSufficientFunds = PlayerStatManager.instance.currentMoney >= _CurrentBalance;
            _CheckoutPopUp.onAfterProcessing = () => OnAfterProcessing(_CurrentBalance);
            _CheckoutPopUp.EnablePopUp();
        }
        
        public abstract bool IsWithinDate(DateTime dateTime);
    
        public abstract double CalculateBill(DateTime dateTime);
    }
}