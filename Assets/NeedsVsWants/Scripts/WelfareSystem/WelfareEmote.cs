using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareEmote : PopUp
    {
        [SerializeField]
        Sprite _PositiveSprite;
        [SerializeField]
        Sprite _NegativeSprite;

        bool _IsPositive = false;

        public bool isPositive 
        { 
            get => _IsPositive; 
            set
            {
                panel.sprite = value ? _PositiveSprite : _NegativeSprite;

                _IsPositive = value;
            }
        }

        protected override void onDisablePopUp()
        {
            transform.SetActiveChildren(true);
        }
    }
}