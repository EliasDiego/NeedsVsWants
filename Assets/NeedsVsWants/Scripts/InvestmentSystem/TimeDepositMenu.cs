using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.InvestmentSystem
{
    public class TimeDepositMenu :Menu
    {
        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}