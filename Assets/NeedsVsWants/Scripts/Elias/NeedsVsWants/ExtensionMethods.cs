using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeedsVsWants
{
    public static class ExtensionMethods
    {
        public static void SetActiveChildren(this Transform transform, bool isActive)
        {
            for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(isActive);
        }
    }
}