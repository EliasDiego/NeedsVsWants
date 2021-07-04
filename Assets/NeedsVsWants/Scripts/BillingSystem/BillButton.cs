using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.BillingSystem
{        
    public class BillButton : Button
    {
        TMP_Text _BillNameText;

        Image _BillIconImage;

        protected override void Awake() 
        {
            base.Awake();

            _BillNameText = GetComponentInChildren<TMP_Text>();
            _BillIconImage = GetComponentInChildren<Image>();
        }

        public void AssignBill(BillEvent billEvent, AppMenuGroup appMenuGroup, BillViewerMenu billViewerMenu)
        {
            _BillNameText.text = billEvent.name;
            _BillIconImage.sprite = billEvent.icon;

            onClick.RemoveAllListeners();

            onClick.AddListener(() =>
            {
                appMenuGroup.SwitchTo(billViewerMenu);

                billViewerMenu.billEvent = billEvent;
            });
        }
    }
}