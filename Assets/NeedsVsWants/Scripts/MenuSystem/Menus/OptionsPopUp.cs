using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.MenuSystem
{
    public class OptionsPopUp : BoxPopUp
    {
        [SerializeField]
        AudioTab _AudioTab;
        
        void Start() 
        {
            _AudioTab.SetSettings();
        }

        protected override void OnDisableBoxPopUp()
        {
            _AudioTab.DisableTab();
                
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableBoxPopUp()
        {
            transform.SetActiveChildren(true);
            
            _AudioTab.EnableTab();
        }
    }
}