using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.UI;

using NeedsVsWants.Player;
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

        Conversation _CurrentConversation;

        bool _IsShowingChoice = false;

        public Chat chat { get; set; }

        void Awake() 
        {
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

            _IsShowingChoice = false;

            chat.currentMessageIndex = 0;
            chat.choicesMadeList.Add(choiceIndex);

            _CurrentConversation = chatChoice.nextConversation;

            if(chatChoice.applyEffects)
            {
                PlayerStatManager.instance.currentMoney += chatChoice.moneyOnChoice;
                PlayerStatManager.instance.currentHealthWelfare = chatChoice.welfareOnChoice.GetHealth(PlayerStatManager.instance.currentHealthWelfare);
                PlayerStatManager.instance.currentHappinessWelfare = chatChoice.welfareOnChoice.GetHealth(PlayerStatManager.instance.currentHappinessWelfare);
                PlayerStatManager.instance.currentHungerWelfare = chatChoice.welfareOnChoice.GetHealth(PlayerStatManager.instance.currentHungerWelfare);
                PlayerStatManager.instance.currentSocialWelfare = chatChoice.welfareOnChoice.GetHealth(PlayerStatManager.instance.currentSocialWelfare);
            }

            if(_CurrentConversation)
                DisplayToCurrentMessageOrChoice();
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
                if(!_IsShowingChoice && _CurrentConversation.choices.Length > 0)
                {
                    // Show Choices Here
                    AddChatChoiceHolder(_CurrentConversation.choices, OnClickChoice);

                    // move the Scroll Position to current Chat Choice Holder
                    ScrollToBottom();

                    _IsShowingChoice = true;
                }

                else
                    chat.currentMessageIndex--;
            }

            else
            {
                message = _CurrentConversation.messages[chat.currentMessageIndex];

                // Add Another Message Box
                AddMessageBox(message, _CurrentConversation.characters[message.characterIndex]);
                
                // move the Scroll Position to current Message Box       
                ScrollToBottom();
            }
        }

        async void ScrollToBottom()
        {
            await System.Threading.Tasks.Task.Delay(20);

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

            // foreach(Message message in conversation.messages)
            //     AddMessageBox(message, conversation.characters[message.characterIndex]);
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

            // Message[] messages = conversation.messages;

            // for(int i = 0; i <= currentIndex; i++)
            //     AddMessageBox(messages[i], conversation.characters[messages[i].characterIndex]);
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
            transform.SetActiveChildren(false);

            _IsShowingChoice = false;
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _ChatTitle.text = chat.title;

            FillChat();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}