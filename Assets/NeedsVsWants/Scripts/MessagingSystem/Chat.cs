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
        public Conversation conversation;
    }
}