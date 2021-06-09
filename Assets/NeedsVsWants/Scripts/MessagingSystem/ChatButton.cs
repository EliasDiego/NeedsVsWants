using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatButton : Button
    {
        TMP_Text _ChatTitle;
        TMP_Text _PreviewText;

        Image _Icon;

        protected override void Awake() 
        {
            TMP_Text[] texts = GetComponentsInChildren<TMP_Text>();

            _Icon = transform.GetChild(1).GetComponentInChildren<Image>();

            _ChatTitle = texts[0];
            _PreviewText = texts[1];
        }

        public void AssignChat(Chat chat, MenuSystem.AppMenuGroup appMenuGroup, ChatViewerMenu chatViewerMenu)
        {
            Message message;

            _ChatTitle.text = chat.title;
            _ChatTitle.fontStyle = chat.hasRead ? FontStyles.Normal : FontStyles.Bold;

            if(chat.conversation.messages.Count() > 0)
            {
                message = chat.conversation.messages[0];

                _PreviewText.text = chat.conversation.characters[message.characterIndex].name + ": " + message.text;
            }

            else
                _PreviewText.text  = "";

            //_Icon.sprite = billEvent.icon;

            onClick.RemoveAllListeners();

            onClick.AddListener(() =>
            {
                chatViewerMenu.chat = chat;

                appMenuGroup.SwitchTo(chatViewerMenu);
            });
        }
    }
}