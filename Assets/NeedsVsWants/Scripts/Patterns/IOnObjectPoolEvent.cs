using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants.Patterns
{
    public interface IOnObjectPoolEvent
    {
        void OnReset();
    }
}