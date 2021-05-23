using System;
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

        public static bool IsOnSameMonth(this DateTime dateTime, DateTime otherDateTime) =>
            dateTime.Year == otherDateTime.Year && dateTime.Month == otherDateTime.Month;

        public static bool IsOnSameDay(this DateTime dateTime, DateTime otherDateTime) =>
            IsOnSameMonth(dateTime, otherDateTime) && dateTime.Day == otherDateTime.Day;
    }
}