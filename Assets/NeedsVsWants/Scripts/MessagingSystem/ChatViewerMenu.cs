using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatViewerMenu : Menu
    {
        [SerializeField]
        TMP_Text _ChatTitle;
        [SerializeField]
        Transform _Content;
        [SerializeField]
        Character _Anne;

        public Chat chat { get; set; }

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Message Box");    
        }

        void UpdateMessageBoxes()
        {
            MessageBox messageBox;

            Character character;

            foreach(Message message in chat.conversation.messages)
            {
                messageBox = ObjectPoolManager.instance.GetObject("Message Box").GetComponent<MessageBox>();

                character = chat.conversation.characters[message.characterIndex];

                messageBox.transform.SetParent(_Content, false);
                messageBox.AssignMessage(character, message.text, character == _Anne);
            }
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _ChatTitle.text = chat.title;

            UpdateMessageBoxes();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}