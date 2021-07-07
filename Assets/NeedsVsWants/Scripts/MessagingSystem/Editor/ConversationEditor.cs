using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

namespace NeedsVsWants.MessagingSystem
{
    [CustomEditor(typeof(Conversation))]
    public class ConversationEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Open Editor"))
                ConversationWindow.Open(new SerializedObject(target), "Conversation Editor");
        }
    }
}