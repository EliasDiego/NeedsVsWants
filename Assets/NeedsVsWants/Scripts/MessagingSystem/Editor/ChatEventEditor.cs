using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.MessagingSystem
{
    [CustomEditor(typeof(ChatEvent))]
    public class ChatEventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Open Editor"))
                ChatEventWindow.Open(new SerializedObject(target), "Chat Event Editor");
        }
    }
}