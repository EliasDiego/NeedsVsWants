using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class AppPopUp : PopUp
    {
        [Header("Disable")]
        [SerializeField]
        Button[] _DisableButtons;
        [SerializeField]
        DayProgressor _DayProgressor;

        protected override void onEnablePopUp()
        {
            foreach(Button button in _DisableButtons)
                button.interactable = false;
                
            _DayProgressor.Pause();
        }

        protected override void onDisablePopUp()
        {
            foreach(Button button in _DisableButtons)
                button.interactable = true;

            _DayProgressor.Unpause();
        }
    }
}