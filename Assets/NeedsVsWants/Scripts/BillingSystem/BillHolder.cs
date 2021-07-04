using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Player;
using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.BillingSystem
{
    public class BillHolder : MonoBehaviour
    {
        [SerializeField]
        TMP_Text _BillNameText;
        [SerializeField]
        Button _BillButton;
        [SerializeField]
        Indicator _Indicator;

        Action<DateTime> onDateChange;

        BillEvent _BillEvent;

        void Awake() 
        {
            onDateChange = dateTime =>
            {
                if(_BillEvent.IsWithinDate(dateTime))
                    _Indicator.gameObject.SetActive(_BillEvent.currentBalance > 0);
            }; 
        }

        void OnEnable() 
        {
            PlayerStatManager.instance.onDateChange += onDateChange;
        }

        void OnDisable() 
        {
            PlayerStatManager.instance.onDateChange -= onDateChange;
            
            _BillEvent = null;
        }

        public void AssignBill(BillEvent billEvent, AppMenuGroup appMenuGroup, BillViewerMenu billViewerMenu)
        {
            _BillNameText.text = billEvent.name;

            _BillButton.onClick.RemoveAllListeners();

            _BillButton.onClick.AddListener(() =>
            {
                appMenuGroup.SwitchTo(billViewerMenu);

                billViewerMenu.billEvent = billEvent;
            });

            _Indicator.gameObject.SetActive(billEvent.currentBalance > 0);

            _BillEvent = billEvent;
        }
    }
}