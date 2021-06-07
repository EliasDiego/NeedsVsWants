using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.BillingSystem
{
    public class BillListMenu : Menu
    {
        [SerializeField]
        BillViewerMenu _BillViewerMenu;

        Transform _BillListTransform;


        void Awake() 
        {
            _BillListTransform = GetComponentInChildren<GridLayoutGroup>()?.transform;

            ObjectPoolManager.instance.Instantiate("Bill Button");
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            UpdateBillList();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }

        public void UpdateBillList()
        {
            BillButton billButton;

            Button.ButtonClickedEvent onClickBill;

            GameObject[] billButtons;
            
            BillEvent[] billEvents;

            // Get All Bills and Spawn Bill Buttons
            billEvents = PlayerStatManager.instance.calendarEventList.Where(calendarEvent => calendarEvent.GetType().IsSubclassOf(typeof(BillEvent))).Cast<BillEvent>().ToArray();

            billButtons = ObjectPoolManager.instance.GetObjects("Bill Button", billEvents.Length).ToArray();

            for(int i = 0; i < billEvents.Length; i++)
            {
                billButton = billButtons[i].GetComponent<BillButton>();
                
                // Set parent to Bill List GameObject
                billButtons[i].transform.SetParent(_BillListTransform, false);

                // Assign OnClickBill to switch to Bill Viewer Menu and show bill
                onClickBill = new Button.ButtonClickedEvent();

                onClickBill.AddListener(() => 
                {
                    AppMenuGroup appMenuGroup = transform.GetComponentInParent<AppMenuGroup>();

                    appMenuGroup.SwitchTo(_BillViewerMenu);
                    
                    _BillViewerMenu.billEvent = billEvents[i]; // Problem!!!! NO IDEA WHY IT WON"T KEEP THE INDEX
                });

                billButton.onClick = onClickBill;
                
                // Assign Icon and Name to Bill Button
                // Set Icon

                billButton.billName = billEvents[i].name;
            }
        }
    }
}