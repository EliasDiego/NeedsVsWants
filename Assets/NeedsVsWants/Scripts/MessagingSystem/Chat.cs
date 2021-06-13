using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;
using System;

namespace NeedsVsWants.MessagingSystem
{
    public class Chat
    {
        public string title => conversation.title;
        
        public bool hasRead = false;

        public int currentMessageIndex = 0;

        public List<int> choicesMadeList = new List<int>();

        public Conversation conversation;
    }
}