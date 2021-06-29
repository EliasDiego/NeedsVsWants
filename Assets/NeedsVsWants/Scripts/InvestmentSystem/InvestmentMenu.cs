using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;
using NeedsVsWants.CalendarSystem;

using TMPro;

namespace NeedsVsWants.InvestmentSystem
{
    public abstract class InvestmentMenu : Menu
    {
        [SerializeField]
        Indicator _Indicator;
        [SerializeField]
        TMP_InputField _AmountInputField;
        [SerializeField]
        TMP_Text _CapitalText;

        protected double capital { get; private set; }

        protected Date dateInvested { get; private set; }

        protected double capitalGainLoss { get; private set; }

        protected TMP_Text capitalText => _CapitalText;
        
        protected override void Start() 
        {
            base.Start();

            PlayerStatManager.instance.onDateChange += dateTime => 
            {
                if(IsWithinRange(dateTime))
                {
                    if(!isActive)
                        _Indicator.gameObject.SetActive(true);

                    if(capital > 0)
                        capitalGainLoss += CalculateGainLoss(dateTime, capital);
                }
            };
        }

        protected abstract bool IsWithinRange(DateTime dateTime);

        protected abstract Double CalculateGainLoss(DateTime dateTime, double money);

        public void OnAmountValueChange()
        {
            _AmountInputField.text = Regex.Replace(_AmountInputField.text, @"[^0-9.]", "");
        }

        public void CashIn(CheckoutPopUp checkoutPopUp)
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
                return;

            double parsedAmount = Double.Parse(_AmountInputField.text);
            double newMoney = PlayerStatManager.instance.currentMoney - parsedAmount;

            if(parsedAmount <= 0)
                return;

            checkoutPopUp.useDefaultText = true;
            checkoutPopUp.hasSufficientFunds = newMoney > 0;
            checkoutPopUp.onAfterProcessing = () => 
            {
                PlayerStatManager.instance.currentMoney = newMoney;

                capital += parsedAmount;
                capitalGainLoss = 0;
                
                capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
                
                dateInvested = (Date)PlayerStatManager.instance.currentDate;
            };
            
            checkoutPopUp.EnablePopUp();

            _AmountInputField.text = "0";
        }
        
        public void CashOut(CheckoutPopUp checkoutPopUp)
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
                return;

            double parsedAmount = Double.Parse(_AmountInputField.text);
            double overallCapital = capital + capitalGainLoss;

            if(parsedAmount <= 0)
                return;
                
            checkoutPopUp.hasSufficientFunds = parsedAmount <= overallCapital;
            checkoutPopUp.onAfterProcessing = () => 
            {
                overallCapital -= parsedAmount;

                PlayerStatManager.instance.currentMoney += parsedAmount;

                capital = overallCapital;
                capitalGainLoss = 0;
                
                capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
            };
            
            checkoutPopUp.EnablePopUp();

            _AmountInputField.text = "0";
        }
    }
}