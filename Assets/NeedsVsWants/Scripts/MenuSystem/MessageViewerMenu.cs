using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MenuSystem
{
    public class MessageViewerMenu : Menu
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

        protected override void OnSwitchFrom()
        {
            
        }
    }
}