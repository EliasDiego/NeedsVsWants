using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.InvestmentSystem
{
    public class InvestmentListMenu : Menu
    {
        [SerializeField]
        GameObject _TutorialSequence;
        
        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _TutorialSequence.SetActive(true);
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
            
            _TutorialSequence.SetActive(false);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}