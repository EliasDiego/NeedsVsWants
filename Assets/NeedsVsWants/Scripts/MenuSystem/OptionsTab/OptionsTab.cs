using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

namespace NeedsVsWants.MenuSystem
{
    public abstract class OptionsTab : Tab
    {
        [SerializeField]
        Button _TabButton;

        void Start()
        {
            _TabButton.onClick.AddListener(() => 
            {
                if(current)
                    current.SwitchTo(this);

                else
                    EnableTab();
            });
        }

        protected override void OnEnableTab()
        {
            transform.SetActiveChildren(true);

            _TabButton.image.color = Color.white;
        }

        protected override void OnDisableTab()
        {
            transform.SetActiveChildren(false);
            
            _TabButton.gameObject.SetActive(true);
            _TabButton.image.color = Color.grey;
        }
    }
}