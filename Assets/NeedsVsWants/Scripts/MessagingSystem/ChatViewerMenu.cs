using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.MenuSystem;

using TMPro;

namespace NeedsVsWants.MessagingSystem
{
    public class ChatViewerMenu : Menu
    {
        [SerializeField]
        TMP_Text _ChatTitle;

        public Chat chat { get; set; }

        protected override void OnDisableMenu()
        {
            transform.SetActiveChildren(false);
        }

        protected override void OnEnableMenu()
        {
            transform.SetActiveChildren(true);

            _ChatTitle.text = chat.title;
        }

        protected override void OnReturn()
        {
            
        }

        protected override void OnSwitchFrom()
        {
            
        }
    }
}