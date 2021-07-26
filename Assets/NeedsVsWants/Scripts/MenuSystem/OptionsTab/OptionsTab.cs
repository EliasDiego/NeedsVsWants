using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    public class OptionsTab : Tab
    {
        protected override void OnEnableTab()
        {
            transform.SetActiveChildren(true);
        }

        protected override void OnDisableTab()
        {
            transform.SetActiveChildren(false);
        }

        public virtual void SetSettings() { }
    }
}