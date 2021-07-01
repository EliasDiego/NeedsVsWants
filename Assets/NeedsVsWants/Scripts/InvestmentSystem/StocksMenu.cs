using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.InvestmentSystem
{
    public class StocksMenu : InvestmentMenu
    {
        [SerializeField]
        TMP_Text _GainLossText;
        [SerializeField]
        Image _BoxCapitalImage;
        [SerializeField]
        GainLossChance[] _GainLossChances;

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        protected override void OnEnableMenu()
        {
            base.OnEnableMenu();

            _BoxCapitalImage.color = HasReachedMinReq() ? Color.white : Color.grey;
        }

        protected override void OnCashIn(bool hasSufficientFunds)
        {
            if(hasSufficientFunds)
                _BoxCapitalImage.color = HasReachedMinReq() ? Color.white : Color.grey;
        }

        protected override void OnCashOut(bool hasSufficientFunds)
        {
            if(hasSufficientFunds)
            {
                if(HasReachedMinReq())
                    _BoxCapitalImage.color = Color.white;
                
                else
                {
                    _BoxCapitalImage.color = Color.grey;

                    _GainLossText.text = "- %";
                }
            }
        }

        protected override bool HasReachedMinReq()
        {
            return capital > 0;
        }

        protected override bool IsWithinRange(DateTime dateTime)
        {
            bool isNextMonth = false;

            isNextMonth = dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
            
            return isNextMonth;
        }

        protected override double CalculateGainLoss(DateTime dateTime)
        {
            int randomChance = UnityEngine.Random.Range(1, 100);
            int currentChanceOfHappening = 0;

            double gainLoss = 0;

            foreach(GainLossChance chance in _GainLossChances)
            {
                currentChanceOfHappening += chance.chanceOfHappening;

                if(randomChance <= currentChanceOfHappening)
                {
                    gainLoss = capital * ((float)chance.effect / 100);
                    
                    _GainLossText.text = chance.effect + " %";

                    break;
                }
            }

            return gainLoss;
        }
    }
}