using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Player;
using NeedsVsWants.PhoneSystem;
using NeedsVsWants.CalendarSystem;

using TMPro;

namespace NeedsVsWants.InvestmentSystem
{
    public class TimeDepositMenu : InvestmentMenu
    {
        [SerializeField]
        int _Effect;
        [SerializeField]
        double _MinRequirement;
        [SerializeField]
        Button _CashInButton;
        [SerializeField]
        Button _CashOutButton;
        [SerializeField]
        TMP_Text _LockInPeriodText;

        bool _HasInvested = false;
        bool _IsInLockInPeriod = false;

        Date _DateInvested;

        void SetUI()
        {
            _CashInButton.gameObject.SetActive(!_HasInvested);

            _CashOutButton.gameObject.SetActive(_HasInvested);
            _CashOutButton.interactable = !_IsInLockInPeriod;

            amountInputField.gameObject.SetActive(!_HasInvested);

            _LockInPeriodText.gameObject.SetActive(_IsInLockInPeriod);
            
            if(_LockInPeriodText.IsActive())
            {
                _LockInPeriodText.text = "Lock In Period is until " + CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_DateInvested.month) + " " +
                    _DateInvested.day + ", " + (_DateInvested.year + 1);
            }

            amountInputField.interactable = !_HasInvested;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        protected override void OnEnableMenu()
        {
            base.OnEnableMenu();

            SetUI();
        }

        protected override void OnCashIn(bool hasSufficientFunds)
        {
            if(hasSufficientFunds)
            {
                _DateInvested = (Date)PlayerStatManager.instance.currentDate;

                _HasInvested = true;

                _IsInLockInPeriod = true;
                
                SetUI();
            }
        }

        protected override void OnCashOutAll(bool hasSufficientFunds)
        {
            if(hasSufficientFunds)
            {
                _HasInvested = false;
                
                SetUI();
            }
        }

        protected override bool AllowCashInputted(bool isCashIn, ref double parsedAmount, out string errorText)
        {
            bool allow = false;

            errorText = "";

            if(isCashIn)
            {
                allow = parsedAmount >= _MinRequirement;

                if(!allow)
                    errorText = "Amount must be Greater Than or Equal to " + StringFormat.ToPriceFormat(_MinRequirement);
            }

            else
            {
                allow = true;

                parsedAmount = capital + capitalGainLoss;
            }


            return allow;
        }

        protected override bool HasReachedMinReq()
        {
            return capital + capitalGainLoss >= _MinRequirement;
        }

        protected override bool IsWithinRange(DateTime dateTime)
        {
            int daysInMonth;

            bool isNewYear = false;

            if(dateTime.Month == _DateInvested.month)
            {
                daysInMonth = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

                isNewYear = dateTime.Day == _DateInvested.day || (daysInMonth < _DateInvested.day && dateTime.Day == daysInMonth);
            }

            return isNewYear;
        }

        protected override double CalculateGainLoss(DateTime dateTime)
        {
            if(_HasInvested)
            {
                _IsInLockInPeriod = false;

                SetUI();
            }

            return capital * ((float)_Effect / (float)100);
        }
    }
}