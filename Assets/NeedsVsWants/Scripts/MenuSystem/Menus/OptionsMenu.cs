using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MenuSystem
{
    public class OptionsMenu : Menu
    {
        Tab[] _Tabs;

        void Awake() 
        {
            _Tabs = GetComponentsInChildren<Tab>(true);
        }

        protected override void OnDisableMenu()
        {
            for(int i = 0; i < _Tabs.Length; i++)
                _Tabs[i].DisableTab();
                
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
            
            _Tabs[0].EnableTab();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}