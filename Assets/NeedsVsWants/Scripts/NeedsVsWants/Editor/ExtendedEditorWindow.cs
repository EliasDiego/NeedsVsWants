using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants
{
    public abstract class ExtendedEditorWindow<TWindow> : EditorWindow where TWindow : ExtendedEditorWindow<TWindow>
    {
        protected SerializedObject serializedObject { get; set; }
        
        public static void Open(SerializedObject serializedObject, string titleName)
        {
            TWindow window = GetWindow<TWindow>();

            window.titleContent = new GUIContent(titleName);

            window.serializedObject = serializedObject;
        }

        void OnGUI() 
        {
            if(serializedObject == null)
                OnNullSerializeObject();

            else
                OnValidSerializeObject();
        }

        protected abstract void OnValidSerializeObject();

        protected abstract void OnNullSerializeObject();
    }
}