using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class AppPopUp : PopUp
    {
        protected override void onEnablePopUp()
        {
            Phone.instance.DisablePlayerControl();
        }

        protected override void onDisablePopUp()
        {
            Phone.instance.EnablePlayerControl();
        }
    }
}