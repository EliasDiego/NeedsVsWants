using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

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
        Transform _ContentTransform;
        [SerializeField]
        Character _Anne;
        [SerializeField]
        InputSystemUIInputModule _InputModule;
        [SerializeField]
        ScrollRect _ScrollRect;

        public Chat chat { get; set; }

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Message Box");    

            _InputModule.leftClick.action.canceled += context =>
            {
                if(!isActive)
                    return;

                RectTransform rectTransform = transform as RectTransform;
                
                Bounds menuBounds = new Bounds((Vector2)rectTransform.position, rectTransform.rect.size);

                if(menuBounds.Contains(_InputModule.point.action.ReadValue<Vector2>()))
                {
                    // On Click, Do something
                }
            };
        }

        async void UpdateMessageBoxes()
        {
            MessageBox messageBox;

            List<MessageBox> messageBoxList = new List<MessageBox>();

            Character character;

            // Temp Solution
            foreach(Message message in chat.conversation.messages)
            {
                messageBox = ObjectPoolManager.instance.GetObject("Message Box").GetComponent<MessageBox>();

                character = chat.conversation.characters[message.characterIndex];

                messageBox.AssignMessage(character, message.text, character == _Anne);

                messageBoxList.Add(messageBox);
            }

            // Remove Inertia
            _ScrollRect.inertia = false;
            _ScrollRect.velocity = Vector2.zero;

            foreach(MessageBox m in messageBoxList)
            {
                m.transform.SetParent(_ContentTransform, false);

                _ScrollRect.verticalNormalizedPosition = -1;

                await System.Threading.Tasks.Task.Delay(1);
            }

            _ScrollRect.inertia = true;
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