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
        GameObject _TutorialSequence;
        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);

            _TutorialSequence.SetActive(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _TutorialSequence.SetActive(true);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}