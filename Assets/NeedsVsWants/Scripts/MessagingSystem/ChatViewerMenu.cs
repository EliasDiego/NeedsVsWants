using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatViewerMenu : Menu
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