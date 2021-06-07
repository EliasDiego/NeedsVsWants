using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.BillingSystem
{        
    public class BillButton : Button
    {
        TMP_Text _BillNameText;

        public string billName { get => _BillNameText.text; set => _BillNameText.text = value; }

        public int billEventIndex { get; set; }

        protected override void Awake() 
        {
            base.Awake();

            _BillNameText = GetComponentInChildren<TMP_Text>();
        }
    }
}