using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

using TMPro;

namespace NeedsVsWants.BillingSystem
{
    public class BillViewerMenu : Menu
    {
        [SerializeField]
        TMP_Text _BillNameText;
        [SerializeField]
        TMP_Text _AmountText;
        [SerializeField]
        TMP_InputField _AmountInputField;

        BillEvent _BillEvent;

        public BillEvent billEvent 
        { 
            get => _BillEvent; 
            
            set
            {
                _BillEvent = value;
                
                _BillNameText.text = value.name;

                _AmountText.transform.parent.gameObject.SetActive(value.showAmount);
                
                if(value.showAmount)
                    _AmountText.text = value.currentAmount.ToString();
            }
        }

        // Check if Amount Input Field is being inputted numbers
        // Make sure to deny pay bill if player doesn't have enough money
        
        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        public void CheckInput(string text)
        {
            //_AmountInputField
        }

        public void PayBill(TMP_InputField amountInputField)
        {
            _BillEvent.PayBill(float.Parse(amountInputField.text));
        }
    }
}