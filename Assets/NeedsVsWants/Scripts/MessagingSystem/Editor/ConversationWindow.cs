using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;

using UnityEditor;

using UnityEditorInternal;

namespace NeedsVsWants.MessagingSystem
{
    public class ConversationWindow : ExtendedEditorWindow<ConversationWindow>
    {
        int _MessageIndex = 0;

        Vector2 _ScrollPosition = Vector2.zero;

        SerializedProperty _MessageProperty;

        bool IsCharactersPropertyValid()
        {
            SerializedProperty charactersProperty = serializedObject.FindProperty("characters");

            if(charactersProperty.arraySize <= 0)
                return false;
            
            else 
            {
                foreach(SerializedProperty property in charactersProperty)
                {
                    if(!property.objectReferenceValue)
                        return false;
                }
            }

            return true;
        }

        void SideBar()
        {
            int tempMessageIndex = 0;

            string characterName;

            SerializedProperty messagesProperty = serializedObject.FindProperty("messages");
            SerializedProperty charactersProperty = serializedObject.FindProperty("characters");
            SerializedProperty titleProperty = serializedObject.FindProperty("title");

            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));

            // Title 
            EditorGUILayout.LabelField("Title", EditorStyles.boldLabel);
            titleProperty.stringValue = EditorGUILayout.TextField(titleProperty.stringValue);
            EditorGUILayout.Space();

            // Characters Reorderable List
            EditorGUILayout.PropertyField(charactersProperty);
            
            // Messages Label
            EditorGUILayout.LabelField("Messages", EditorStyles.boldLabel);

            // Clamp Messages Index to Messages Array Size
            _MessageIndex = messagesProperty.arraySize > 0 ? Mathf.Clamp(_MessageIndex, 0, messagesProperty.arraySize - 1) : 0;

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

                    // Get Character name from property's characterindex
                    if(IsCharactersPropertyValid())
                    {
                        characterName = (charactersProperty.GetArrayElementAtIndex(
                            property.FindPropertyRelative("characterIndex").intValue).objectReferenceValue as Character).name;
                    }

                    else
                        characterName = "";

                    // Message Button
                    if(GUILayout.Button(characterName))
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
                if(messagesProperty.arraySize > 0)
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
            SerializedProperty characterIndexProperty;
            SerializedProperty charactersProperty;
            SerializedProperty textProperty;

            List<string> characterNames = new List<string>();

            Character character;

            if(_MessageProperty == null)
                return;
                
            characterIndexProperty = _MessageProperty.FindPropertyRelative("characterIndex");
            charactersProperty = serializedObject.FindProperty("characters");
            textProperty = _MessageProperty.FindPropertyRelative("text");

            // Get Character Names
            foreach(SerializedProperty property in charactersProperty)
            {
                character = property.objectReferenceValue as Character;
                
                characterNames.Add(character != null ? character.name : "");
            }

            EditorGUILayout.BeginVertical();

            // Character Index
            EditorGUILayout.BeginHorizontal();

            EditorGUILayout.LabelField("Character", GUILayout.MaxWidth(150));
            characterIndexProperty.intValue = IsCharactersPropertyValid() ? EditorGUILayout.Popup(characterIndexProperty.intValue, characterNames.ToArray()) : 0;

            EditorGUILayout.EndHorizontal();

            // Text
            EditorGUILayout.LabelField("Text");
            textProperty.stringValue = EditorGUILayout.TextArea(Regex.Replace(textProperty.stringValue, @"[^a-zA-Z0-9,.]", ""), GUILayout.Height(250));

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
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}