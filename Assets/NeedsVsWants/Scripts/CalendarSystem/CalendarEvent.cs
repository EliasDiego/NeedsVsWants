using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.CalendarSystem
{
    public abstract class CalendarEvent : ScriptableObject
    {
        public UDateTime minDateTime { get; set; }
        public UDateTime maxDateTime { get; set; }


        #if UNITY_EDITOR
        [CustomEditor(typeof(CalendarEvent), true)]
        class CalendarEventEditor : Editor
        {
            bool _IsDateRange = false;

            public override void OnInspectorGUI()
            {
                CalendarEvent calendarEvent = target as CalendarEvent;

                if(!calendarEvent)
                    return;

                _IsDateRange = EditorGUILayout.Toggle("Is Date Range?", _IsDateRange);

                if(!_IsDateRange)
                {
                    // Min Date
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Date", EditorStyles.boldLabel);
                    
                    calendarEvent.maxDateTime = calendarEvent.minDateTime = DrawDateTime(calendarEvent.minDateTime);
                }

                else
                {
                    // Min Date
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Min Date", EditorStyles.boldLabel);
                    
                    calendarEvent.minDateTime = DrawDateTime(calendarEvent.minDateTime);
                    
                    // Max Date
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Max Date", EditorStyles.boldLabel);

                    calendarEvent.maxDateTime = DrawDateTime(calendarEvent.maxDateTime);
                }

                EditorGUI.BeginChangeCheck();
                serializedObject.UpdateIfRequiredOrScript();

                // Loop through properties and create one field (including children) for each top level property.
                SerializedProperty property = serializedObject.GetIterator();
                
                property.NextVisible(true);
                
                while (property.NextVisible(true))
                    EditorGUILayout.PropertyField(property, true);

                serializedObject.ApplyModifiedProperties();
                EditorGUI.EndChangeCheck();
            }

            public DateTime DrawDateTime(DateTime dateTime) 
            {
                int year = dateTime.Year, month = dateTime.Month, day = dateTime.Day;
                
                year = Mathf.Clamp(EditorGUILayout.IntField("Year", year), 1, 9999);
                month = Mathf.Clamp(EditorGUILayout.IntField("Month", month), 1, 12);
                day = Mathf.Clamp(EditorGUILayout.IntField("Day", day), 1, DateTime.DaysInMonth(year, month));

                return new DateTime(year, month, day);
            }
        }
        #endif

        public abstract void Invoke();

        public bool IsWithinDateRange(DateTime dateTime)
        {
            Debug.Log(minDateTime == null);
            DateTime max = new DateTime(maxDateTime.dateTime.Year, maxDateTime.dateTime.Month, maxDateTime.dateTime.Day, 23, 59, 59);

            return minDateTime.dateTime <= dateTime && dateTime <= max;
        }
    }
}