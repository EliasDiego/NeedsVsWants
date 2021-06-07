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

        BillEvent _BillEvent;

        public BillEvent billEvent 
        { 
            get => _BillEvent; 
            
            set
            {
                _BillEvent = value;
                
                _BillNameText.text = value.name;
            }
        }
        
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
    }
}