using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

using NeedsVsWants.Audio;
using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

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
        [SerializeField]
        AudioAsset _MessageSFXAsset;
        [SerializeField]
        AudioAsset _ButtonClickAsset;
        [SerializeField]
        Indicator _Indicator;
        [SerializeField]
        TMP_Text _LeftClickText;
        [SerializeField]
        PopUp _PopUpPanel;

        Conversation _CurrentConversation;

        bool _IsShowingChoice = false;

        public Chat chat { get; set; }

        protected override void Start()
        {
            base.Start();
            
            ObjectPoolManager.instance.Instantiate("Message Box");    
            ObjectPoolManager.instance.Instantiate("Chat Choice Button");
            ObjectPoolManager.instance.Instantiate("Chat Choices Holder");

            _InputModule.leftClick.action.canceled += context =>
            {
                if(!isActive)
                    return;

                RectTransform rectTransform = transform as RectTransform;
                
                Bounds menuBounds = new Bounds((Vector2)rectTransform.position, rectTransform.rect.size);

                if(_CurrentConversation && menuBounds.Contains(_InputModule.point.action.ReadValue<Vector2>()))
                    OnClickScreen();
            };
        }

        void OnClickChoice(int choiceIndex)
        {
            ChatChoice chatChoice = _CurrentConversation.choices[choiceIndex];

            _ButtonClickAsset.PlayOneShot(audioSource);

            _IsShowingChoice = false;

            chat.currentMessageIndex = 0;
            chat.choicesMadeList.Add(choiceIndex);

            _CurrentConversation = chatChoice.nextConversation;

            if(chatChoice.applyEffects)
            {
                PlayerStatManager.instance.currentMoney += chatChoice.moneyOnChoice;

                WelfareSystem.WelfareDropManager.instance.SpawnWelfareDropsOnAnne(chatChoice.welfareOnChoice, 5);

                chatChoice.onChoiceEvent?.Invoke();
            }

            if(_CurrentConversation)
                DisplayToCurrentMessageOrChoice();
                
            else
            {
                chat.hasRead = true;

                Phone.instance.EnablePlayerControl();
                
                _PopUpPanel.DisablePopUp();
            }
        }

        void OnClickScreen()
        {
            Message message;

            // Update Current Message Index
            chat.currentMessageIndex++;

            // If Message Index goes beyond message length
            if(chat.currentMessageIndex >= _CurrentConversation.messages.Length)
            {
                // Check if there are choices
                if(!_IsShowingChoice)
                {
                    if(_CurrentConversation.choices.Length > 0)
                    {
                        // Show Choices Here
                        AddChatChoiceHolder(_CurrentConversation.choices, OnClickChoice);

                        // move the Scroll Position to current Chat Choice Holder
                        ScrollToBottom();

                        _IsShowingChoice = true;
                    }

                    else if(!chat.hasRead)// If At the end of the conversation
                    {
                        chat.hasRead = true;

                        Phone.instance.EnablePlayerControl();

                        _PopUpPanel.DisablePopUp();

                        chat.currentMessageIndex--;
                    }
                }
            }

            else
            {
                message = _CurrentConversation.messages[chat.currentMessageIndex];

                // Add Another Message Box
                AddMessageBox(message, _CurrentConversation.characters[message.characterIndex]);
                
                // move the Scroll Position to current Message Box       
                ScrollToBottom();

                _MessageSFXAsset.PlayOneShot(audioSource);
            }
            
            if(_LeftClickText.gameObject.activeSelf)
                _LeftClickText.gameObject.SetActive(false);
        }

        async void ScrollToBottom()
        {
            await System.Threading.Tasks.Task.Delay(50);

            _ScrollRect.verticalNormalizedPosition = 0;
        }

        async void AddChatChoiceHolder(string choiceName)
        {
            ChatChoicesHolder choicesHolder = ObjectPoolManager.instance.GetObject("Chat Choices Holder").GetComponent<ChatChoicesHolder>();

            choicesHolder.ShowChoice(choiceName);
            
            await System.Threading.Tasks.Task.Delay(10);
            
            choicesHolder.transform.SetParent(_ContentTransform, false);
        }

        async void AddChatChoiceHolder(ChatChoice[] choices, System.Action<int> onClickChoice)
        {
            ChatChoicesHolder choicesHolder = ObjectPoolManager.instance.GetObject("Chat Choices Holder").GetComponent<ChatChoicesHolder>();

            choicesHolder.AssignChoices(choices, onClickChoice);
            
            await System.Threading.Tasks.Task.Delay(10);
            
            choicesHolder.transform.SetParent(_ContentTransform, false);
        }

        async void AddMessageBox(Message message, Character character)
        {
            MessageBox messageBox = ObjectPoolManager.instance.GetObject("Message Box").GetComponent<MessageBox>();

            messageBox.AssignMessage(character, message.text, character == _Anne);

            await System.Threading.Tasks.Task.Delay(10);
            
            messageBox.transform.SetParent(_ContentTransform, false);
        }

        void DisplayToCurrentMessageOrChoice()
        {
            DisplayMessages(_CurrentConversation, Mathf.Clamp(chat.currentMessageIndex, 0, _CurrentConversation.messages.Length - 1));

            // If player is currently in a choice
            if(chat.currentMessageIndex >= _CurrentConversation.messages.Length)
            {
                _IsShowingChoice = true;

                AddChatChoiceHolder(_CurrentConversation.choices, OnClickChoice);
            }
        }

        async void DisplayAllMessages(Conversation conversation)
        {
            Message message;

            Character character;

            MessageBox[] messageBoxes = ObjectPoolManager.instance.GetObjects("Message Box", conversation.messages.Length).
                Select(g => g.GetComponent<MessageBox>()).ToArray();

            for(int i = 0; i < messageBoxes.Length; i++)
            {
                message = conversation.messages[i];

                character = conversation.characters[message.characterIndex];

                messageBoxes[i].AssignMessage(character, message.text, character == _Anne);
            }
                
            await System.Threading.Tasks.Task.Delay(10);
            
            foreach(MessageBox messageBox in messageBoxes)
                messageBox.transform.SetParent(_ContentTransform, false);
        }

        async void DisplayMessages(Conversation conversation, int currentIndex)
        {
            Message message;

            Character character;

            MessageBox[] messageBoxes = ObjectPoolManager.instance.GetObjects("Message Box", currentIndex + 1).
                Select(g => g.GetComponent<MessageBox>()).ToArray();
            
            for(int i = 0; i <= currentIndex; i++)
            {
                message = conversation.messages[i];

                character = conversation.characters[message.characterIndex];

                messageBoxes[i].AssignMessage(character, message.text, character == _Anne);
            }
            
            await System.Threading.Tasks.Task.Delay(10);
            
            foreach(MessageBox messageBox in messageBoxes)
                messageBox.transform.SetParent(_ContentTransform, false);
        }

        void FillChat()
        {
            _CurrentConversation = chat.conversation;

            // if player went through choices
            if(chat.choicesMadeList.Count > 0)
            {
                foreach(int i in chat.choicesMadeList)
                {
                    DisplayAllMessages(_CurrentConversation);

                    AddChatChoiceHolder(_CurrentConversation.choices[i].name);

                    _CurrentConversation = _CurrentConversation.choices[i].nextConversation;
                }

                if(_CurrentConversation)
                    DisplayToCurrentMessageOrChoice();
            }
            
            // If player didn't go through choices
            else
                DisplayToCurrentMessageOrChoice();

            ScrollToBottom();
        }

        protected override void OnDisableMenu()
        {
            int messagesUnread = PlayerStatManager.instance.chats.Where(chat => !chat.hasRead).Count();
            
            transform.SetActiveChildren(false);

            _IsShowingChoice = false;

            if(messagesUnread > 0)
            {
                _Indicator.gameObject.SetActive(true);
                _Indicator.text = messagesUnread.ToString();
            }

            else
                _Indicator.gameObject.SetActive(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _ChatTitle.text = chat.title;

            FillChat();

            if(!chat.hasRead)
            {
                Phone.instance.DisablePlayerControl();

                _MessageSFXAsset.PlayOneShot(audioSource);

                _PopUpPanel.EnablePopUp();

                _LeftClickText.gameObject.SetActive(true);
            }
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}