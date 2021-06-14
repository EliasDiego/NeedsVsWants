using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MessagingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Messages/Conversation")]
    public class Conversation : ScriptableObject
    {
        public string title;
        public Character[] characters;
        public Message[] messages;
        public ChatChoice[] choices;
    }
}