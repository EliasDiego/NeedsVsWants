using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.PhoneSystem;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatButton : Button
    {
        TMP_Text _ChatTitle;
        TMP_Text _PreviewText;

        Image _Icon;
        Image _BackgroundImage;

        protected override void Awake() 
        {
            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

            _BackgroundImage = GetComponent<Image>();

            _Icon = transform.GetChild(0).GetComponentInChildren<Image>();

            _ChatTitle = texts[0];
            _PreviewText = texts[1];
        }

        public void AssignChat(Chat chat, AppMenuGroup appMenuGroup, ChatViewerMenu chatViewerMenu, System.Action onClickEvent)
        {
            Conversation currentConversation;
            Message message;

            _ChatTitle.text = chat.title;

            if(chat.conversation.messages.Count() > 0)
            {
                if(chat.hasRead)
                {
                    currentConversation = chat.conversation;

                    foreach(int choiceIndex in chat.choicesMadeList)
                    {
                        if(currentConversation.choices[choiceIndex].nextConversation)
                            currentConversation = currentConversation.choices[choiceIndex].nextConversation;
                    }
                        
                    message = currentConversation.messages[currentConversation.messages.Length - 1];

                    // Limit Text
                    _PreviewText.text = currentConversation.characters[message.characterIndex].name + ": ";
                    _PreviewText.text += message.text;

                    if(_PreviewText.text.Length > 16)
                        _PreviewText.text = _PreviewText.text.Substring(0, Mathf.Clamp(16, 0, _PreviewText.text.Length)) + "...";
                    
                    _Icon.sprite =  currentConversation.characters[message.characterIndex].profilePicture;
                }

                else
                {
                    message = chat.conversation.messages[0];

                    // Limit Text
                    _PreviewText.text = chat.conversation.characters[message.characterIndex].name + ": ";
                    _PreviewText.text += message.text;
                    
                    if(_PreviewText.text.Length > 16)
                        _PreviewText.text = _PreviewText.text.Substring(0, Mathf.Clamp(16, 0, _PreviewText.text.Length)) + "...";

                    _Icon.sprite =  chat.conversation.characters[message.characterIndex].profilePicture;
                }
            }

            else
                _PreviewText.text  = "";

            Color color = _BackgroundImage.color;

            color.a = chat.hasRead ? .5f : 1;

            _BackgroundImage.color = color; 

            onClick.RemoveAllListeners();

            onClick.AddListener(() =>
            {
                chatViewerMenu.chat = chat;

                appMenuGroup.SwitchTo(chatViewerMenu);

                onClickEvent?.Invoke();
            });
        }
    }
}