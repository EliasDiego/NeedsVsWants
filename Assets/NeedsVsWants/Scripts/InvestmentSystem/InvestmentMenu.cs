using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

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
        [SerializeField]
        TMP_Text _ErrorText;

        InvestmentEvent _InvestmentEvent;

        protected double capital { get; set; }

        protected double capitalGainLoss { get; set; }

        protected TMP_Text capitalText => _CapitalText;
        protected TMP_InputField amountInputField => _AmountInputField;
        
        protected abstract string investmentEventName { get; }
        protected bool investmentEventShownInCalendar { get => _InvestmentEvent.isShownOnCalendar; set => _InvestmentEvent.isShownOnCalendar = value; }

        void Awake() 
        {
            PlayerStatManager.instance.onDateChange += dateTime => 
            {
                if(IsWithinRange(dateTime))
                {
                    if(HasReachedMinReq())
                    {
                        if(!isActive)
                            _Indicator.gameObject.SetActive(true);

                        capitalGainLoss += CalculateGainLoss(dateTime);

                        if(capital + capitalGainLoss < 0)
                            capitalGainLoss = -(capital + capitalGainLoss);

                        capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
                    }
                }
            };
            
            _InvestmentEvent = InvestmentEvent.CreateInstance<InvestmentEvent>();

            _InvestmentEvent.name = investmentEventName;
            _InvestmentEvent.onIsWithinDate += IsWithinRange;

            PlayerStatManager.instance.AddCalendarEvent(_InvestmentEvent);
        }

        void SetErrorText(string errorText)
        {
            _ErrorText.gameObject.SetActive(true);
            _ErrorText.text = errorText;
        }
        
        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);

            _ErrorText.gameObject.SetActive(false);
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected virtual bool HasReachedMinReq()
        {
            return true;
        }

        protected virtual void OnCashIn(bool hasSufficientFunds) { }
        
        protected virtual void OnCashOut(bool hasSufficientFunds) { }

        protected virtual void OnCashOutAll(bool hasSufficientFunds) { }

        protected virtual bool AllowCashInputted(bool isCashIn, ref double parsedAmount, out string errorText) 
        {
            errorText = "";

            return true;
        }
        
        protected abstract bool IsWithinRange(DateTime dateTime);

        protected abstract Double CalculateGainLoss(DateTime dateTime);


        public void OnAmountValueChange()
        {
            _AmountInputField.text = Regex.Replace(_AmountInputField.text, @"[^0-9.]", "");
        }

        public void CashIn(CheckoutPopUp checkoutPopUp)
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
            {
                SetErrorText("Please enter an amount!");

                return;
            }

            double parsedAmount = Double.Parse(_AmountInputField.text);
            double newMoney = PlayerStatManager.instance.currentMoney - parsedAmount;

            if(parsedAmount <= 0)
                return;

            if(!AllowCashInputted(true, ref parsedAmount, out string errorText))
            {
                SetErrorText(errorText);

                return;
            }

            checkoutPopUp.useDefaultText = true;
            checkoutPopUp.hasSufficientFunds = newMoney >= 0;
            checkoutPopUp.onAfterProcessing = () => 
            {
                PlayerStatManager.instance.currentMoney = newMoney;

                capital += parsedAmount;
                capitalGainLoss = 0;
                
                capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
                
                OnCashIn(true);
            };
            
            checkoutPopUp.EnablePopUp();

            _AmountInputField.text = "";
                
            _ErrorText.gameObject.SetActive(false);

            if(!checkoutPopUp.hasSufficientFunds)
                OnCashIn(false);
        }
        
        public void CashOut(CheckoutPopUp checkoutPopUp)
        {
            if(string.IsNullOrEmpty(_AmountInputField.text))
            {
                SetErrorText("Please enter an amount!");

                return;
            }

            double parsedAmount = Double.Parse(_AmountInputField.text);
            double overallCapital = capital + capitalGainLoss;

            if(parsedAmount <= 0)
                return;
                
            if(!AllowCashInputted(false, ref parsedAmount, out string errorText))
            {
                SetErrorText(errorText);

                return;
            }
                
            checkoutPopUp.hasSufficientFunds = parsedAmount <= overallCapital;
            checkoutPopUp.onAfterProcessing = () => 
            {
                overallCapital -= parsedAmount;

                PlayerStatManager.instance.currentMoney += parsedAmount;

                capital = overallCapital;
                capitalGainLoss = 0;
                
                capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
                
                OnCashOut(true);
            };
            
            checkoutPopUp.EnablePopUp();

            _AmountInputField.text = "";
            
            _ErrorText.gameObject.SetActive(false);
            
            if(!checkoutPopUp.hasSufficientFunds)
                OnCashOut(false);
        }

        public void CashOutAll(CheckoutPopUp checkoutPopUp)
        {
            double overallCapital = capital + capitalGainLoss;
                
            checkoutPopUp.hasSufficientFunds = overallCapital > 0;
            checkoutPopUp.onAfterProcessing = () => 
            {
                PlayerStatManager.instance.currentMoney += overallCapital;

                capital = 0;
                capitalGainLoss = 0;
                
                capitalText.text = StringFormat.ToPriceFormat(capital + capitalGainLoss);
                
                OnCashOutAll(true);
            };
            
            checkoutPopUp.EnablePopUp();

            _AmountInputField.text = "";
            
            _ErrorText.gameObject.SetActive(false);
            
            if(!checkoutPopUp.hasSufficientFunds)
                OnCashOutAll(false);
        }
    }
}