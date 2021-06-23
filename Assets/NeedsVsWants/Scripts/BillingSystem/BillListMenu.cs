using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

namespace NeedsVsWants.BillingSystem
{
    public class BillListMenu : Menu
    {
        [SerializeField]
        BillViewerMenu _BillViewerMenu;
        [SerializeField]
        Transform _BillListTransform;


        void Awake() 
        {
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
            
            BillEvent[] billEvents;
            
            AppMenuGroup appMenuGroup = transform.GetComponentInParent<AppMenuGroup>();

            // Get All Bills
            billEvents = PlayerStatManager.instance.calendarEventList.Where(calendarEvent => calendarEvent.GetType().
                IsSubclassOf(typeof(BillEvent))).Cast<BillEvent>().ToArray();

            foreach(BillEvent billEvent in billEvents)
            {
                billButton = ObjectPoolManager.instance.GetObject("Bill Button").GetComponent<BillButton>();

                billButton.transform.SetParent(_BillListTransform, false);

                billButton.AssignBill(billEvent, appMenuGroup, _BillViewerMenu);
            }

            // billButtons = ObjectPoolManager.instance.GetObjects("Bill Button", billEvents.Length).ToArray();

            // for(int i = 0; i < billEvents.Length; i++)
            // {
            //     billButton = billButtons[i].GetComponent<BillButton>();
                
            //     // Set parent to Bill List GameObject
            //     billButtons[i].transform.SetParent(_BillListTransform, false);

            //     // Assign Bill Event to Button alongside necessities when clicked
            //     billButton.AssignBill(billEvents[i], appMenuGroup, _BillViewerMenu);
            // }
        }
    }
}