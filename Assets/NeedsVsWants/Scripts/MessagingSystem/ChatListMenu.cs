using System.Collections;
using System.Collections.Generic;

using UnityEngine;

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
        Transform _ContentTransform;

        void Awake() 
        {
            ObjectPoolManager.instance.Instantiate("Chat Button");    
            
            PlayerStatManager.instance.onNewChat += conversation =>
            {
                if(isActive)
                    UpdateChatList();
            };
        }

        void UpdateChatList()
        {
            ChatButton chatButton;

            AppMenuGroup appMenuGroup = GetComponentInParent<AppMenuGroup>();

            Chat[] chatList = PlayerStatManager.instance.chatList;

            // Return current Chat Buttons to Pool
            for(int i = 0; i < _ContentTransform.childCount; i++)
                _ContentTransform.GetChild(i).gameObject.SetActive(false);

            for(int i = chatList.Length - 1; i >= 0; i--)
            {
                chatButton = ObjectPoolManager.instance.GetObject("Chat Button").GetComponent<ChatButton>();

                chatButton.transform.SetParent(_ContentTransform, false);

                chatButton.AssignChat(chatList[i], appMenuGroup, _ChatViewerMenu);
            }
        }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);
            
            UpdateChatList();
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}