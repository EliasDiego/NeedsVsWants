using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using NeedsVsWants.Player;
using NeedsVsWants.CalendarSystem;

namespace NeedsVsWants.MessagingSystem
{
    [CreateAssetMenu(menuName = "NeedsVsWants/Messages/Message Event")]
    public class MessageEvent : CalendarEvent
    {
        public Conversation conversation;
        public ChatChoice[] chatChoices;

        public Date date;
        
        public bool isYearSpecific = false;

        public override bool showOnCalendar => false;

        public override void Invoke(DateTime dateTime)
        {
            PlayerStatManager.instance.AddConversationToChat(conversation);
        }

        public override bool IsWithinDate(DateTime dateTime)
        {
            return dateTime.IsOnSameDay(date, isYearSpecific);
        }
    }
}