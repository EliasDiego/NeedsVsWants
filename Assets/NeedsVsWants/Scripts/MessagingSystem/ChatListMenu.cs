using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using NeedsVsWants.Player;
using NeedsVsWants.Patterns;
using NeedsVsWants.MenuSystem;
using NeedsVsWants.PhoneSystem;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatListMenu : Menu
    {
        [SerializeField]
        ChatViewerMenu _ChatViewerMenu;
        [SerializeField]
        ScrollRect _ScrollRect;
        [SerializeField]
        Transform _ContentTransform;
        [SerializeField]
        Audio.AudioAsset _ButtonClickAsset;
        [SerializeField]
        Indicator _Indicator;
        [SerializeField]
        GameObject _TutorialSequence;

        protected override void Start()
        {
            base.Start();
            
            ObjectPoolManager.instance.Instantiate("Chat Button");    
            
            PlayerStatManager.instance.onAddChat += chat =>
            {
                int messagesUnread = PlayerStatManager.instance.chats.Where(chat => !chat.hasRead).Count();

                if(isActive)
                    UpdateChatList();

                if(messagesUnread > 0)
                {
                    _Indicator.gameObject.SetActive(true);
                    _Indicator.text = messagesUnread.ToString();
                }

                else
                    _Indicator.gameObject.SetActive(false);
            };
        }

        void UpdateChatList()
        {
            ChatButton chatButton;

            AppMenuGroup appMenuGroup = GetComponentInParent<AppMenuGroup>();

            Chat[] chatList = PlayerStatManager.instance.chats;

            // Return current Chat Buttons to Pool
            for(int i = 0; i < _ContentTransform.childCount; i++)
                _ContentTransform.GetChild(i).gameObject.SetActive(false);

            for(int i = chatList.Length - 1; i >= 0; i--)
            {
                chatButton = ObjectPoolManager.instance.GetObject("Chat Button").GetComponent<ChatButton>();

                chatButton.transform.SetParent(_ContentTransform, false);

                chatButton.AssignChat(chatList[i], appMenuGroup, _ChatViewerMenu, () => _ButtonClickAsset.PlayOneShot(audioSource));
            }
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
            
            _TutorialSequence.SetActive(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
            
            UpdateChatList();

            _ScrollRect.verticalNormalizedPosition = 1;
            
            _TutorialSequence.SetActive(true);
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}