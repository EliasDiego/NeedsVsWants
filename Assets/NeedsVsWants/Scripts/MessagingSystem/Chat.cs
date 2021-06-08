using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MessagingSystem
{
    [System.Serializable]
    public struct Chat
    {
        public Character[] characters;
        public Message[] messages;
        public Date date;
    }
}