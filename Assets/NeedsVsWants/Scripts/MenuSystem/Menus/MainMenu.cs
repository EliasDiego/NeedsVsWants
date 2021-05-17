using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants;

namespace NeedsVsWants.MenuSystem
{
    public class MainMenu : Menu
    {
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
    }
}