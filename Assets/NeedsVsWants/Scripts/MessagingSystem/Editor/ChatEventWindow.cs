using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;

using UnityEditorInternal;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatEventWindow : ExtendedEditorWindow<ChatEventWindow>
    {
        int _MessageIndex = 0;

        Vector2 _ScrollPosition = Vector2.zero;

        SerializedProperty _MessageProperty;

        void SideBar()
        {
            int tempMessageIndex = 0;

            SerializedProperty chatProperty = serializedObject.FindProperty("chat");
            SerializedProperty messagesProperty = chatProperty.FindPropertyRelative("messages");
            SerializedProperty charactersProperty = chatProperty.FindPropertyRelative("characters");
            SerializedProperty tempCharacterProperty;

            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

            EditorGUILayout.PropertyField(charactersProperty);
            
            EditorGUILayout.LabelField("Messages");

            // Clamp Messages Index to Messages Array Size
            _MessageIndex = Mathf.Clamp(_MessageIndex, 0, messagesProperty.arraySize - 1);

            _ScrollPosition = EditorGUILayout.BeginScrollView(_ScrollPosition);

            if(messagesProperty.arraySize > 0)
            {
                foreach(SerializedProperty property in messagesProperty)
                {
                    // Change Color if the Message is currently selected
                    if(tempMessageIndex == _MessageIndex)
                    {
                        GUI.backgroundColor = Color.grey;

                        // Assign Message Property if not current message
                        if(property != _MessageProperty)
                            _MessageProperty = property;
                    }

                    else
                        GUI.backgroundColor = Color.white;

                    // Get Character Property from currently selected characterIndex
                    tempCharacterProperty = charactersProperty.GetArrayElementAtIndex(property.FindPropertyRelative("characterIndex").intValue);

                    // Message Button
                    if(GUILayout.Button((tempCharacterProperty.objectReferenceValue as Character).name))
                    {
                        _MessageIndex = tempMessageIndex;

                        _MessageProperty = property;

                        GUI.FocusControl(null);
                    }

                    tempMessageIndex++;
                }
            }

            else
                _MessageProperty = null;

            EditorGUILayout.EndScrollView();
            
            // Reset Colors
            GUI.backgroundColor = Color.white;

            // Insert And Delete
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Insert", GUILayout.Width(100)))
            {
                messagesProperty.InsertArrayElementAtIndex(_MessageIndex);
                messagesProperty.GetArrayElementAtIndex(messagesProperty.arraySize - 1).FindPropertyRelative("text").stringValue = "";
            }

            if(GUILayout.Button("Delete", GUILayout.Width(100)))
            {
                messagesProperty.DeleteArrayElementAtIndex(_MessageIndex);

                if(messagesProperty.arraySize <= 0)
                    _MessageProperty = null;
            }

            EditorGUILayout.EndHorizontal();

            // Move Up And Move Down
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Move Up", GUILayout.Width(100)))
            {
                tempMessageIndex = Mathf.Clamp(_MessageIndex - 1, 0, messagesProperty.arraySize - 1);
                
                messagesProperty.MoveArrayElement(_MessageIndex, tempMessageIndex);
                
                _MessageIndex = tempMessageIndex;
            }

            if(GUILayout.Button("Move Down", GUILayout.Width(100)))
            {
                tempMessageIndex = Mathf.Clamp(_MessageIndex + 1, 0, messagesProperty.arraySize - 1);
                
                messagesProperty.MoveArrayElement(_MessageIndex, tempMessageIndex);
                
                _MessageIndex = tempMessageIndex;
            }

            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.EndVertical();
        }

        void ContentPage()
        {
            SerializedProperty chatProperty;
            SerializedProperty characterIndexProperty;
            SerializedProperty textProperty;

            List<string> characterNames = new List<string>();

            Character character;

            if(_MessageProperty == null)
                return;
                
            chatProperty = serializedObject.FindProperty("chat");
            characterIndexProperty = _MessageProperty.FindPropertyRelative("characterIndex");
            textProperty = _MessageProperty.FindPropertyRelative("text");

            // Get Character Names
            foreach(SerializedProperty property in chatProperty.FindPropertyRelative("characters"))
            {
                character = property.objectReferenceValue as Character;
                
                characterNames.Add(character != null ? character.name : "");
            }

            EditorGUILayout.BeginVertical();

            // Character Index
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Character", GUILayout.MaxWidth(150));
            characterIndexProperty.intValue = EditorGUILayout.Popup(characterIndexProperty.intValue, characterNames.ToArray());

            EditorGUILayout.EndHorizontal();

            // Text
            EditorGUILayout.LabelField("Text");
            textProperty.stringValue = EditorGUILayout.TextArea(textProperty.stringValue, GUILayout.Height(250));

            EditorGUILayout.EndVertical();
        }

        protected override void OnNullSerializeObject()
        {
            EditorGUILayout.LabelField("Please select a Chat Event Asset and Press the Open Editor Button.");
        }

        protected override void OnValidSerializeObject()
        {
            EditorGUILayout.BeginHorizontal();

            SideBar();

            ContentPage();

            EditorGUILayout.EndHorizontal();
            
            //serializedObject.ApplyModifiedProperties();
        }
    }
}