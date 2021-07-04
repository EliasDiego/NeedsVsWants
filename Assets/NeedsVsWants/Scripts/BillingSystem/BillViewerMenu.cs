using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.BillingSystem
{
    public class BillViewerMenu : Menu
    {
        [SerializeField]
        TMP_Text _BillNameText;
        [SerializeField]
        TMP_Text _BillBalanceText;
        [SerializeField]
        TMP_InputField _AmountInputField;
        [SerializeField]
        CheckoutPopUp _CheckoutPopUp;

        BillEvent _BillEvent;

        double _BillBalanceDisplay = 0;

        public BillEvent billEvent 
        { 
            get => _BillEvent; 
            
            set
            {
                _BillEvent = value;
                
                _BillNameText.text = value.name;

                _BillBalanceDisplay = value.currentBalance;

                UpdateAmountDisplay();
            }
        }

        void Awake() 
        {
            PlayerStatManager.instance.onDateChange += dateTime => 
            {
                if(isActive)
                {
                    if(billEvent.IsWithinDate(dateTime))
                        UpdateAmountDisplay();
                }
            };
        }

        void OnAfterProcessing(double inputAmount)
        {
            _BillEvent.PayBill(inputAmount);
            
            if(_BillBalanceDisplay != _BillEvent.currentBalance)
                UpdateAmountDisplay();

            PlayerStatManager.instance.currentMoney -= inputAmount;
            
            GetComponentInParent<AppMenuGroup>().Return();
        }

        void UpdateAmountDisplay()
        {
            _BillBalanceText.transform.parent.gameObject.SetActive(_BillEvent.showAmount);
            
            if(_BillEvent.showAmount)
                _BillBalanceText.text = StringFormat.ToPriceFormat(_BillEvent.currentBalance);
        }
        
        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _AmountInputField.text = "";
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        public void OnAmountValueChange()
        {
            if(_BillBalanceDisplay != _BillEvent.currentBalance)
                UpdateAmountDisplay();

            _AmountInputField.text = Regex.Replace(_AmountInputField.text, @"[^0-9.]", "");
        }

        public void PayBill()
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
                return;

            double inputAmount = double.Parse(_AmountInputField.text);

            _CheckoutPopUp.hasSufficientFunds = PlayerStatManager.instance.currentMoney >= inputAmount;
            _CheckoutPopUp.onAfterProcessing = () => OnAfterProcessing(inputAmount);
            _CheckoutPopUp.EnablePopUp();
        }

        public void PayAllBill()
        {
            _CheckoutPopUp.hasSufficientFunds = PlayerStatManager.instance.currentMoney >= billEvent.currentBalance;
            _CheckoutPopUp.onAfterProcessing = () => OnAfterProcessing(billEvent.currentBalance);
            _CheckoutPopUp.EnablePopUp();
        }
    }
}