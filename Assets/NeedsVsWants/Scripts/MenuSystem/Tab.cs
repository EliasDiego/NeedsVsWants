using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using NeedsVsWants;

namespace NeedsVsWants.MenuSystem
{
    public abstract class Tab : MonoBehaviour
    {
        bool _IsActive = false;

        public bool isActive => _IsActive;

        static Tab _Current;

        public static Tab current => _Current;

        #if UNITY_EDITOR
        [CustomEditor(typeof(Tab), true)]
        class TabEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                Tab tab = target as Tab;
         
                SerializedProperty iterator = serializedObject.GetIterator();

                if(!tab)
                    return;

                EditorGUI.BeginChangeCheck();

                // Script
                iterator.NextVisible(true);

                using (new EditorGUI.DisabledScope("m_Script" == iterator.propertyPath))
                    EditorGUILayout.PropertyField(iterator, true);

                // Enable Disable Buttons
                GUILayout.BeginHorizontal();

                if(GUILayout.Button("Enable Tab"))
                    tab.transform.SetActiveChildren(true);

                if(GUILayout.Button("Disable Tab"))
                    tab.transform.SetActiveChildren(false);

                GUILayout.EndHorizontal();
         
                // Base Fields 
                EditorGUILayout.Space();

                while (iterator.NextVisible(false))
                    EditorGUILayout.PropertyField(iterator, true);

                EditorGUI.EndChangeCheck();
         
                serializedObject.ApplyModifiedProperties();
            }
        }
        #endif

        protected abstract void OnEnableTab();
        protected abstract void OnDisableTab();

        public void SwitchTo(Tab tab)
        {
            DisableTab();

            tab.EnableTab();
        }

        public void EnableTab()
        {
            OnEnableTab();

            _Current = this;

            _IsActive = true;
        }

        public void DisableTab()
        {
            transform.SetActiveChildren(false);

            OnDisableTab();

            _Current = null;

            _IsActive = false;
        }
    }
}