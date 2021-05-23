using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using TMPro;

using NeedsVsWants.Player;

namespace NeedsVsWants.MoneySystem
{
    public class MoneyDisplay : MonoBehaviour
    {
        TMP_Text _MoneyText;

        void Awake()
        {
            _MoneyText = GetComponentInChildren<TMP_Text>();
        }

        void Start() 
        {
            PlayerStatManager.instance.onMoneyChange += money => SetText(money);   
        }

        string RoundDecimals(float value) => 
            value % 1 == 0 ? value.ToString() + ".00" : ((float)Math.Round(value * 100f) / 100f).ToString();

        string AddZeroes(string value)
        {
            string[] strings = value.Split('.');

            string zeroText = "";

            string intText = strings[0];

            for(int i = 0; i < 9 - intText.Length; i++)
                zeroText += '0';

            return zeroText + intText + "." + strings[1];
        }

        string AddCommas(string value)
        {
            string intString = value.Split('.')[0];
            string valueText = "";
            
            for(int i = 0; i < 7; i += 3)
                valueText += intString.Substring(i, 3) + (i < 4 ? "," : "");

            return valueText + "." + value.Split('.')[1];
        }

        public void SetText(float value)
        {
            string valueText = RoundDecimals(value);

            valueText = AddZeroes(valueText);

            valueText = AddCommas(valueText);

            _MoneyText.text = "₱ " + valueText;
        }
    }
}