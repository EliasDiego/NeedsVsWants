using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;

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

        BillEvent _BillEvent;

        float _BillAmountDisplay = 0;

        public BillEvent billEvent 
        { 
            get => _BillEvent; 
            
            set
            {
                _BillEvent = value;
                
                _BillNameText.text = value.name;

                _BillAmountDisplay = value.currentBalance;

                UpdateAmountDisplay();
            }
        }

        void Update() 
        {
            if(isActive)
            {
                if(_BillAmountDisplay != _BillEvent.currentBalance)
                    UpdateAmountDisplay();

                _AmountInputField.text = Regex.Replace(_AmountInputField.text, @"[^0-9.]", "");
            }    
        }

        void UpdateAmountDisplay()
        {
            _BillBalanceText.transform.parent.gameObject.SetActive(_BillEvent.showAmount);
            _BillBalanceText.text = _BillEvent.currentBalance.ToString();
            
            if(_BillEvent.showAmount)
                _BillBalanceText.text = _BillEvent.currentBalance.ToString();
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

        public void PayBill()
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
                return;

            float inputAmount = float.Parse(_AmountInputField.text);

            if(PlayerStatManager.instance.currentMoney >= inputAmount)
            {
                _BillEvent.PayBill(inputAmount);

                PlayerStatManager.instance.currentMoney -= inputAmount;
            }
        }
    }
}