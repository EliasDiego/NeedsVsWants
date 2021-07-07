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
            PlayerStatManager.instance.onMoneyChange += money => _MoneyText.text = StringFormat.ToPriceFormat(money);

            _MoneyText.text = StringFormat.ToPriceFormat(PlayerStatManager.instance.currentMoney);
        }

    }
}