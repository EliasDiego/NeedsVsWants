using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.WelfareSystem
{
    [System.Serializable]
    public struct WelfareValue
    {
        public float value;
        public float maxValue;

        public WelfareValue(float value, float maxValue)
        {
            this.value = value;

            this.maxValue = maxValue;
        }
    }
}