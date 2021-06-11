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
        int _ChoiceIndex = 0;

        bool _ShowMessages = true;

        Vector2 _MessagesScrollPosition = Vector2.zero;
        Vector2 _ChoicesScrollPosition = Vector2.zero;

        SerializedProperty _MessageProperty;
        SerializedProperty _ChoiceProperty;

        void ShowMessages()
        {
            int tempMessageIndex = 0;

            string characterName;

            SerializedProperty messagesProperty = serializedObject.FindProperty("messages");
            SerializedProperty charactersProperty = serializedObject.FindProperty("characters");
            SerializedProperty titleProperty = serializedObject.FindProperty("title");

             // Clamp Messages Index to Messages Array Size
            _MessageIndex = messagesProperty.arraySize > 0 ? Mathf.Clamp(_MessageIndex, 0, messagesProperty.arraySize - 1) : 0;

            _MessagesScrollPosition = EditorGUILayout.BeginScrollView(_MessagesScrollPosition);

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
        }

        void ShowMessageContent()
        {
            if(_MessageProperty == null)
                return;
                
            SerializedProperty characterIndexProperty = _MessageProperty.FindPropertyRelative("characterIndex");
            SerializedProperty charactersProperty = serializedObject.FindProperty("characters");
            SerializedProperty textProperty = _MessageProperty.FindPropertyRelative("text");

            GUIStyle textAreaGUIStyle;

            List<string> characterNames = new List<string>();

            Character character;

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

            textAreaGUIStyle = EditorStyles.textArea;
            textAreaGUIStyle.wordWrap = true;
            
            textProperty.stringValue = EditorGUILayout.TextArea(Regex.Replace(textProperty.stringValue, @"[^a-zA-Z0-9,.]", ""), textAreaGUIStyle, GUILayout.Height(250));

            EditorGUILayout.EndVertical();
        }

        void ShowChoices()
        {
            int tempChoiceIndex = 0;

            SerializedProperty choicesProperty = serializedObject.FindProperty("choices");

            _ChoiceIndex = choicesProperty.arraySize > 0 ? Mathf.Clamp(_ChoiceIndex, 0, choicesProperty.arraySize - 1) : 0;

            // Scroll View
            _ChoicesScrollPosition = EditorGUILayout.BeginScrollView(_ChoicesScrollPosition);

            if(choicesProperty.arraySize > 0)
            {
                foreach(SerializedProperty property in choicesProperty)
                {
                    // Change Color if the Message is currently selected
                    if(tempChoiceIndex == _ChoiceIndex)
                    {
                        GUI.backgroundColor = Color.grey;

                        // Assign Message Property if not current message
                        if(property != _ChoiceProperty)
                            _ChoiceProperty = property;
                    }

                    else
                        GUI.backgroundColor = Color.white;

                    if(GUILayout.Button(property.FindPropertyRelative("name").stringValue))
                    {
                        _ChoiceIndex = tempChoiceIndex;

                        _ChoiceProperty = property;

                        GUI.FocusControl(null);
                    }

                    tempChoiceIndex++;
                }
            }

            else
                _ChoiceProperty = null;

            EditorGUILayout.EndScrollView();
            
            // Reset Colors
            GUI.backgroundColor = Color.white;

            // Insert And Delete
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Insert", GUILayout.Width(100)))
                choicesProperty.InsertArrayElementAtIndex(_ChoiceIndex);

            if(GUILayout.Button("Delete", GUILayout.Width(100)))
            {
                if(choicesProperty.arraySize > 0)
                    choicesProperty.DeleteArrayElementAtIndex(_ChoiceIndex);

                _ChoiceProperty = null;
            }

            EditorGUILayout.EndHorizontal();

            // Move Up And Move Down
            EditorGUILayout.BeginHorizontal();

            if(GUILayout.Button("Move Up", GUILayout.Width(100)))
            {
                tempChoiceIndex = Mathf.Clamp(_ChoiceIndex - 1, 0, choicesProperty.arraySize - 1);
                
                choicesProperty.MoveArrayElement(_ChoiceIndex, tempChoiceIndex);
                
                _ChoiceIndex = tempChoiceIndex;
                
            }

            if(GUILayout.Button("Move Down", GUILayout.Width(100)))
            {
                tempChoiceIndex = Mathf.Clamp(_ChoiceIndex + 1, 0, choicesProperty.arraySize - 1);
                
                choicesProperty.MoveArrayElement(_ChoiceIndex, tempChoiceIndex);
                
                _ChoiceIndex = tempChoiceIndex;
            }

            EditorGUILayout.EndHorizontal();
        }

        void ShowChoiceContent()
        {
            if(_ChoiceProperty == null)
                return;

            EditorGUILayout.BeginVertical();
            
            EditorGUILayout.PropertyField(_ChoiceProperty.FindPropertyRelative("name"));
            EditorGUILayout.PropertyField(_ChoiceProperty.FindPropertyRelative("nextConversation"));

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Effects On Choice", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(_ChoiceProperty.FindPropertyRelative("moneyOnChoice"));

            EditorGUILayout.PropertyField(_ChoiceProperty.FindPropertyRelative("welfareOnChoice"));
            
            EditorGUILayout.EndVertical();
        }

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
            EditorGUILayout.BeginHorizontal();
         
            GUI.color = _ShowMessages ? Color.grey : Color.white;
            if(GUILayout.Button("Messages"))
                _ShowMessages = true;

            GUI.color = _ShowMessages ? Color.white :  Color.grey;
            if(GUILayout.Button("Choices"))
                _ShowMessages = false;

            GUI.color = Color.white;
         
            EditorGUILayout.EndHorizontal();

            if(_ShowMessages)
                ShowMessages();

            else
                ShowChoices();
            
            EditorGUILayout.EndVertical();
        }

        void ContentPage()
        {
            if(_ShowMessages)
                ShowMessageContent();

            else
                ShowChoiceContent();
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