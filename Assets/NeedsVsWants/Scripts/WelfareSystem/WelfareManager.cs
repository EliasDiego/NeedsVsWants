using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;

namespace NeedsVsWants.WelfareSystem
{
    public class WelfareManager : SimpleSingleton<WelfareManager>
    {
        WelfareSlider[] _WelfareSliders;

        public WelfareSlider[] welfareSliders => _WelfareSliders;

        protected override void Awake() 
        {
            base.Awake();

            _WelfareSliders = GetComponentsInChildren<WelfareSlider>();
        }
    }
}