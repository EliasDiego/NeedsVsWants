using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.CalendarSystem
{
    public abstract class CalendarEvent : ScriptableObject
    {
        // [HideInInspector]
        // public Date minDate; // Do I need these...
        // [HideInInspector]
        // public Date maxDate;

        public abstract bool isShowOnCalendar { get; }

        // #if UNITY_EDITOR
        // [CustomEditor(typeof(CalendarEvent), true)]
        // class CalendarEventEditor : Editor
        // {
        //     bool _IsDateRange = false;

        //     public override void OnInspectorGUI()
        //     {
        //         CalendarEvent calendarEvent = target as CalendarEvent;

        //         if(!calendarEvent)
        //             return;

        //         _IsDateRange = EditorGUILayout.Toggle("Is Date Range?", _IsDateRange);

        //         if(!_IsDateRange)
        //         {
        //             EditorGUILayout.Space();
        //             EditorGUILayout.LabelField("Date", EditorStyles.boldLabel);
                    
        //             calendarEvent.maxDate = calendarEvent.minDate = DrawDate(calendarEvent.minDate);
        //         }

        //         else
        //         {
        //             // Min Date
        //             EditorGUILayout.Space();
        //             EditorGUILayout.LabelField("Min Date", EditorStyles.boldLabel);
                    
        //             calendarEvent.minDate = DrawDate(calendarEvent.minDate);
                    
        //             // Max Date
        //             EditorGUILayout.Space();
        //             EditorGUILayout.LabelField("Max Date", EditorStyles.boldLabel);

        //             calendarEvent.maxDate = DrawDate(calendarEvent.maxDate);
        //         }
        //         //AssetDatabase.SaveAssets();
        //         EditorUtility.SetDirty(target);

        //         EditorGUI.BeginChangeCheck();
        //         serializedObject.UpdateIfRequiredOrScript();

        //         // Loop through properties and create one field (including children) for each top level property.
        //         SerializedProperty property = serializedObject.GetIterator();
                
        //         property.NextVisible(true);
                
        //         while (property.NextVisible(true))
        //             EditorGUILayout.PropertyField(property, true);

        //         //serializedObject.ApplyModifiedProperties();
        //         EditorGUI.EndChangeCheck();
        //     }

        //     public Date DrawDate(Date date) 
        //     {
        //         int year = date.year, month = date.month, day = date.day;
                
        //         year = Mathf.Clamp(EditorGUILayout.IntField("Year", year), 1, 9999);
        //         month = Mathf.Clamp(EditorGUILayout.IntField("Month", month), 1, 12);
        //         day = Mathf.Clamp(EditorGUILayout.IntField("Day", day), 1, DateTime.DaysInMonth(year, month));

        //         return new Date(year, month, day);
        //     }
        // }
        // #endif

        public abstract void Invoke();

        public abstract bool IsWithinDate(DateTime dateTime);

        // public virtual bool IsWithinDateRange(DateTime dateTime)
        // {
        //     DateTime min = new DateTime(minDate.year, minDate.month, minDate.day);
        //     DateTime max = new DateTime(maxDate.year, maxDate.month, maxDate.day, 23, 59, 59);

        //     return min <= dateTime && dateTime <= max;
        // }
    }
}