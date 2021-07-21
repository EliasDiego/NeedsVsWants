using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.PhoneSystem
{
    public class AppPopUp : PopUp
    {
        protected override void OnEnablePopUp()
        {
            Phone.instance.DisablePlayerControl();
        }

        protected override void OnDisablePopUp()
        {
            Phone.instance.EnablePlayerControl();
        }
    }
}