using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants
{
    [System.Serializable]
    public struct FloatMinMax
    {
        public float min;
        public float max;

        public FloatMinMax(float min, float max)
        {
            this.min = min;
            this.max = max;
        }
    }
}